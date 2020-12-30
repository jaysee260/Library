using System.Threading.Tasks;
using Library.Contracts.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly LibraryDbContext _dbContext;

        public PublisherRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Publisher> GetPublisherByNameAsync(string publisherName)
        {
            var publisher = await _dbContext.Publishers.FirstOrDefaultAsync(p => p.Name.Equals(publisherName));
            return publisher;
        }
    }
}