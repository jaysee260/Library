using System;
using System.Threading.Tasks;
using Library.Api.Services;
using Library.Contracts.Common;
using Library.Contracts.RestApi;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync(BookDto book)
        {
            var newBook = await _booksService.AddBookAsync(book);
            return Ok(newBook);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBookAsync(Guid id)
        {
            var book = await _booksService.GetBookAsync(id);
            
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> RemoveBookAsync(Guid id)
        {
            await _booksService.RemoveBookAsync(id);
            return NoContent();
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetBooksCountAsync()
        {
            var booksCount = await _booksService.GetBooksCountAsync();
            return Ok(booksCount);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync(
            [FromQuery]int resultsPerPage = 25,
            [FromQuery]int offset = 0,
            [FromQuery]OrderBy orderBy = OrderBy.Asc
        )
        {
            var books = await _booksService.GetAllBooksAsync(resultsPerPage, offset, orderBy);
            return Ok(books);
        }
    }
}