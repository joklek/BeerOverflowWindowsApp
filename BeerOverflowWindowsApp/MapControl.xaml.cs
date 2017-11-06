using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using System.Windows.Input;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl
    {
        public MapControl()
        {
            InitializeComponent();
            if (CurrentLocation.currentLocation != null)
            {
                var latitude = CurrentLocation.currentLocation.Latitude;
                var longitude = CurrentLocation.currentLocation.Longitude;
                var center = new Location(latitude, longitude);
                var pin = new Pushpin { Location = center };
                Map.Children.Add(pin);
                var zoom = 13.000;
                Map.SetView(center, zoom);
            }

            var BarData = new BarDataModel();

            foreach (var bar in BarData)
            {
                var barPin = new Pushpin
                {
                    Location =
                    {
                        Latitude = bar.Latitude,
                        Longitude = bar.Longitude
                    }
                };
                Map.Children.Add(barPin);
            }
        }

        private void ShowCurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            var latitude = CurrentLocation.currentLocation.Latitude;
            var longitude = CurrentLocation.currentLocation.Longitude;
            var center = new Location(latitude, longitude);
            var zoom = 16.000;
            Map.SetView(center, zoom);
        }

        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            var mousePosition = e.GetPosition(this);
            var pinLocation = Map.ViewportPointToLocation(mousePosition);
            var pin = new Pushpin {Location = pinLocation};
            Map.Children.Add(pin);
        }
    }
}
