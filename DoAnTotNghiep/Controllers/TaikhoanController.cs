using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/taikhoan")] // Đường dẫn RESTful cố định
    [ApiController]
    public class TaikhoanController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public TaikhoanController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/taikhoan
        // Lấy danh sách tất cả tài khoản
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Taikhoan>>> GetAllTaikhoans()
        {
            var taikhoans = await _context.Taikhoans.ToListAsync();
            if (taikhoans == null || taikhoans.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy tài khoản nào." });
            }
            return Ok(taikhoans);
        }

        // GET: api/taikhoan/{id}
        // Lấy chi tiết một tài khoản theo mã tài khoản
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Taikhoan>> GetTaikhoanById(string id)
        {
            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return NotFound(new { message = $"Không tìm thấy tài khoản với mã: {id}" });
            }
            return Ok(taikhoan);
        }

        // POST: api/taikhoan
        // Tạo một tài khoản mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Taikhoan>> CreateTaikhoan([FromBody] Taikhoan taikhoan)
        {
            if (!ModelState.IsValid || taikhoan == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Taikhoans.AnyAsync(t => t.Mtk == taikhoan.Mtk))
            {
                return BadRequest(new { message = $"Mã tài khoản {taikhoan.Mtk} đã tồn tại." });
            }

            if (await _context.Taikhoans.AnyAsync(t => t.Username == taikhoan.Username))
            {
                return BadRequest(new { message = $"Tên đăng nhập {taikhoan.Username} đã tồn tại." });
            }

            _context.Taikhoans.Add(taikhoan);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaikhoanById), new { id = taikhoan.Mtk }, taikhoan);
        }

        // PUT: api/taikhoan/{id}
        // Cập nhật thông tin một tài khoản
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTaikhoan(string id, [FromBody] Taikhoan taikhoan)
        {
            if (id != taikhoan.Mtk || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã tài khoản không khớp." });
            }

            var existingTaikhoan = await _context.Taikhoans.FindAsync(id);
            if (existingTaikhoan == null)
            {
                return NotFound(new { message = $"Không tìm thấy tài khoản với mã: {id}" });
            }

            // Kiểm tra nếu Username thay đổi và đã tồn tại trong hệ thống
            if (existingTaikhoan.Username != taikhoan.Username &&
                await _context.Taikhoans.AnyAsync(t => t.Username == taikhoan.Username))
            {
                return BadRequest(new { message = $"Tên đăng nhập {taikhoan.Username} đã tồn tại." });
            }

            _context.Entry(existingTaikhoan).CurrentValues.SetValues(taikhoan);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaikhoanExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy tài khoản với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/taikhoan/{id}
        // Xóa một tài khoản
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTaikhoan(string id)
        {
            var taikhoan = await _context.Taikhoans.FindAsync(id);
            if (taikhoan == null)
            {
                return NotFound(new { message = $"Không tìm thấy tài khoản với mã: {id}" });
            }

            _context.Taikhoans.Remove(taikhoan);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa tài khoản với mã: {id}" });
        }

        // Hàm kiểm tra tài khoản có tồn tại không
        private bool TaikhoanExists(string id)
        {
            return _context.Taikhoans.Any(e => e.Mtk == id);
        }
    }
}