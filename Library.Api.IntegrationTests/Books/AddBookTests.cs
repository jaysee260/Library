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
    public class AddBookTests : IClassFixture<LibraryApiFixture>
    {
        private readonly LibraryApiFixture _fixture;
        private readonly HttpClientWrapper _client;
        private const string Url = "/books";

        public AddBookTests(LibraryApiFixture fixture)
        {
            _fixture = fixture;
            _client = new HttpClientWrapper(fixture.CreateClient());
        }

        [Fact]
        public async Task AddBook_ReturnsOkResponse_WithCreatedBook()
        {
            var bookDto = TestData.BookDto;
            var response = await _client.PostAsync(Url, bookDto);
            var content = response.GetResponseContent<BookDto>();
            
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            content.Should().NotBeNull();
            content.Id.Should().NotBeEmpty();
        }
    }
}