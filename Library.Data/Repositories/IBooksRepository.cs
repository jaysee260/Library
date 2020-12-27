using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Data.Entities;

namespace Library.Data.Repositories
{
    public interface IBooksRepository
    {
        Task<IEnumerable<Book>> GetAllBooksAsync(int resultsPerPage, int offset);
        Task<Book> AddBookAsync(Book book);
        Task<Book> GetBookAsync(Guid id);
        Task RemoveBookAsync(Guid id);
        // TODO: Implement Update
    }
}