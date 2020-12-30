using System.Threading.Tasks;
using Library.Contracts.DatabaseEntities;

namespace Library.Data.Repositories
{
    public interface IAuthorsRepository
    {
        Task<Author> GetAuthorByNameAsync(string firstName, string lastName);
    }
}