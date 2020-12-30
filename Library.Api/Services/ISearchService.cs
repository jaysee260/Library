using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Contracts.RestApi;

namespace Library.Api.Services
{
    public interface ISearchService
    {
        Task<IEnumerable<BookDto>> Search(BookSearchCriteria searchCriteria);
    }
}