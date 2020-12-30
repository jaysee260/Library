using System.Threading.Tasks;
using Library.Contracts.DatabaseEntities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly LibraryDbContext _dbContext;

        public TagsRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Tag> GetTagByNameAsync(string tagName)
        {
            var tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Name.Equals(tagName));
            return tag;
        }
    }
}