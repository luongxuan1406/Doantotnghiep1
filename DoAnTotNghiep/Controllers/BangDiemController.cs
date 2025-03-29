using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/bangdiem")] // Fixed RESTful route
    [ApiController]
    public class BangDiemController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public BangDiemController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/bangdiem
        // Retrieve all grade records
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Bangdiem>>> GetAllBangDiems()
        {
            var bangDiems = await _context.Bangdiems.ToListAsync();
            if (bangDiems == null || bangDiems.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy bảng điểm nào." });
            }
            return Ok(bangDiems);
        }

        // GET: api/bangdiem/{msv}
        // Retrieve a grade record by student ID (Msv)
        [HttpGet("{msv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Bangdiem>> GetBangDiemByMsv(string msv)
        {
            var bangDiem = await _context.Bangdiems.FindAsync(msv);
            if (bangDiem == null)
            {
                return NotFound(new { message = $"Không tìm thấy bảng điểm với mã sinh viên: {msv}" });
            }
            return Ok(bangDiem);
        }

        // POST: api/bangdiem
        // Create a new grade record
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bangdiem>> CreateBangDiem([FromBody] Bangdiem bangDiem)
        {
            if (!ModelState.IsValid || bangDiem == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Bangdiems.AnyAsync(b => b.Msv == bangDiem.Msv))
            {
                return BadRequest(new { message = $"Mã sinh viên {bangDiem.Msv} đã có bảng điểm." });
            }

            _context.Bangdiems.Add(bangDiem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBangDiemByMsv), new { msv = bangDiem.Msv }, bangDiem);
        }

        // PUT: api/bangdiem/{msv}
        // Update an existing grade record
        [HttpPut("{msv}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBangDiem(string msv, [FromBody] Bangdiem bangDiem)
        {
            if (msv != bangDiem.Msv || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã sinh viên không khớp." });
            }

            var existingBangDiem = await _context.Bangdiems.FindAsync(msv);
            if (existingBangDiem == null)
            {
                return NotFound(new { message = $"Không tìm thấy bảng điểm với mã sinh viên: {msv}" });
            }

            _context.Entry(existingBangDiem).CurrentValues.SetValues(bangDiem);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BangDiemExists(msv))
                {
                    return NotFound(new { message = $"Không tìm thấy bảng điểm với mã sinh viên: {msv}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/bangdiem/{msv}
        // Delete a grade record
        [HttpDelete("{msv}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBangDiem(string msv)
        {
            var bangDiem = await _context.Bangdiems.FindAsync(msv);
            if (bangDiem == null)
            {
                return NotFound(new { message = $"Không tìm thấy bảng điểm với mã sinh viên: {msv}" });
            }

            _context.Bangdiems.Remove(bangDiem);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa bảng điểm với mã sinh viên: {msv}" });
        }

        // Helper method to check if a grade record exists
        private bool BangDiemExists(string msv)
        {
            return _context.Bangdiems.Any(e => e.Msv == msv);
        }
    }
}