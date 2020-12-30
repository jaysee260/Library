using System.Threading.Tasks;
using Library.Contracts.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly LibraryDbContext _dbContext;

        public AuthorsRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Author> GetAuthorByNameAsync(string firstName, string lastName)
        {

            var author = await _dbContext.Authors.FirstOrDefaultAsync(a =>
                a.FirstName.ToLower().Equals(firstName.ToLower()) &&
                a.LastName.ToLower().Equals(lastName.ToLower())
            );
            return author;
        }
    }
}