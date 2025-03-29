using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/lhp")] // Đường dẫn RESTful cố định
    [ApiController]
    public class LhpController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public LhpController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/lhp
        // Lấy danh sách tất cả lớp học phần
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Lhp>>> GetAllLhps()
        {
            var lhps = await _context.Lhps.ToListAsync();
            if (lhps == null || lhps.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy lớp học phần nào." });
            }
            return Ok(lhps);
        }

        // GET: api/lhp/{id}
        // Lấy chi tiết một lớp học phần theo mã lớp học phần
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Lhp>> GetLhpById(string id)
        {
            var lhp = await _context.Lhps.FindAsync(id);
            if (lhp == null)
            {
                return NotFound(new { message = $"Không tìm thấy lớp học phần với mã: {id}" });
            }
            return Ok(lhp);
        }

        // POST: api/lhp
        // Tạo một lớp học phần mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Lhp>> CreateLhp([FromBody] Lhp lhp)
        {
            if (!ModelState.IsValid || lhp == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Lhps.AnyAsync(l => l.Mlhp == lhp.Mlhp))
            {
                return BadRequest(new { message = $"Mã lớp học phần {lhp.Mlhp} đã tồn tại." });
            }

            if (!await _context.Khoas.AnyAsync(k => k.Makhoa == lhp.Makhoa))
            {
                return BadRequest(new { message = $"Mã khoa {lhp.Makhoa} không tồn tại." });
            }

            _context.Lhps.Add(lhp);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLhpById), new { id = lhp.Mlhp }, lhp);
        }

        // PUT: api/lhp/{id}
        // Cập nhật thông tin một lớp học phần
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLhp(string id, [FromBody] Lhp lhp)
        {
            if (id != lhp.Mlhp || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã lớp học phần không khớp." });
            }

            var existingLhp = await _context.Lhps.FindAsync(id);
            if (existingLhp == null)
            {
                return NotFound(new { message = $"Không tìm thấy lớp học phần với mã: {id}" });
            }

            if (!await _context.Khoas.AnyAsync(k => k.Makhoa == lhp.Makhoa))
            {
                return BadRequest(new { message = $"Mã khoa {lhp.Makhoa} không tồn tại." });
            }

            _context.Entry(existingLhp).CurrentValues.SetValues(lhp);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LhpExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy lớp học phần với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/lhp/{id}
        // Xóa một lớp học phần
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLhp(string id)
        {
            var lhp = await _context.Lhps.FindAsync(id);
            if (lhp == null)
            {
                return NotFound(new { message = $"Không tìm thấy lớp học phần với mã: {id}" });
            }

            _context.Lhps.Remove(lhp);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa lớp học phần với mã: {id}" });
        }

        // Hàm kiểm tra lớp học phần có tồn tại không
        private bool LhpExists(string id)
        {
            return _context.Lhps.Any(e => e.Mlhp == id);
        }
    }
}