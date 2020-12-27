using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Data.Entities
{
    [Table("Book")]
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<Author> Authors { get; set; }
    }
}