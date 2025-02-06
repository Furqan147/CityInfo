namespace CityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int NoOfPointsOfInterest 
        { 
            get 
            {
                return PointsOfInterestDto.Count;
            } 
        }

        public ICollection<PointsOfInterestDto> PointsOfInterestDto { get; set; } = new List<PointsOfInterestDto>();
    }
}
