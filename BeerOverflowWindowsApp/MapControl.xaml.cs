using Microsoft.Maps.MapControl.WPF;
using System.Windows;
using System.Windows.Input;
using System;

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
            Console.WriteLine(pinLocation);
            Map.Children.Add(pin);
        }

        private void ChangeMapMode_Click(object sender, RoutedEventArgs e)
        {
            if (Map.Mode.ToString() == "Microsoft.Maps.MapControl.WPF.RoadMode")
            {
                Map.Mode = new AerialMode(true);
            }
            else if (Map.Mode.ToString() == "Microsoft.Maps.MapControl.WPF.AerialMode")
            {
                Map.Mode = new RoadMode();
            }
        }

        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            MapWindow close = new MapWindow();
            close.MapWindowForm();
        }
    }
}
