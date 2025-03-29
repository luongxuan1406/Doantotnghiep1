using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/pcgv")] // Đường dẫn RESTful cố định
    [ApiController]
    public class PcgvController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public PcgvController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/pcgv
        // Lấy danh sách tất cả phân công giảng viên
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Pcgv>>> GetAllPcgvs()
        {
            var pcgvs = await _context.Pcgvs.ToListAsync();
            if (pcgvs == null || pcgvs.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy phân công giảng viên nào." });
            }
            return Ok(pcgvs);
        }

        // GET: api/pcgv/{id}
        // Lấy chi tiết một phân công giảng viên theo mã phân công
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pcgv>> GetPcgvById(string id)
        {
            var pcgv = await _context.Pcgvs.FindAsync(id);
            if (pcgv == null)
            {
                return NotFound(new { message = $"Không tìm thấy phân công giảng viên với mã: {id}" });
            }
            return Ok(pcgv);
        }

        // POST: api/pcgv
        // Tạo một phân công giảng viên mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pcgv>> CreatePcgv([FromBody] Pcgv pcgv)
        {
            if (!ModelState.IsValid || pcgv == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Pcgvs.AnyAsync(p => p.Mpc == pcgv.Mpc))
            {
                return BadRequest(new { message = $"Mã phân công {pcgv.Mpc} đã tồn tại." });
            }

            if (!await _context.Giangviens.AnyAsync(g => g.Mgv == pcgv.Mgv))
            {
                return BadRequest(new { message = $"Mã giảng viên {pcgv.Mgv} không tồn tại." });
            }

            if (pcgv.Mlhp != null && !await _context.Lhps.AnyAsync(l => l.Mlhp == pcgv.Mlhp))
            {
                return BadRequest(new { message = $"Mã lớp học phần {pcgv.Mlhp} không tồn tại." });
            }

            if (pcgv.Mdntt != null && !await _context.Dntts.AnyAsync(d => d.Mdntt == pcgv.Mdntt))
            {
                return BadRequest(new { message = $"Mã doanh nghiệp thực tập {pcgv.Mdntt} không tồn tại." });
            }

            _context.Pcgvs.Add(pcgv);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPcgvById), new { id = pcgv.Mpc }, pcgv);
        }

        // PUT: api/pcgv/{id}
        // Cập nhật thông tin một phân công giảng viên
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdatePcgv(string id, [FromBody] Pcgv pcgv)
        {
            if (id != pcgv.Mpc || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã phân công không khớp." });
            }

            var existingPcgv = await _context.Pcgvs.FindAsync(id);
            if (existingPcgv == null)
            {
                return NotFound(new { message = $"Không tìm thấy phân công giảng viên với mã: {id}" });
            }

            if (!await _context.Giangviens.AnyAsync(g => g.Mgv == pcgv.Mgv))
            {
                return BadRequest(new { message = $"Mã giảng viên {pcgv.Mgv} không tồn tại." });
            }

            if (pcgv.Mlhp != null && !await _context.Lhps.AnyAsync(l => l.Mlhp == pcgv.Mlhp))
            {
                return BadRequest(new { message = $"Mã lớp học phần {pcgv.Mlhp} không tồn tại." });
            }

            if (pcgv.Mdntt != null && !await _context.Dntts.AnyAsync(d => d.Mdntt == pcgv.Mdntt))
            {
                return BadRequest(new { message = $"Mã doanh nghiệp thực tập {pcgv.Mdntt} không tồn tại." });
            }

            _context.Entry(existingPcgv).CurrentValues.SetValues(pcgv);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PcgvExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy phân công giảng viên với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/pcgv/{id}
        // Xóa một phân công giảng viên
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePcgv(string id)
        {
            var pcgv = await _context.Pcgvs.FindAsync(id);
            if (pcgv == null)
            {
                return NotFound(new { message = $"Không tìm thấy phân công giảng viên với mã: {id}" });
            }

            _context.Pcgvs.Remove(pcgv);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa phân công giảng viên với mã: {id}" });
        }

        // Hàm kiểm tra phân công giảng viên có tồn tại không
        private bool PcgvExists(string id)
        {
            return _context.Pcgvs.Any(e => e.Mpc == id);
        }
    }
}