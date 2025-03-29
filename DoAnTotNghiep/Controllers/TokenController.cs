using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DoAnTotNghiep.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Controllers
{
    [Route("api/token")] // Đường dẫn RESTful cố định
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly QlthucTapContext _context;

        // Constructor: Inject DbContext
        public TokenController(QlthucTapContext context)
        {
            _context = context;
        }

        // GET: api/token
        // Lấy danh sách tất cả token
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Token>>> GetAllTokens()
        {
            var tokens = await _context.Tokens.ToListAsync();
            if (tokens == null || tokens.Count == 0)
            {
                return NotFound(new { message = "Không tìm thấy token nào." });
            }
            return Ok(tokens);
        }

        // GET: api/token/{id}
        // Lấy chi tiết một token theo mã token
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Token>> GetTokenById(string id)
        {
            var token = await _context.Tokens.FindAsync(id);
            if (token == null)
            {
                return NotFound(new { message = $"Không tìm thấy token với mã: {id}" });
            }
            return Ok(token);
        }

        // POST: api/token
        // Tạo một token mới
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Token>> CreateToken([FromBody] Token token)
        {
            if (!ModelState.IsValid || token == null)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ.", errors = ModelState });
            }

            if (await _context.Tokens.AnyAsync(t => t.Id == token.Id))
            {
                return BadRequest(new { message = $"Mã token {token.Id} đã tồn tại." });
            }

            _context.Tokens.Add(token);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTokenById), new { id = token.Id }, token);
        }

        // PUT: api/token/{id}
        // Cập nhật thông tin một token
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateToken(string id, [FromBody] Token token)
        {
            if (id != token.Id || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Dữ liệu không hợp lệ hoặc mã token không khớp." });
            }

            var existingToken = await _context.Tokens.FindAsync(id);
            if (existingToken == null)
            {
                return NotFound(new { message = $"Không tìm thấy token với mã: {id}" });
            }

            _context.Entry(existingToken).CurrentValues.SetValues(token);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TokenExists(id))
                {
                    return NotFound(new { message = $"Không tìm thấy token với mã: {id}" });
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/token/{id}
        // Xóa một token
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteToken(string id)
        {
            var token = await _context.Tokens.FindAsync(id);
            if (token == null)
            {
                return NotFound(new { message = $"Không tìm thấy token với mã: {id}" });
            }

            _context.Tokens.Remove(token);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Đã xóa token với mã: {id}" });
        }

        // Hàm kiểm tra token có tồn tại không
        private bool TokenExists(string id)
        {
            return _context.Tokens.Any(e => e.Id == id);
        }
    }
}