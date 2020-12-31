using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Api.IntegrationTests.Fixtures;
using Library.Contracts.RestApi;
using Library.Testing.Data;
using Library.Testing.Extensions;
using Library.Testing.Utilities;
using Xunit;

namespace Library.Api.IntegrationTests.Books
{
    [Collection("Library Api Collection")]
    public class GetBookTests
    {
        private readonly LibraryApiFixture _fixture;
        private readonly HttpClientWrapper _client;
        private const string Url = "/books";

        public GetBookTests(LibraryApiFixture fixture)
        {
            _fixture = fixture;
            _client = new HttpClientWrapper(fixture.CreateClient());
        }

        [Fact]
        public async Task GettingABook_ThatExists_ReturnsAMatch()
        {
            // Arrange
            var createBookResponse = await _client.PostAsync(Url, TestData.BookDto);
            createBookResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            var createdBook = createBookResponse.GetResponseContent<BookDto>();
            
            // Act
            var getBookResponse = await _client.GetAsync($"/books/{createdBook.Id}");
            
            // Assert
            var match = getBookResponse.GetResponseContent<BookDto>();
            getBookResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            match.Should().NotBeNull();
            match.Id.Should().Be(createdBook.Id);
        }

        [Fact]
        public async Task GettingABook_ThatDoesntExist_Returns_NotFoundResponse()
        {
            // Arrange
            var randomId = Guid.NewGuid();
            
            // Act
            var response = await _client.GetAsync($"/books/{randomId}");
            
            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }
        
        // TODO: Include GetBooksCount and GetAllBooks tests here
    }
}