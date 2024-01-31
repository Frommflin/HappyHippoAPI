using HappyHippoAPI.Data;
using HappyHippoAPI.DTO;
using HappyHippoAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HappyHippoAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class HappyHippoQuotesController : ControllerBase
    {
        private readonly DataContext _context;

        public HappyHippoQuotesController(DataContext context)
        {
            _context = context;
        }

        // POST: HappyHippoQuotes/getquotes/user
        [HttpPost("getquotes/{user}")]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes(string user)
        {
            var quotes = _context.Quotes.Where(x => x.UserId == user);

            if (quotes == null)
            {
                return BadRequest();
            }

            await quotes.ToListAsync();
            return Ok(quotes);
        }

        // POST: HappyHippoQuotes/addquote
        [HttpPost("addquote")]
        public async Task<ActionResult<Quote>> PostQuote(QuoteRequest quote)
        {
            var newQuote = new Quote
            {
                QuoteText = quote.QuoteText,
                Author = quote.Author,
                UserId = quote.UserId
            };


            await _context.Quotes.AddAsync(newQuote);
            await _context.SaveChangesAsync();

            return Ok(newQuote);
        }

        // GET: HappyHippoQuotes/getquote/5
        [HttpGet("getquote/{id}")]
        public async Task<ActionResult<Quote>> GetQuote(int id)
        {
            var quote = await _context.Quotes.FirstOrDefaultAsync(x => x.Id == id);

            if (quote == null)
            {
                return NotFound();
            }

            return Ok(quote);
        }

        // PUT: HappyHippoQuotes/edit/5
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutQuote([FromRoute] int id, Quote quote)
        {
            if (id != quote.Id)
            {
                return BadRequest();
            }

            var existingQuote = await _context.Quotes.FirstOrDefaultAsync(x => x.Id == id);

            if (existingQuote == null)
            {
                return NotFound();
            }

            existingQuote.QuoteText = quote.QuoteText;
            existingQuote.Author = quote.Author;

            await _context.SaveChangesAsync();

            return Ok(existingQuote);
        }

        // DELETE: HappyHippoQuotes/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteQuote(int id)
        {
            var quote = await _context.Quotes.FirstOrDefaultAsync(x => x.Id == id);
            if (quote == null)
            {
                return NotFound();
            }

            _context.Quotes.Remove(quote);
            await _context.SaveChangesAsync();

            return Ok(quote);
        }
    }
}
