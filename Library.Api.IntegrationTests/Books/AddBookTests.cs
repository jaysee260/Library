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
    public class AddBookTests
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
        public async Task AddingANewBook_ReturnsOkResponse_WithCreatedBook()
        {
            var bookDto = TestData.BookDto;
            var response = await _client.PostAsync(Url, bookDto);
            var content = response.GetResponseContent<BookDto>();
            
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            content.Should().NotBeNull();
            content.Id.Should().NotBeEmpty();
        }

        [Fact(Skip = "Because can't test until there's an Authors endpoint")]
        public async Task AddingABook_WithAnExistingAuthor_DoesNotDuplicateTheAuthor()
        {
            
        }
        
        [Fact(Skip = "Because can't test until there's a Publishers endpoint")]
        public async Task AddingABook_WithAnExistingPublisher_DoesNotDuplicateThePublisher()
        {
            
        }
        
        [Fact(Skip = "Because can't test until there's a Tags endpoint")]
        public async Task AddingABook_WithAnExistingTag_DoesNotDuplicateTheTag()
        {
            
        }
    }
}