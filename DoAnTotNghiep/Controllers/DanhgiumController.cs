using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/danhgium")] // Fixed RESTful route
    [ApiController]
    public class DanhgiumController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public DanhgiumController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/danhgium
        // Retrieve all evaluation records
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Danhgium>>> GetAllDanhgiums()
        {
            var danhgiums = await _context.Danhgia.ToListAsync();
            if (danhgiums == null || danhgiums.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy đánh giá nào." });
            }
            return Ok(danhgiums);
        }

        // GET: api/danhgium/{msv}
        // Retrieve evaluation records by Msv (student ID)
        [HttpGet("{msv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Danhgium>>> GetDanhgiumByMsv(string msv)
        {
            var danhgiums = await _context.Danhgia.Where(d => d.Msv == msv).ToListAsync();
            if (danhgiums == null || danhgiums.Count == 0)
            {
                return NotFound(new { message = $"Không tìm thấy đánh giá nào cho mã sinh viên: {msv}" });
            }
            return Ok(danhgiums);
        }

        // POST: api/danhgium
        // Create a new evaluation record
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Danhgium>> CreateDanhgium([FromBody] Danhgium danhgium)
        {
            if (!ModelState.IsValid || danhgium == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            // Validate Msv exists in Sinhvien table
            if (!await _context.Sinhviens.AnyAsync(s => s.Msv == danhgium.Msv))
            {
                return BadRequest(new { message = $"Mã sinh viên {danhgium.Msv} không tồn tại." });
            }

            _context.Danhgia.Add(danhgium);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDanhgiumByMsv), new { msv = danhgium.Msv }, danhgium);
        }

        // PUT: api/danhgium/{msv}/{nguoidg}/{ngaydg}
        // Update an existing evaluation record (assuming composite key: Msv, Nguoidg, Ngaydg)
        [HttpPut("{msv}/{nguoidg}/{ngaydg}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDanhgium(string msv, string nguoidg, DateTime ngaydg, [FromBody] Danhgium danhgium)
        {
            if (msv != danhgium.Msv || nguoidg != danhgium.Nguoidg || ngaydg != danhgium.Ngaydg || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc thông tin đánh giá không khớp." });
            }

            var existingDanhgium = await _context.Danhgia
                .FirstOrDefaultAsync(d => d.Msv == msv && d.Nguoidg == nguoidg && d.Ngaydg == ngaydg);
            if (existingDanhgium == null)
            {
                return NotFound(new { message = $"Không tìm thấy đánh giá với mã sinh viên: {msv}, người đánh giá: {nguoidg}, ngày: {ngaydg}" });
            }

            // Validate Msv exists in Sinhvien table
            if (!await _context.Sinhviens.AnyAsync(s => s.Msv == danhgium.Msv))
            {
                return BadRequest(new { message = $"Mã sinh viên {danhgium.Msv} không tồn tại." });
            }

            _context.Entry(existingDanhgium).CurrentValues.SetValues(danhgium);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhgiumExists(msv, nguoidg, ngaydg))
                {
                    return NotFound(new { message = $"Không tìm thấy đánh giá với mã sinh viên: {msv}, người đánh giá: {nguoidg}, ngày: {ngaydg}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/danhgium/{msv}/{nguoidg}/{ngaydg}
        // Delete an evaluation record (assuming composite key: Msv, Nguoidg, Ngaydg)
        [HttpDelete("{msv}/{nguoidg}/{ngaydg}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDanhgium(string msv, string nguoidg, DateTime ngaydg)
        {
            var danhgium = await _context.Danhgia
                .FirstOrDefaultAsync(d => d.Msv == msv && d.Nguoidg == nguoidg && d.Ngaydg == ngaydg);
            if (danhgium == null)
            {
                return NotFound(new { message = $"Không tìm thấy đánh giá với mã sinh viên: {msv}, người đánh giá: {nguoidg}, ngày: {ngaydg}" });
            }

            _context.Danhgia.Remove(danhgium);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa đánh giá với mã sinh viên: {msv}, người đánh giá: {nguoidg}, ngày: {ngaydg}" });
        }

        // Helper method to check if an evaluation record exists
        private bool DanhgiumExists(string msv, string nguoidg, DateTime ngaydg)
        {
            return _context.Danhgia.Any(e => e.Msv == msv && e.Nguoidg == nguoidg && e.Ngaydg == ngaydg);
        }
    }
}