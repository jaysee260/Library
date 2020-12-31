using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Contracts.RestApi;
using Library.Testing.Data;
using Library.Testing.Extensions;
using Library.Testing.Utilities;
using Xunit;

namespace Library.Api.IntegrationTests.Books
{
    public class DeleteBookTests : IClassFixture<LibraryApiFixture>
    {
        private readonly LibraryApiFixture _fixture;
        private readonly HttpClientWrapper _client;
        private const string Url = "/books";

        public DeleteBookTests(LibraryApiFixture fixture)
        {
            _fixture = fixture;
            _client = new HttpClientWrapper(fixture.CreateClient());
        }

        [Fact]
        public async Task DeletingABook_ThatExists_ReturnsNoContentResponse()
        {
            // Arrange
            var createBookResponse = await _client.PostAsync(Url, TestData.BookDto);
            createBookResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            var createdBook = createBookResponse.GetResponseContent<BookDto>();
            
            // Act
            var deleteResponse = await _client.DeleteAsync($"/books/{createdBook.Id}");
            
            // Assert
            deleteResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NoContent);
        }
        
        [Fact]
        public async Task DeletingABook_ThatDoesntExists_ReturnsNoContentResponse()
        {
            // Arrange
            var randomId = Guid.NewGuid();
            
            // Act
            var deleteResponse = await _client.DeleteAsync($"/books/{randomId}");
            
            // Assert
            deleteResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NoContent);
        }
    }
}