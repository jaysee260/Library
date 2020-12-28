using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Contracts.DatabaseEntities
{
    [Table("Tag")]
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}