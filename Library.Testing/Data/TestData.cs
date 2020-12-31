using System.Collections.Generic;
using Library.Contracts.Common;
using Library.Contracts.RestApi;

namespace Library.Testing.Data
{
    public static class TestData
    {
        public static BookDto BookDto => new BookDto
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
    }
}