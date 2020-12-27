using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Library.Contracts.RestApi;
using Library.Data.Entities;
using Library.Data.Repositories;

namespace Library.Api.Services
{
    public class BooksService : IBooksService
    {
        private readonly IMapper _mapper;
        private IBooksRepository _booksRepository;

        public BooksService(
            IMapper mapper,
            IBooksRepository booksRepository
        )
        {
            _mapper = mapper;
            _booksRepository = booksRepository;
        }
        
        public async Task<BookDto> AddBook(BookDto book)
        {
            var bookEntity = _mapper.Map<Book>(book);
            var newBook = await _booksRepository.AddBookAsync(bookEntity);
            return _mapper.Map<BookDto>(newBook);
        }

        public async Task<BookDto> GetBook(Guid id)
        {
            var bookEntity = await _booksRepository.GetBookAsync(id);
            var bookDto = _mapper.Map<BookDto>(bookEntity);
            return bookDto;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(int resultsPerPage, int offset)
        {
            var bookEntities = await _booksRepository.GetAllBooksAsync(resultsPerPage, offset);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(bookEntities);
            return bookDtos;
        }

        public async Task RemoveBookAsync(Guid id)
        {
            await _booksRepository.RemoveBookAsync(id);
        }
    }
}