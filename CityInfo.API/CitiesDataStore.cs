using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {

        #region Properties

        public List<CityDto> Cities { get; set; }

        public static CitiesDataStore CurrentInstance { get; } = new CitiesDataStore();

        #endregion

        #region Constructor

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto
                {
                    Id = 1,
                    Name = "Tokyo",
                    Description = "Capital city of japan.",
                    PointsOfInterestDto = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto
                        {
                            Id = 1,
                            Name = "Tokyo Tower",
                            Description = "Reminiscent of the Eiffel Tower, this landmark features observation areas & other attractions."
                        },
                        new PointsOfInterestDto
                        {
                            Id = 2,
                            Name = "Tokyo Skytree",
                            Description = "World's tallest freestanding broadcasting tower with an observation deck boasting 360-degree views."
                        }
                    }
                },
                new CityDto
                {
                    Id = 2,
                    Name = "Shanghai",
                    Description = "Economic capital of china.",
                    PointsOfInterestDto = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto
                        {
                            Id = 3,
                            Name = "Yu Garden",
                            Description = "5-acre garden built in 1577 that features Ming dynasty pavilions, ponds, rockeries & arched bridges."
                        }
                    }
                },
                new CityDto
                {
                    Id = 3,
                    Name = "Berlin",
                    Description = "Capitol of germany.",
                    PointsOfInterestDto = new List<PointsOfInterestDto>()
                    {
                        new PointsOfInterestDto
                        {
                            Id = 4,
                            Name = "Brandenberg Gate",
                            Description = "Restored 18th-century gate & landmark with 12 Doric columns topped by a classical goddess statue."
                        }
                    }
                }
            };
        }

        #endregion

    }
}
