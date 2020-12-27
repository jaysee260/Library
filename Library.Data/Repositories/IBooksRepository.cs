using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Contracts.RestApi;
using Library.Data.Entities;

namespace Library.Data.Repositories
{
    public interface IBooksRepository
    {
        Task<Book> AddBookAsync(Book book);
        Task<Book> GetBookAsync(Guid id);
        Task RemoveBookAsync(Guid id);
        // TODO: Implement Update
        Task<int> GetBooksCountAsync();
        Task<IEnumerable<Book>> GetAllBooksAsync(int resultsPerPage, int offset, OrderBy orderBy);
    }
}