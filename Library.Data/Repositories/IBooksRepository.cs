using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Data.Entities;

namespace Library.Data.Repositories
{
    public interface IBooksRepository
    {
        // TODO: Implement pagination
        Task<IEnumerable<Book>> GetAllBooks();
        void AddBook(Book book);
        Task<Book> GetBook(Guid id);
        void DeleteBook(Guid id);
        // TODO: Implement Update
    }
}