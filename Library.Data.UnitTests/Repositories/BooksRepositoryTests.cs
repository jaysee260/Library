using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Contracts.Common;
using Library.Contracts.DatabaseEntities;
using Library.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Library.Data.UnitTests.Repositories
{
    public class BooksRepositoryTests
    {
        private readonly LibraryDbContext _mockDbContext;
        private readonly IBooksRepository repository;
        public BooksRepositoryTests()
        {
            var mockContextOptions = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: "MockLibraryDatabase")
                .Options;

            _mockDbContext = new LibraryDbContext(mockContextOptions);

            var alpha = "ABCDEFG".ToCharArray();
            var mockSeedData = alpha.Select(letter => new Book {Title = letter.ToString()}).ToList();
            _mockDbContext.Books.AddRange(mockSeedData);
            _mockDbContext.SaveChanges();
            
            repository = new BooksRepository(_mockDbContext);
        }

        [Fact]
        public async Task AddAsync_Returns_Added_Book_With_Id()
        {
            // Arrange
            var book = new Book
            {
                Title = "Test Book",
                Authors = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Test First Name",
                        LastName = "Test Last Name"
                    }
                },
                ISBN = "123456789",
                PublicationYear = 2020,
                Publisher = new Publisher
                {
                    Name = "Test Publisher"
                },
                Location = new Location
                {
                    Type = LocationType.Unshelved,
                    Value = "coffee table"
                },
                Tags = new List<Tag>
                {
                    new Tag { Name = "test tag" }
                }
            };

            // Act
            book.Id.Should().BeEmpty();
            var newBook = await repository.AddBookAsync(book);

            // Assert
            newBook.Id.Should().NotBeEmpty();
            // newBook isn't really a "new" object. it's the same object we pass in,
            // except EF Core sets the Id after it's saved it to the database.
            // We check the hash code only to verify that it's the same object.
            newBook.GetHashCode().Should().Be(book.GetHashCode());
        }

        [Fact]
        public async Task GetBookAsync_Retrieves_A_Book_From_The_Database()
        {
            // Arrange
            var bookId = await _mockDbContext.Books.Select(b => b.Id).FirstAsync();

            // Act
            var match = await repository.GetBookAsync(bookId);

            // Assert
            match.Should().NotBeNull();
            match.Id.Should().Be(bookId);
        }

        [Fact]
        public async Task DeleteBookAsync_Should_Delete_A_Book_From_The_Database()
        {
            // Arrange
            var countBeforeDelete = await _mockDbContext.Books.CountAsync();
            var bookId = await _mockDbContext.Books.Select(b => b.Id).FirstAsync();
            
            // Act
            await repository.RemoveBookAsync(bookId);
            
            // Assert
            var countAfterDelete = await _mockDbContext.Books.CountAsync();
            countAfterDelete.Should().Be(countBeforeDelete - 1);
        }

        [Fact]
        public async Task GetBooksCountAsync_Returns_An_Integer()
        {
            // Arrange
            
            // Act
            var count = await repository.GetBooksCountAsync();
            
            // Assert
            count.Should().BeOfType(typeof(int));
        }

        [Fact]
        public async Task GetAllBooksAsync_Returns_A_List_Of_Books()
        {
            // Arrange
            var expectedCount = await _mockDbContext.Books.CountAsync();
            
            // Act
            var books = await repository.GetAllBooksAsync(
                resultsPerPage: 25,
                offset: 0,
                orderBy: OrderBy.Asc
            );
            
            // Assert
            books.Should().NotBeEmpty().And.ContainItemsAssignableTo<Book>();
            books.Should().HaveCount(expectedCount);
        }
    }
}