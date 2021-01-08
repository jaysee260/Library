using System;
using System.Collections.Generic;
using Library.Contracts.Common;
using Library.Contracts.DatabaseEntities;
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
        
        public static Book BookEntity => new Book
        {
            Id = Guid.NewGuid(),
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
    }
}