using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/bct")] // Fixed RESTful route
    [ApiController]
    public class BCTController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public BCTController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/bct
        // Retrieve all BCT records
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Bct>>> GetAllBcts()
        {
            var bcts = await _context.Bcts.ToListAsync();
            if (bcts == null || bcts.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy báo cáo thực tập nào." });
            }
            return Ok(bcts);
        }

        // GET: api/bct/{mbct}
        // Retrieve a BCT record by Mbct (report ID)
        [HttpGet("{mbct}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Bct>> GetBctByMbct(string mbct)
        {
            var bct = await _context.Bcts.FindAsync(mbct);
            if (bct == null)
            {
                return NotFound(new { message = $"Không tìm thấy báo cáo thực tập với mã: {mbct}" });
            }
            return Ok(bct);
        }

        // POST: api/bct
        // Create a new BCT record
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bct>> CreateBct([FromBody] Bct bct)
        {
            if (!ModelState.IsValid || bct == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Bcts.AnyAsync(b => b.Mbct == bct.Mbct))
            {
                return BadRequest(new { message = $"Mã báo cáo thực tập {bct.Mbct} đã tồn tại." });
            }

            // Validate Msv exists in Sinhvien table
            if (!await _context.Sinhviens.AnyAsync(s => s.Msv == bct.Msv))
            {
                return BadRequest(new { message = $"Mã sinh viên {bct.Msv} không tồn tại." });
            }

            _context.Bcts.Add(bct);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBctByMbct), new { mbct = bct.Mbct }, bct);
        }

        // PUT: api/bct/{mbct}
        // Update an existing BCT record
        [HttpPut("{mbct}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBct(string mbct, [FromBody] Bct bct)
        {
            if (mbct != bct.Mbct || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã báo cáo không khớp." });
            }

            var existingBct = await _context.Bcts.FindAsync(mbct);
            if (existingBct == null)
            {
                return NotFound(new { message = $"Không tìm thấy báo cáo thực tập với mã: {mbct}" });
            }

            // Validate Msv exists in Sinhvien table
            if (!await _context.Sinhviens.AnyAsync(s => s.Msv == bct.Msv))
            {
                return BadRequest(new { message = $"Mã sinh viên {bct.Msv} không tồn tại." });
            }

            _context.Entry(existingBct).CurrentValues.SetValues(bct);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BctExists(mbct))
                {
                    return NotFound(new { message = $"Không tìm thấy báo cáo thực tập với mã: {mbct}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/bct/{mbct}
        // Delete a BCT record
        [HttpDelete("{mbct}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBct(string mbct)
        {
            var bct = await _context.Bcts.FindAsync(mbct);
            if (bct == null)
            {
                return NotFound(new { message = $"Không tìm thấy báo cáo thực tập với mã: {mbct}" });
            }

            _context.Bcts.Remove(bct);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa báo cáo thực tập với mã: {mbct}" });
        }

        // Helper method to check if a BCT record exists
        private bool BctExists(string mbct)
        {
            return _context.Bcts.Any(e => e.Mbct == mbct);
        }
    }
}