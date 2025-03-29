using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/bctk")] // Fixed RESTful route
    [ApiController]
    public class BctkController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public BctkController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/bctk
        // Retrieve all BCTK records
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Bctk>>> GetAllBctks()
        {
            var bctks = await _context.Bctks.ToListAsync();
            if (bctks == null || bctks.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy báo cáo tổng kết nào." });
            }
            return Ok(bctks);
        }

        // GET: api/bctk/{mbctk}
        // Retrieve a BCTK record by Mbctk (report ID)
        [HttpGet("{mbctk}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Bctk>> GetBctkByMbctk(string mbctk)
        {
            var bctk = await _context.Bctks.FindAsync(mbctk);
            if (bctk == null)
            {
                return NotFound(new { message = $"Không tìm thấy báo cáo tổng kết với mã: {mbctk}" });
            }
            return Ok(bctk);
        }

        // POST: api/bctk
        // Create a new BCTK record
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bctk>> CreateBctk([FromBody] Bctk bctk)
        {
            if (!ModelState.IsValid || bctk == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Bctks.AnyAsync(b => b.Mbctk == bctk.Mbctk))
            {
                return BadRequest(new { message = $"Mã báo cáo tổng kết {bctk.Mbctk} đã tồn tại." });
            }

            // Validate Msv exists in Sinhvien table
            if (!await _context.Sinhviens.AnyAsync(s => s.Msv == bctk.Msv))
            {
                return BadRequest(new { message = $"Mã sinh viên {bctk.Msv} không tồn tại." });
            }

            _context.Bctks.Add(bctk);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBctkByMbctk), new { mbctk = bctk.Mbctk }, bctk);
        }

        // PUT: api/bctk/{mbctk}
        // Update an existing BCTK record
        [HttpPut("{mbctk}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBctk(string mbctk, [FromBody] Bctk bctk)
        {
            if (mbctk != bctk.Mbctk || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã báo cáo không khớp." });
            }

            var existingBctk = await _context.Bctks.FindAsync(mbctk);
            if (existingBctk == null)
            {
                return NotFound(new { message = $"Không tìm thấy báo cáo tổng kết với mã: {mbctk}" });
            }

            // Validate Msv exists in Sinhvien table
            if (!await _context.Sinhviens.AnyAsync(s => s.Msv == bctk.Msv))
            {
                return BadRequest(new { message = $"Mã sinh viên {bctk.Msv} không tồn tại." });
            }

            _context.Entry(existingBctk).CurrentValues.SetValues(bctk);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BctkExists(mbctk))
                {
                    return NotFound(new { message = $"Không tìm thấy báo cáo tổng kết với mã: {mbctk}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/bctk/{mbctk}
        // Delete a BCTK record
        [HttpDelete("{mbctk}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBctk(string mbctk)
        {
            var bctk = await _context.Bctks.FindAsync(mbctk);
            if (bctk == null)
            {
                return NotFound(new { message = $"Không tìm thấy báo cáo tổng kết với mã: {mbctk}" });
            }

            _context.Bctks.Remove(bctk);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa báo cáo tổng kết với mã: {mbctk}" });
        }

        // Helper method to check if a BCTK record exists
        private bool BctkExists(string mbctk)
        {
            return _context.Bctks.Any(e => e.Mbctk == mbctk);
        }
    }
}