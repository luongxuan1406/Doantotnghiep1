using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/dnsv")] // Đường dẫn RESTful cố định
    [ApiController]
    public class DnsvController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public DnsvController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/dnsv
        // Lấy danh sách tất cả doanh nghiệp sinh viên
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Dnsv>>> GetAllDnsvs()
        {
            var dnsvs = await _context.Dnsvs.ToListAsync();
            if (dnsvs == null || dnsvs.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy doanh nghiệp sinh viên nào." });
            }
            return Ok(dnsvs);
        }

        // GET: api/dnsv/{id}
        // Lấy chi tiết một doanh nghiệp sinh viên theo mã doanh nghiệp
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Dnsv>> GetDnsvById(string id)
        {
            var dnsv = await _context.Dnsvs.FindAsync(id);
            if (dnsv == null)
            {
                return NotFound(new { message = $"Không tìm thấy doanh nghiệp sinh viên với mã: {id}" });
            }
            return Ok(dnsv);
        }

        // POST: api/dnsv
        // Tạo một doanh nghiệp sinh viên mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Dnsv>> CreateDnsv([FromBody] Dnsv dnsv)
        {
            if (!ModelState.IsValid || dnsv == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Dnsvs.AnyAsync(d => d.Mdn == dnsv.Mdn))
            {
                return BadRequest(new { message = $"Mã doanh nghiệp {dnsv.Mdn} đã tồn tại." });
            }

            _context.Dnsvs.Add(dnsv);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDnsvById), new { id = dnsv.Mdn }, dnsv);
        }

        // PUT: api/dnsv/{id}
        // Cập nhật thông tin một doanh nghiệp sinh viên
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateDnsv(string id, [FromBody] Dnsv dnsv)
        {
            if (id != dnsv.Mdn || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã doanh nghiệp không khớp." });
            }

            var existingDnsv = await _context.Dnsvs.FindAsync(id);
            if (existingDnsv == null)
            {
                return NotFound(new { message = $"Không tìm thấy doanh nghiệp sinh viên với mã: {id}" });
            }

            _context.Entry(existingDnsv).CurrentValues.SetValues(dnsv);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DnsvExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy doanh nghiệp sinh viên với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/dnsv/{id}
        // Xóa một doanh nghiệp sinh viên
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDnsv(string id)
        {
            var dnsv = await _context.Dnsvs.FindAsync(id);
            if (dnsv == null)
            {
                return NotFound(new { message = $"Không tìm thấy doanh nghiệp sinh viên với mã: {id}" });
            }

            _context.Dnsvs.Remove(dnsv);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa doanh nghiệp sinh viên với mã: {id}" });
        }

        // Hàm kiểm tra doanh nghiệp sinh viên có tồn tại không
        private bool DnsvExists(string id)
        {
            return _context.Dnsvs.Any(e => e.Mdn == id);
        }
    }
}