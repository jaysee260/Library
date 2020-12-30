using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Library.Contracts.Common;
using Library.Contracts.RestApi;
using Library.Contracts.DatabaseEntities;
using Library.Data.Repositories;

namespace Library.Api.Services
{
    public class BooksService : IBooksService
    {
        private readonly IMapper _mapper;
        private readonly IBooksRepository _booksRepository;
        private readonly IPublisherRepository _publisherRepository;

        public BooksService(
            IMapper mapper,
            IBooksRepository booksRepository,
            IPublisherRepository publisherRepository
        )
        {
            _mapper = mapper;
            _booksRepository = booksRepository;
            _publisherRepository = publisherRepository;
        }
        
        public async Task<BookDto> AddBookAsync(BookDto book)
        {
            /* TODO Check if:
                - Any of the Authors exist. []
                - The Publisher exists. [x]
                - Any of the Tags exist. []
                
                For any of these that is true, pull the respective matches
                from the DB and set them on the incoming BookDto.
            */
            SetPublisherIfItAlreadyExists(book);
            
            var bookEntity = _mapper.Map<Book>(book);
            var newBook = await _booksRepository.AddBookAsync(bookEntity);
            return _mapper.Map<BookDto>(newBook);
        }

        public async Task<BookDto> GetBookAsync(Guid id)
        {
            var bookEntity = await _booksRepository.GetBookAsync(id);
            var bookDto = _mapper.Map<BookDto>(bookEntity);
            return bookDto;
        }

        public async Task RemoveBookAsync(Guid id)
        {
            await _booksRepository.RemoveBookAsync(id);
        }

        public async Task<int> GetBooksCountAsync()
        {
            return await _booksRepository.GetBooksCountAsync();
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync(int resultsPerPage, int offset, OrderBy orderBy)
        {
            var bookEntities = await _booksRepository.GetAllBooksAsync(resultsPerPage, offset, orderBy);
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(bookEntities);
            return bookDtos;
        }

        private async void SetPublisherIfItAlreadyExists(BookDto book)
        {
            if (book.Publisher?.Name == null) return;
            var publisher = await _publisherRepository.GetPublisherByNameAsync(book.Publisher.Name);
            if (publisher == null) return;
            book.Publisher = _mapper.Map<PublisherDto>(publisher);
        }
    }
}