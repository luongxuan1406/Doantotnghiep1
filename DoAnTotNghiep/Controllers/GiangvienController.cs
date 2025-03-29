using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/giangvien")] // Đường dẫn RESTful cố định
    [ApiController]
    public class GiangvienController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public GiangvienController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/giangvien
        // Lấy danh sách tất cả giảng viên
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Giangvien>>> GetAllGiangviens()
        {
            var giangviens = await _context.Giangviens.ToListAsync();
            if (giangviens == null || giangviens.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy giảng viên nào." });
            }
            return Ok(giangviens);
        }

        // GET: api/giangvien/{id}
        // Lấy chi tiết một giảng viên theo mã giảng viên
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Giangvien>> GetGiangvienById(string id)
        {
            var giangvien = await _context.Giangviens.FindAsync(id);
            if (giangvien == null)
            {
                return NotFound(new { message = $"Không tìm thấy giảng viên với mã: {id}" });
            }
            return Ok(giangvien);
        }

        // POST: api/giangvien
        // Tạo một giảng viên mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Giangvien>> CreateGiangvien([FromBody] Giangvien giangvien)
        {
            if (!ModelState.IsValid || giangvien == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Giangviens.AnyAsync(g => g.Mgv == giangvien.Mgv))
            {
                return BadRequest(new { message = $"Mã giảng viên {giangvien.Mgv} đã tồn tại." });
            }

            if (!await _context.Khoas.AnyAsync(k => k.Makhoa == giangvien.Makhoa))
            {
                return BadRequest(new { message = $"Mã khoa {giangvien.Makhoa} không tồn tại." });
            }

            _context.Giangviens.Add(giangvien);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGiangvienById), new { id = giangvien.Mgv }, giangvien);
        }

        // PUT: api/giangvien/{id}
        // Cập nhật thông tin một giảng viên
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateGiangvien(string id, [FromBody] Giangvien giangvien)
        {
            if (id != giangvien.Mgv || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã giảng viên không khớp." });
            }

            var existingGiangvien = await _context.Giangviens.FindAsync(id);
            if (existingGiangvien == null)
            {
                return NotFound(new { message = $"Không tìm thấy giảng viên với mã: {id}" });
            }

            if (!await _context.Khoas.AnyAsync(k => k.Makhoa == giangvien.Makhoa))
            {
                return BadRequest(new { message = $"Mã khoa {giangvien.Makhoa} không tồn tại." });
            }

            _context.Entry(existingGiangvien).CurrentValues.SetValues(giangvien);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GiangvienExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy giảng viên với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/giangvien/{id}
        // Xóa một giảng viên
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteGiangvien(string id)
        {
            var giangvien = await _context.Giangviens.FindAsync(id);
            if (giangvien == null)
            {
                return NotFound(new { message = $"Không tìm thấy giảng viên với mã: {id}" });
            }

            _context.Giangviens.Remove(giangvien);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa giảng viên với mã: {id}" });
        }

        // Hàm kiểm tra giảng viên có tồn tại không
        private bool GiangvienExists(string id)
        {
            return _context.Giangviens.Any(e => e.Mgv == id);
        }
    }
}