using System;
using System.Threading.Tasks;
using Library.Data.Entities;
using Library.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {
            await _booksRepository.AddBook(book);
            return Ok(book);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var book = await _booksRepository.GetBook(id);
            return book == null ? NotFound() : Ok(book);
        }

    }
}