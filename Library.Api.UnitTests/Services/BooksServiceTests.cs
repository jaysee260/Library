using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Library.Api.Mapping.Profiles;
using Library.Api.Services;
using Library.Contracts.Common;
using Library.Contracts.RestApi;
using Library.Data.Entities;
using Library.Data.Repositories;
using Moq;
using Xunit;

namespace Library.Api.UnitTests.Services
{

    public class BooksServiceTests
    {
        private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<BooksMappingProfile>()));
        private Mock<IBooksRepository> mockBooksRepository { get; set; } = new Mock<IBooksRepository>(MockBehavior.Strict);
        private IBooksService service => new BooksService(_mapper, mockBooksRepository.Object);
        
        public BooksServiceTests()
        {

        }


        [Fact]
        public async Task AddBookAsync_Returns_Created_Book_With_Id()
        {

            // Arrange
            var newBookId = Guid.NewGuid();
            
            mockBooksRepository
                .Setup(x => x.AddBookAsync(It.IsAny<Book>()))
                .ReturnsAsync(new Book { Id = newBookId });

            // Act
            var result = await service.AddBookAsync(new BookDto());

            // Assert
            result.Should().NotBeNull().And.BeOfType<BookDto>();
            result.Id.Should().Be(newBookId);
        }

        [Fact]
        public async Task GetBookAsync_Returns_Book_With_Matching_Id()
        {
            // Arrange
            var idOfExistingBook = Guid.NewGuid();

            mockBooksRepository
                .Setup(x => x.GetBookAsync(It.Is<Guid>(param => param.Equals(idOfExistingBook))))
                .ReturnsAsync(new Book { Id = idOfExistingBook });

            // Act
            var result = await service.GetBookAsync(idOfExistingBook);

            // Assert
            result.Should().NotBeNull().And.BeOfType<BookDto>();
            result.Id.Should().Be(idOfExistingBook);
        }

        [Fact]
        public async Task DeleteBookAsync_Returns_Nothing()
        {
            // Arrange
            var idOfExistingBook = Guid.NewGuid();

            mockBooksRepository
                .Setup(x => x.RemoveBookAsync(It.Is<Guid>(param => param.Equals(idOfExistingBook))))
                .Returns(Task.CompletedTask);

            // Act
            await service.RemoveBookAsync(idOfExistingBook);

            // Assert
            mockBooksRepository.Verify(x => x.RemoveBookAsync(idOfExistingBook), Times.Once);
        }

        [Fact]
        public async Task GetBooksCountAsync_Returns_Total_Count()
        {
            // Arrange
            var mockBooksCollection = Enumerable
                .Range(0, new Random().Next(minValue: 1, maxValue: 100))
                .Select(x => new Book());

            mockBooksRepository
                .Setup(x => x.GetBooksCountAsync())
                .ReturnsAsync(mockBooksCollection.Count());

            // Act
            var count = await service.GetBooksCountAsync();
            
            // Assert
            count.Should().NotBe(0);
            count.Should().Be(mockBooksCollection.Count());
        }

        [Fact]
        public async Task GetAllBooksAsync_Returns_A_List_of_Books()
        {
            // Arrange
            var collectionSize = new Random().Next(minValue: 1, maxValue: 100);
            var mockBooksCollection = Enumerable.Range(0, collectionSize).Select(x => new Book());

            mockBooksRepository
                .Setup(x => x.GetAllBooksAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<OrderBy>()
                ))
                .ReturnsAsync(mockBooksCollection);

            // Act
            var results = await service.GetAllBooksAsync(resultsPerPage: 25, offset: 0, orderBy: OrderBy.Asc);

            // Assert
            results.Should().NotBeNullOrEmpty().And.ContainItemsAssignableTo<BookDto>();
            results.Should().HaveSameCount(mockBooksCollection);
        }
    }
}