namespace WebApi.DataModels
{
    public class LocationRequestModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Radius { get; set; }
        public User User { get; set; }
    }
}