using System.Threading.Tasks;
using Library.Contracts.DatabaseEntities;

namespace Library.Data.Repositories
{
    public interface ITagsRepository
    {
        Task<Tag> GetTagByNameAsync(string tagName);
    }
}