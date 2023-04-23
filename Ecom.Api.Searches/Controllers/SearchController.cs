using Ecom.Api.Searches.Interfaces;
using Ecom.Api.Searches.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Searches.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService searchService; 

        public SearchController(ISearchService searchService )
        {
            this.searchService = searchService;
        }


        [HttpPost]
        public async Task<IActionResult> SerachAsync(SearchTerm term)
        {
            var result = await this.searchService.SearchAsync(term.CustomerId);
            if(result.isSuccess)
            {
                return Ok(result.SearchResult);
            }
            return NotFound();
        }
    }
}
