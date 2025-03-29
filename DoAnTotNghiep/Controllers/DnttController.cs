using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/dntt")] // Đường dẫn RESTful cố định
    [ApiController]
    public class DnttController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public DnttController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/dntt
        // Lấy danh sách tất cả doanh nghiệp thực tập
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Dntt>>> GetAllDntts()
        {
            var dntts = await _context.Dntts.ToListAsync();
            if (dntts == null || dntts.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy doanh nghiệp thực tập nào." });
            }
            return Ok(dntts);
        }

        // GET: api/dntt/{id}
        // Lấy chi tiết một doanh nghiệp thực tập theo mã doanh nghiệp thực tập
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dntt>> GetDnttById(string id)
        {
            var dntt = await _context.Dntts.FindAsync(id);
            if (dntt == null)
            {
                return NotFound(new { message = $"Không tìm thấy doanh nghiệp thực tập với mã: {id}" });
            }
            return Ok(dntt);
        }

        // POST: api/dntt
        // Tạo một doanh nghiệp thực tập mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dntt>> CreateDntt([FromBody] Dntt dntt)
        {
            if (!ModelState.IsValid || dntt == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Dntts.AnyAsync(d => d.Mdntt == dntt.Mdntt))
            {
                return BadRequest(new { message = $"Mã doanh nghiệp thực tập {dntt.Mdntt} đã tồn tại." });
            }

            _context.Dntts.Add(dntt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDnttById), new { id = dntt.Mdntt }, dntt);
        }

        // PUT: api/dntt/{id}
        // Cập nhật thông tin một doanh nghiệp thực tập
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDntt(string id, [FromBody] Dntt dntt)
        {
            if (id != dntt.Mdntt || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã doanh nghiệp thực tập không khớp." });
            }

            var existingDntt = await _context.Dntts.FindAsync(id);
            if (existingDntt == null)
            {
                return NotFound(new { message = $"Không tìm thấy doanh nghiệp thực tập với mã: {id}" });
            }

            _context.Entry(existingDntt).CurrentValues.SetValues(dntt);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DnttExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy doanh nghiệp thực tập với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/dntt/{id}
        // Xóa một doanh nghiệp thực tập
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDntt(string id)
        {
            var dntt = await _context.Dntts.FindAsync(id);
            if (dntt == null)
            {
                return NotFound(new { message = $"Không tìm thấy doanh nghiệp thực tập với mã: {id}" });
            }

            _context.Dntts.Remove(dntt);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa doanh nghiệp thực tập với mã: {id}" });
        }

        // Hàm kiểm tra doanh nghiệp thực tập có tồn tại không
        private bool DnttExists(string id)
        {
            return _context.Dntts.Any(e => e.Mdntt == id);
        }
    }
}