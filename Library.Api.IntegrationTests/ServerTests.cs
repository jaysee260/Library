using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Library.Contracts.Common;
using Library.Contracts.RestApi;
using Newtonsoft.Json;
using Xunit;

namespace Library.Api.IntegrationTests
{
    public class ServerTests : IClassFixture<LibraryApiFixture>
    {
        private readonly LibraryApiFixture _fixture;
        private readonly HttpClient _client;

        public ServerTests(LibraryApiFixture fixture)
        {
            // _server = new TestServer(new WebHostBuilder().UseStartup<Startup>().UseTestServer());
            // _client = _server.CreateClient();
            _fixture = fixture;
            _client = _fixture.CreateClient();
        }

        [Fact]
        public async Task RequestTest()
        {
            var response = await _client.GetAsync("/books");
            var content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<IEnumerable<BookDto>>(content);
            Console.WriteLine("");
        }

        [Fact]
        public async Task CanCreate()
        {
            var book = new BookDto
            {
                Title = "Test Book",
                Authors = new List<AuthorDto>
                {
                    new AuthorDto
                    {
                        FirstName = "Test First Name",
                        LastName = "Test Last Name"
                    }
                },
                ISBN = "123456789",
                PublicationYear = 2020,
                Publisher = new PublisherDto
                {
                    Name = "Test Publisher"
                },
                Location = new LocationDto
                {
                    Type = LocationType.Unshelved,
                    Value = "coffee table"
                },
                Tags = new List<TagDto>
                {
                    new TagDto { Name = "test tag" }
                }
            };

            var serializedPayload = JsonConvert.SerializeObject(book);
            var response = await _client.PostAsync("/books", new StringContent(serializedPayload, Encoding.UTF8, "application/json"));
            Console.WriteLine("");
        }
    }
}