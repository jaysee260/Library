using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Contracts.RestApi;
using Library.Data.Repositories;

namespace Library.Api.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public Task<IEnumerable<BookDto>> Search(BookSearchCriteria searchCriteria)
        {
            throw new System.NotImplementedException();
        }
    }
}