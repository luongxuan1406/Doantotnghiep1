using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/cbk")] // Fixed RESTful route
    [ApiController]
    public class CbkController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public CbkController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/cbk
        // Retrieve all CBK records
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Cbk>>> GetAllCbks()
        {
            var cbks = await _context.Cbks.ToListAsync();
            if (cbks == null || cbks.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy cán bộ khoa nào." });
            }
            return Ok(cbks);
        }

        // GET: api/cbk/{mcbk}
        // Retrieve a CBK record by Mcbk (staff ID)
        [HttpGet("{mcbk}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Cbk>> GetCbkByMcbk(string mcbk)
        {
            var cbk = await _context.Cbks.FindAsync(mcbk);
            if (cbk == null)
            {
                return NotFound(new { message = $"Không tìm thấy cán bộ khoa với mã: {mcbk}" });
            }
            return Ok(cbk);
        }

        // POST: api/cbk
        // Create a new CBK record
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Cbk>> CreateCbk([FromBody] Cbk cbk)
        {
            if (!ModelState.IsValid || cbk == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Cbks.AnyAsync(c => c.Mcbk == cbk.Mcbk))
            {
                return BadRequest(new { message = $"Mã cán bộ khoa {cbk.Mcbk} đã tồn tại." });
            }

            // Validate Makhoa exists in Khoa table
            if (!await _context.Khoas.AnyAsync(k => k.Makhoa == cbk.Makhoa))
            {
                return BadRequest(new { message = $"Mã khoa {cbk.Makhoa} không tồn tại." });
            }

            _context.Cbks.Add(cbk);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCbkByMcbk), new { mcbk = cbk.Mcbk }, cbk);
        }

        // PUT: api/cbk/{mcbk}
        // Update an existing CBK record
        [HttpPut("{mcbk}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCbk(string mcbk, [FromBody] Cbk cbk)
        {
            if (mcbk != cbk.Mcbk || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã cán bộ khoa không khớp." });
            }

            var existingCbk = await _context.Cbks.FindAsync(mcbk);
            if (existingCbk == null)
            {
                return NotFound(new { message = $"Không tìm thấy cán bộ khoa với mã: {mcbk}" });
            }

            // Validate Makhoa exists in Khoa table
            if (!await _context.Khoas.AnyAsync(k => k.Makhoa == cbk.Makhoa))
            {
                return BadRequest(new { message = $"Mã khoa {cbk.Makhoa} không tồn tại." });
            }

            _context.Entry(existingCbk).CurrentValues.SetValues(cbk);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CbkExists(mcbk))
                {
                    return NotFound(new { message = $"Không tìm thấy cán bộ khoa với mã: {mcbk}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/cbk/{mcbk}
        // Delete a CBK record
        [HttpDelete("{mcbk}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCbk(string mcbk)
        {
            var cbk = await _context.Cbks.FindAsync(mcbk);
            if (cbk == null)
            {
                return NotFound(new { message = $"Không tìm thấy cán bộ khoa với mã: {mcbk}" });
            }

            _context.Cbks.Remove(cbk);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa cán bộ khoa với mã: {mcbk}" });
        }

        // Helper method to check if a CBK record exists
        private bool CbkExists(string mcbk)
        {
            return _context.Cbks.Any(e => e.Mcbk == mcbk);
        }
    }
}