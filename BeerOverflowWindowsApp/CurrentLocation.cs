using System.Device.Location;

namespace BeerOverflowWindowsApp
{
    class CurrentLocation
    {
        private const double latitude = 54.684815;
        private const double longitude = 25.288464;
        public GeoCoordinate currentLocation { get; set; }
        public CurrentLocation()
        {
            GetCurrentLocation();
        }
        public void GetCurrentLocation() //TODO implement
        {
            currentLocation = new GeoCoordinate(latitude, longitude);
        }
    }
}
