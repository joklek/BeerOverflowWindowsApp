namespace BeerOverflowWindowsApp.DataModels
{
    public class BarData
    {
        public string BarId { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public float AvgRating { get; set; }
        public CategoryTypes Categories { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public double DistanceToCurrentLocation { get; set; }
        public int UserRating { get; set; }
    }
}
