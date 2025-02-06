using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {

        #region Private Fields

        private readonly ILogger<CitiesController> _logger;
        private readonly IHttpContextAccessor _httpContext;

        #endregion

        #region Constructor

        public CitiesController(ILogger<CitiesController> logger, IHttpContextAccessor httpContext)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        #endregion

        #region Public Action Methods

        /// <summary>
        /// Gets all cities.
        /// </summary>
        /// <returns>List of cities</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetAllCitiesAsync()
        {
            var cities = CitiesDataStore.CurrentInstance.Cities;

            if(cities == null)
            {
                _logger.LogDebug($"Cities collection is null, [Request Id] : [{_httpContext.HttpContext?.TraceIdentifier}]");

                return NotFound();
            }

            return Ok(cities);
        }

        /// <summary>
        /// Get city by id.
        /// </summary>
        /// <param name="id">City id</param>
        /// <returns>City</returns>
        [HttpGet("{id:int}")]
        public ActionResult<CityDto> GetCityById([FromRoute] int id)
        {
            var city = CitiesDataStore.CurrentInstance.Cities.FirstOrDefault(c => c.Id == id);

            if(city == null)
            {
                _logger.LogDebug($"City with [Id] : [{id}], not found for , [Request Id] : [{_httpContext.HttpContext?.TraceIdentifier}]");

                return NotFound();
            }

            return Ok(city);
        }

        #endregion


    }
}
