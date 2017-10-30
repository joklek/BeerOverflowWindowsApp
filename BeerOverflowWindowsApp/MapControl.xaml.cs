using Microsoft.Maps.MapControl.WPF;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Maps.MapControl.WPF.Design;
using System.Windows.Input;
using System.Configuration;
using System;
using BeerOverflowWindowsApp;

namespace BeerOverflowWindowsApp
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        LocationConverter locConverter = new LocationConverter();
        public MapControl()
        {
            InitializeComponent();
            string latitude = ConfigurationManager.AppSettings["defaultLatitude"];
            string longitude = ConfigurationManager.AppSettings["defaultLongitude"];
            Map.Focus();
            Map.Mode = new AerialMode(true);
            Pushpin pin = new Pushpin();
            pin.Location = new Location(Convert.ToDouble(latitude), Convert.ToDouble(longitude));
            Map.Children.Add(pin);
        }

        private void ShowCurrentLocation_Click(object sender, RoutedEventArgs e)
        {
            string[] tagInfo = ((Button)sender).Tag.ToString().Split(' ');
            Location center = (Location)locConverter.ConvertFrom(tagInfo[0]);
            double zoom = System.Convert.ToDouble(tagInfo[1]);
            Map.SetView(center, zoom);
        }

        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            Point mousePosition = e.GetPosition(this);
            Location pinLocation = Map.ViewportPointToLocation(mousePosition);
            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;
            Map.Children.Add(pin);
        }
    }
}
