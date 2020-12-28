using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Contracts.RestApi;
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

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await _dbContext.Books
                .AsSplitQuery()
                .Include(b => b.Authors)
                .Include(b => b.Publisher)
                .Include(b => b.Location)
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(b => b.Id.Equals(id));
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

        public async Task<int> GetBooksCountAsync()
        {
            return await _dbContext.Books.CountAsync();
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync(int resultsPerPage, int offset, OrderBy orderBy)
        {
            var orderedQueryable = orderBy == OrderBy.Asc
                ? _dbContext.Books.OrderBy(b => b.Title)
                : _dbContext.Books.OrderByDescending(b => b.Title);

            return await orderedQueryable
                .Include(b => b.Authors)
                .Include(b => b.Publisher)
                .Include(b => b.Location)
                .Include(b => b.Tags)
                .Skip(offset)
                .Take(resultsPerPage)
                .ToListAsync();
        }
    }
}