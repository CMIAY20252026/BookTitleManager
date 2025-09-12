using BooksTitleManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BooksTitleManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksAPIController : ControllerBase
    {
        private static readonly List<BooksAPIModel> books = new()
    {
        new BooksAPIModel { Id = 1, Title = "The Pragmatic Programmer" },
        new BooksAPIModel { Id = 2, Title = "Clean Code" },
        new BooksAPIModel { Id = 3, Title = "Design Patterns" }
    };

        [HttpGet]
        public ActionResult<IEnumerable<BooksAPIModel>> GetBooks([FromQuery] string? search)
        {
            var result = books.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(b => b.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
            return Ok(result.ToList());
        }

        [HttpPost]
        public ActionResult<BooksAPIModel> AddBook([FromBody] BooksAPIModel book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            book.Id = books.Max(b => b.Id) + 1;
            books.Add(book);

            return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
        }
    }

}
