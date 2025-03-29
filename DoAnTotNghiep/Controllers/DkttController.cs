using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/dktt")] // Đường dẫn RESTful cố định
    [ApiController]
    public class DkttController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public DkttController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/dktt
        // Lấy danh sách tất cả đăng ký thực tập
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Dktt>>> GetAllDktts()
        {
            var dktts = await _context.Dktts.ToListAsync();
            if (dktts == null || dktts.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy đăng ký thực tập nào." });
            }
            return Ok(dktts);
        }

        // GET: api/dktt/{id}
        // Lấy chi tiết một đăng ký thực tập theo mã đăng ký
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dktt>> GetDkttById(string id)
        {
            var dktt = await _context.Dktts.FindAsync(id);
            if (dktt == null)
            {
                return NotFound(new { message = $"Không tìm thấy đăng ký thực tập với mã: {id}" });
            }
            return Ok(dktt);
        }

        // POST: api/dktt
        // Tạo một đăng ký thực tập mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dktt>> CreateDktt([FromBody] Dktt dktt)
        {
            if (!ModelState.IsValid || dktt == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Dktts.AnyAsync(d => d.Mdk == dktt.Mdk))
            {
                return BadRequest(new { message = $"Mã đăng ký {dktt.Mdk} đã tồn tại." });
            }

            _context.Dktts.Add(dktt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDkttById), new { id = dktt.Mdk }, dktt);
        }

        // PUT: api/dktt/{id}
        // Cập nhật thông tin một đăng ký thực tập
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDktt(string id, [FromBody] Dktt dktt)
        {
            if (id != dktt.Mdk || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã đăng ký không khớp." });
            }

            var existingDktt = await _context.Dktts.FindAsync(id);
            if (existingDktt == null)
            {
                return NotFound(new { message = $"Không tìm thấy đăng ký thực tập với mã: {id}" });
            }

            _context.Entry(existingDktt).CurrentValues.SetValues(dktt);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DkttExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy đăng ký thực tập với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/dktt/{id}
        // Xóa một đăng ký thực tập
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDktt(string id)
        {
            var dktt = await _context.Dktts.FindAsync(id);
            if (dktt == null)
            {
                return NotFound(new { message = $"Không tìm thấy đăng ký thực tập với mã: {id}" });
            }

            _context.Dktts.Remove(dktt);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa đăng ký thực tập với mã: {id}" });
        }

        // Hàm kiểm tra đăng ký thực tập có tồn tại không
        private bool DkttExists(string id)
        {
            return _context.Dktts.Any(e => e.Mdk == id);
        }
    }
}