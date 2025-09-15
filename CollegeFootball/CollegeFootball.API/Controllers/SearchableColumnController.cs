using CollegeFootball.Domain.Entities;
using CollegeFootball.Domain.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;

namespace CollegeFootball.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SearchableColumnController : ControllerBase
    {
        private readonly ISearchableColumnService scService;

        public SearchableColumnController(ISearchableColumnService _scService)
        {
            scService = _scService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SearchableColumn>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAll()
        {
            try
            {
                var columns = scService.GetAll();
                return Ok(columns);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
