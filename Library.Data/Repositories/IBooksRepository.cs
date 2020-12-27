using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Data.Entities;

namespace Library.Data.Repositories
{
    public interface IBooksRepository
    {
        // TODO: Implement pagination
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> AddBookAsync(Book book);
        Task<Book> GetBookAsync(Guid id);
        Task RemoveBookAsync(Guid id);
        // TODO: Implement Update
    }
}