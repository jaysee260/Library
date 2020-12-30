using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IAuthorsRepository _authorsRepository;
        private readonly ITagsRepository _tagsRepository;

        public BooksService(
            IMapper mapper,
            IBooksRepository booksRepository,
            IPublisherRepository publisherRepository,
            IAuthorsRepository authorsRepository,
            ITagsRepository tagsRepository
        )
        {
            _mapper = mapper;
            _booksRepository = booksRepository;
            _publisherRepository = publisherRepository;
            _authorsRepository = authorsRepository;
            _tagsRepository = tagsRepository;
        }
        
        public async Task<BookDto> AddBookAsync(BookDto book)
        {
            var bookEntity = _mapper.Map<Book>(book);
            
            await SetPublisherIfItAlreadyExists(book, bookEntity);
            await SetAnyAuthorsThatMayAlreadyExist(book, bookEntity);
            await SetAnyTagsThatMayAlreadyExist(book, bookEntity);
            
            var createdBook = await _booksRepository.AddBookAsync(bookEntity);
            return _mapper.Map<BookDto>(createdBook);
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

        private async Task SetPublisherIfItAlreadyExists(BookDto bookDto, Book bookEntity)
        {
            if (bookDto.Publisher?.Name == null) return;
            var publisher = await _publisherRepository.GetPublisherByNameAsync(bookDto.Publisher.Name);
            if (publisher == null) return;
            bookEntity.Publisher = publisher;
        }

        private async Task SetAnyAuthorsThatMayAlreadyExist(BookDto bookDto, Book bookEntity)
        {
            if (!bookDto.Authors.Any()) return;
            
            var existingAuthors = new List<Author>();
            var nonExistingAuthors = new List<AuthorDto>();

            foreach (var incomingAuthor in bookDto.Authors)
            {
                var author = await _authorsRepository.GetAuthorByNameAsync(incomingAuthor.FirstName, incomingAuthor.LastName);
                if (author != null)
                {
                    existingAuthors.Add(author);
                }
                else
                {
                    nonExistingAuthors.Add(incomingAuthor);
                }
            }

            if (!existingAuthors.Any()) return;
            // If there's any non existing authors, we want to merge the list of existing and non-existing authors.
            // Otherwise, we can just use the list of existing authors.
            if (nonExistingAuthors.Any())
            {
                var nonExistingAuthorsEntities = _mapper.Map<List<Author>>(nonExistingAuthors);
                bookEntity.Authors = existingAuthors.Concat(nonExistingAuthorsEntities).ToList();
            }
            else
            {
                bookEntity.Authors = existingAuthors;
            }
        }

        private async Task SetAnyTagsThatMayAlreadyExist(BookDto bookDto, Book bookEntity)
        {
            if (!bookDto.Tags.Any()) return;
            var existingTags = new List<Tag>();
            var nonExistingTags = new List<TagDto>();
            
            foreach (var incomingTag in bookDto.Tags)
            {
                var tag = await _tagsRepository.GetTagByNameAsync(incomingTag.Name);
                if (tag != null)
                {
                    existingTags.Add(tag);
                }
                else
                {
                    nonExistingTags.Add(incomingTag);
                }
            }

            if (!existingTags.Any()) return;
            if (nonExistingTags.Any())
            {
                var nonExistingTagsEntities = _mapper.Map<List<Tag>>(nonExistingTags);
                bookEntity.Tags = existingTags.Concat(nonExistingTagsEntities).ToList();
            }
            else
            {
                bookEntity.Tags = existingTags;
            }
        }
    }
}