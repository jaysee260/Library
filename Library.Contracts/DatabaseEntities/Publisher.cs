using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Contracts.DatabaseEntities
{
    [Table("Publisher")]
    public class Publisher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}