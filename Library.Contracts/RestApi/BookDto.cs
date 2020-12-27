using System;
using System.Collections.Generic;

namespace Library.Contracts.RestApi
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<AuthorDto> Authors { get; set; }
    }
}