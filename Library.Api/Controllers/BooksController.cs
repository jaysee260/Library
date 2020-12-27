using System;
using System.Threading.Tasks;
using Library.Api.Services;
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
        public async Task<IActionResult> AddBook(BookDto book)
        {
            var newBook = await _booksService.AddBook(book);
            return Ok(newBook);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _booksService.GetBook(id);
            return book == null ? NotFound() : Ok(book);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery]int resultsPerPage = 25, int offset = 0)
        {
            var books = await _booksService.GetAllBooksAsync(resultsPerPage, offset);
            return Ok(books);
        }

        [HttpDelete("{id:Guid}")]
        public async Task RemoveBookAsync(Guid id)
        {
            await _booksService.RemoveBookAsync(id);
        }
    }
}