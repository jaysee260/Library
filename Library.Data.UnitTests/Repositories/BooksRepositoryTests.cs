using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Contracts.Common;
using Library.Contracts.DatabaseEntities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Library.Data.UnitTests.Repositories
{
    public class BooksRepositoryTests
    {
        private readonly LibraryDbContext _mockDbContext;
        public BooksRepositoryTests()
        {
            var mockContextOptions = new DbContextOptionsBuilder<LibraryDbContext>()
                .UseInMemoryDatabase(databaseName: "MockLibraryDatabase")
                .Options;

            _mockDbContext = new LibraryDbContext(mockContextOptions);
        }

        [Fact]
        public async Task DbTest()
        {
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

            book.Id.Should().BeEmpty();

            await _mockDbContext.Books.AddAsync(book);

            // book.Id.Should().NotBe
        }
    }
}