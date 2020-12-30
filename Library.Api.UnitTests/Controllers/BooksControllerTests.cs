using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Api.Controllers;
using Library.Api.Services;
using Library.Contracts.Common;
using Library.Contracts.RestApi;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Library.Api.UnitTests.Controllers
{
    public class BooksControllerTests
    {
        private Mock<IBooksService> MockBooksService { get; } = new Mock<IBooksService>(MockBehavior.Strict);
        private BooksController Controller => new BooksController(MockBooksService.Object);

        [Fact]
        public async Task AddBookAsync_ReturnsOkResponse_WithCreatedBook()
        {
            // Arrange
            var newBookId = Guid.NewGuid();
            
            MockBooksService
                .Setup(x => x.AddBookAsync(It.IsAny<BookDto>()))
                .ReturnsAsync(new BookDto { Id = newBookId });

            // Act
            var rawResponse = await Controller.AddBookAsync(new BookDto());

            // Assert
            rawResponse.Should().NotBeNull().And.BeOfType<OkObjectResult>();

            var okResponse = rawResponse as OkObjectResult;
            okResponse?.Value.Should().BeOfType<BookDto>();
            
            var result = okResponse?.Value as BookDto;
            result?.Id.Should().Be(newBookId);
        }

        [Fact]
        public async Task GetBookAsync_ReturnsOkResponse_WithBookWithMatchingId()
        {
            // Arrange
            var existingBookId = Guid.NewGuid();
            var existingBook = new BookDto {Id = existingBookId};
            
            MockBooksService
                .Setup(x => x.GetBookAsync(It.Is<Guid>(param => param.Equals(existingBookId))))
                .ReturnsAsync(existingBook);

            // Act
            var rawResponse = await Controller.GetBookAsync(existingBookId);

            // Assert
            rawResponse.Should().NotBeNull().And.BeOfType<OkObjectResult>();

            var okResponse = rawResponse as OkObjectResult;
            okResponse?.Value.Should().BeOfType<BookDto>();
            
            var result = okResponse?.Value as BookDto;
            result?.Id.Should().Be(existingBookId);
        }

        [Fact]
        public async Task RemoveBookAsync_ReturnsNoContentResponse_WithNoResponseBody()
        {
            // Arrange
            var existingBookId = Guid.NewGuid();
            MockBooksService
                .Setup(x => x.RemoveBookAsync(It.Is<Guid>(param => param.Equals(existingBookId))))
                .Returns(Task.CompletedTask);
            
            // Act
            var rawResponse = await Controller.RemoveBookAsync(existingBookId);

            // Assert
            rawResponse.Should().NotBeNull().And.BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task GetBooksCountAsync_ReturnsOkResponse_WithBooksCount()
        {
            // Arrange
            var expectedBooksCount = 10;
            MockBooksService
                .Setup(x => x.GetBooksCountAsync())
                .ReturnsAsync(expectedBooksCount);

            // Act
            var rawResponse = await Controller.GetBooksCountAsync();

            // Assert
            rawResponse.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            (rawResponse as OkObjectResult)?.Value.Should().Be(expectedBooksCount);
        }
        
        [Fact]
        public async Task GetAllBooksAsync_ReturnsOkResponse_WithAListOfBookDtos()
        {
            // Arrange
            var mockBooksCollection = Enumerable
                .Range(0, 25)
                .Select(x => new BookDto { Id = Guid.NewGuid() });
            
            MockBooksService
                .Setup(x => x.GetAllBooksAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<OrderBy>()
                ))
                .ReturnsAsync(mockBooksCollection);

            // Act
            var rawResponse = await Controller.GetAllBooksAsync(
                resultsPerPage: 25,
                offset: 0,
                orderBy: OrderBy.Asc
            );

            // Assert
            rawResponse.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResponse = rawResponse as OkObjectResult;
            (okResponse?.Value as IEnumerable<BookDto>).Should().NotBeEmpty();
        }
    }
}