using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/sinhvien")] // Đường dẫn RESTful cố định
    [ApiController]
    public class SinhvienController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public SinhvienController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/sinhvien
        // Lấy danh sách tất cả sinh viên
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Sinhvien>>> GetAllSinhviens()
        {
            var sinhviens = await _context.Sinhviens.ToListAsync();
            if (sinhviens == null || sinhviens.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy sinh viên nào." });
            }
            return Ok(sinhviens);
        }

        // GET: api/sinhvien/{id}
        // Lấy chi tiết một sinh viên theo mã sinh viên
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Sinhvien>> GetSinhvienById(string id)
        {
            var sinhvien = await _context.Sinhviens.FindAsync(id);
            if (sinhvien == null)
            {
                return NotFound(new { message = $"Không tìm thấy sinh viên với mã: {id}" });
            }
            return Ok(sinhvien);
        }

        // POST: api/sinhvien
        // Tạo một sinh viên mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Sinhvien>> CreateSinhvien([FromBody] Sinhvien sinhvien)
        {
            if (!ModelState.IsValid || sinhvien == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Sinhviens.AnyAsync(s => s.Msv == sinhvien.Msv))
            {
                return BadRequest(new { message = $"Mã sinh viên {sinhvien.Msv} đã tồn tại." });
            }

            if (!await _context.Khoas.AnyAsync(k => k.Makhoa == sinhvien.Makhoa))
            {
                return BadRequest(new { message = $"Mã khoa {sinhvien.Makhoa} không tồn tại." });
            }

            _context.Sinhviens.Add(sinhvien);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSinhvienById), new { id = sinhvien.Msv }, sinhvien);
        }

        // PUT: api/sinhvien/{id}
        // Cập nhật thông tin một sinh viên
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSinhvien(string id, [FromBody] Sinhvien sinhvien)
        {
            if (id != sinhvien.Msv || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã sinh viên không khớp." });
            }

            var existingSinhvien = await _context.Sinhviens.FindAsync(id);
            if (existingSinhvien == null)
            {
                return NotFound(new { message = $"Không tìm thấy sinh viên với mã: {id}" });
            }

            if (!await _context.Khoas.AnyAsync(k => k.Makhoa == sinhvien.Makhoa))
            {
                return BadRequest(new { message = $"Mã khoa {sinhvien.Makhoa} không tồn tại." });
            }

            _context.Entry(existingSinhvien).CurrentValues.SetValues(sinhvien);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SinhvienExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy sinh viên với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/sinhvien/{id}
        // Xóa một sinh viên
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSinhvien(string id)
        {
            var sinhvien = await _context.Sinhviens.FindAsync(id);
            if (sinhvien == null)
            {
                return NotFound(new { message = $"Không tìm thấy sinh viên với mã: {id}" });
            }

            _context.Sinhviens.Remove(sinhvien);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa sinh viên với mã: {id}" });
        }

        // Hàm kiểm tra sinh viên có tồn tại không
        private bool SinhvienExists(string id)
        {
            return _context.Sinhviens.Any(e => e.Msv == id);
        }
    }
}