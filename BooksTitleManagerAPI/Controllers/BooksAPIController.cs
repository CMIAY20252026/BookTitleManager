using BooksTitleManagerAPI.Data;
using BooksTitleManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksTitleManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksAPIController : ControllerBase
    {
        private readonly BooksTitleManagerContext _context;

        public BooksAPIController(BooksTitleManagerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BooksModelApi>>> GetBooks([FromQuery] string? search)
        {
            IQueryable<BooksModelApi> query = _context.BooksModelApis;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b => b.Title.Contains(search));
            }

            var result = await query.ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BooksModelApi>> AddBook([FromBody] BooksModelApi book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.BooksModelApis.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }
    }

}
