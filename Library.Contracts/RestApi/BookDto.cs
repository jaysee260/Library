using System;
using System.Collections.Generic;

namespace Library.Contracts.RestApi
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<AuthorDto> Authors { get; set; }
        public string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public PublisherDto Publisher { get; set; }
        public LocationDto Location { get; set; }
        public IEnumerable<TagDto> Tags { get; set; }
    }
}