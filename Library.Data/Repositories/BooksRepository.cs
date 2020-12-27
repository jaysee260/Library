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

        public async Task<Book> AddBookAsync(Book book)
        {
            await _dbContext.Books.AddAsync(book);
            await _dbContext.SaveChangesAsync();
            return book;
        }

        public async Task RemoveBookAsync(Guid id)
        {
            var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id.Equals(id));

            if (book != null)
            {
                _dbContext.Books.Remove(book);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _dbContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            // TODO: Handle Not Found
            return await _dbContext.Books.FirstOrDefaultAsync(b => b.Id.Equals(id));
        }
    }
}