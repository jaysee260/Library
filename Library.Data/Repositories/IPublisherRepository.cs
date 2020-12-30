using System.Threading.Tasks;
using Library.Contracts.DatabaseEntities;

namespace Library.Data.Repositories
{
    public interface IPublisherRepository
    {
        Task<Publisher> GetPublisherByNameAsync(string publisherName);
    }
}