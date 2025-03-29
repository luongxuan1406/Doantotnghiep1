using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/dnnt")] // Đường dẫn RESTful cố định
    [ApiController]
    public class DnntController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public DnntController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/dnnt
        // Lấy danh sách tất cả doanh nghiệp nhà trường
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Dnnt>>> GetAllDnnts()
        {
            var dnnts = await _context.Dnnts.ToListAsync();
            if (dnnts == null || dnnts.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy doanh nghiệp nhà trường nào." });
            }
            return Ok(dnnts);
        }

        // GET: api/dnnt/{id}
        // Lấy chi tiết một doanh nghiệp nhà trường theo mã doanh nghiệp
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dnnt>> GetDnntById(string id)
        {
            var dnnt = await _context.Dnnts.FindAsync(id);
            if (dnnt == null)
            {
                return NotFound(new { message = $"Không tìm thấy doanh nghiệp nhà trường với mã: {id}" });
            }
            return Ok(dnnt);
        }

        // POST: api/dnnt
        // Tạo một doanh nghiệp nhà trường mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dnnt>> CreateDnnt([FromBody] Dnnt dnnt)
        {
            if (!ModelState.IsValid || dnnt == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Dnnts.AnyAsync(d => d.Mdn == dnnt.Mdn))
            {
                return BadRequest(new { message = $"Mã doanh nghiệp {dnnt.Mdn} đã tồn tại." });
            }

            _context.Dnnts.Add(dnnt);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDnntById), new { id = dnnt.Mdn }, dnnt);
        }

        // PUT: api/dnnt/{id}
        // Cập nhật thông tin một doanh nghiệp nhà trường
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDnnt(string id, [FromBody] Dnnt dnnt)
        {
            if (id != dnnt.Mdn || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã doanh nghiệp không khớp." });
            }

            var existingDnnt = await _context.Dnnts.FindAsync(id);
            if (existingDnnt == null)
            {
                return NotFound(new { message = $"Không tìm thấy doanh nghiệp nhà trường với mã: {id}" });
            }

            _context.Entry(existingDnnt).CurrentValues.SetValues(dnnt);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DnntExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy doanh nghiệp nhà trường với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/dnnt/{id}
        // Xóa một doanh nghiệp nhà trường
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDnnt(string id)
        {
            var dnnt = await _context.Dnnts.FindAsync(id);
            if (dnnt == null)
            {
                return NotFound(new { message = $"Không tìm thấy doanh nghiệp nhà trường với mã: {id}" });
            }

            _context.Dnnts.Remove(dnnt);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa doanh nghiệp nhà trường với mã: {id}" });
        }

        // Hàm kiểm tra doanh nghiệp nhà trường có tồn tại không
        private bool DnntExists(string id)
        {
            return _context.Dnnts.Any(e => e.Mdn == id);
        }
    }
}