using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BooksRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddBook(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBook(Guid id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id.Equals(id));

            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetBook(Guid id)
        {
            // TODO: Handle Not Found
            return await _dbContext.Books.FirstOrDefaultAsync(b => b.Id.Equals(id));
        }
    }
}