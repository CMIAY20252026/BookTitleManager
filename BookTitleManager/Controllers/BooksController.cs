using BookTitleManager.Models;
using BookTitleManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookTitleManager.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            var books = await _bookService.GetBooksAsync(search);
            ViewBag.Search = search;
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BooksModel book) //ap
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            try
            {
                //throw new UnauthorizedAccessException(); // Force error
                await _bookService.AddBookAsync(book); //ap
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred while adding the book. Please try again.: {ex.Message}");
                return View(book);
            }
        }
    }
}
