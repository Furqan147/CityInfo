using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsOfInterestsController : ControllerBase
    {

        #region Private Fields

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<PointsOfInterestsController> _logger;

        #endregion

        #region

        public PointsOfInterestsController(IHttpContextAccessor contextAccessor, ILogger<PointsOfInterestsController> logger)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region Public Action Methods

        /// <summary>
        /// Get city by city id.
        /// </summary>
        /// <param name="cityId">City id</param>
        /// <returns>City</returns>
        [HttpGet("{cityId:int}")]
        public ActionResult GetPointsOfInterestByCityId(int cityId)
        {
            _logger.LogDebug($"fetching city by [CityId] : [{cityId}], for request : [{_contextAccessor.HttpContext?.TraceIdentifier}]");

            var city = CitiesDataStore.CurrentInstance.Cities.Where(c => c.Id == cityId)
                                                                         .ToList();
            if(city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        /// <summary>
        /// Get point of interest in a city by point of interest id.
        /// </summary>
        /// <param name="cityId">City id</param>
        /// <param name="pointOfInterestId">Point of interest id.</param>
        /// <returns>Point of interest</returns>
        [HttpGet("{pointOfInterestId}")]
        public ActionResult<PointsOfInterestDto> GetPointOfInterestByCityIdAndPointOfInterestId(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.CurrentInstance.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointsOfInterests = city.PointsOfInterestDto.FirstOrDefault(p => p.Id == pointOfInterestId);

            if(pointsOfInterests == null)
            {
                return NotFound();
            }

            return Ok(pointsOfInterests);
        }
        #endregion

    }
}
