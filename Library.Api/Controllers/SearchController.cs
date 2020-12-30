using System.Threading.Tasks;
using Library.Api.Services;
using Library.Contracts.RestApi;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        
        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]BookSearchCriteria searchCriteria)
        {
            var results = await _searchService.Search(searchCriteria);
            return Ok(results);
        }
    }
}