using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/khoa")] // Đường dẫn RESTful cố định
    [ApiController]
    public class KhoaController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public KhoaController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/khoa
        // Lấy danh sách tất cả khoa
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Khoa>>> GetAllKhoas()
        {
            var khoas = await _context.Khoas.ToListAsync();
            if (khoas == null || khoas.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy khoa nào." });
            }
            return Ok(khoas);
        }

        // GET: api/khoa/{id}
        // Lấy chi tiết một khoa theo mã khoa
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Khoa>> GetKhoaById(string id)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            if (khoa == null)
            {
                return NotFound(new { message = $"Không tìm thấy khoa với mã: {id}" });
            }
            return Ok(khoa);
        }

        // POST: api/khoa
        // Tạo một khoa mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Khoa>> CreateKhoa([FromBody] Khoa khoa)
        {
            if (!ModelState.IsValid || khoa == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Khoas.AnyAsync(k => k.Makhoa == khoa.Makhoa))
            {
                return BadRequest(new { message = $"Mã khoa {khoa.Makhoa} đã tồn tại." });
            }

            _context.Khoas.Add(khoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKhoaById), new { id = khoa.Makhoa }, khoa);
        }

        // PUT: api/khoa/{id}
        // Cập nhật thông tin một khoa
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateKhoa(string id, [FromBody] Khoa khoa)
        {
            if (id != khoa.Makhoa || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã khoa không khớp." });
            }

            var existingKhoa = await _context.Khoas.FindAsync(id);
            if (existingKhoa == null)
            {
                return NotFound(new { message = $"Không tìm thấy khoa với mã: {id}" });
            }

            _context.Entry(existingKhoa).CurrentValues.SetValues(khoa);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhoaExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy khoa với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/khoa/{id}
        // Xóa một khoa
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteKhoa(string id)
        {
            var khoa = await _context.Khoas.FindAsync(id);
            if (khoa == null)
            {
                return NotFound(new { message = $"Không tìm thấy khoa với mã: {id}" });
            }

            _context.Khoas.Remove(khoa);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa khoa với mã: {id}" });
        }

        // Hàm kiểm tra khoa có tồn tại không
        private bool KhoaExists(string id)
        {
            return _context.Khoas.Any(e => e.Makhoa == id);
        }
    }
}