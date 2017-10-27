using Microsoft.Maps.MapControl.WPF;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Maps.MapControl.WPF.Design;
using System.Windows.Input;

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
            Map.Focus();
            Map.Mode = new AerialMode(true);
        }

        private void ChangeMapView_Click(object sender, RoutedEventArgs e)
        {
            // Parse the information of the button's Tag property
            string[] tagInfo = ((Button)sender).Tag.ToString().Split(' ');
            Location center = (Location)locConverter.ConvertFrom(tagInfo[0]);
            double zoom = System.Convert.ToDouble(tagInfo[1]);
            // Set the map view
            Map.SetView(center, zoom);
        }

        public void AddPushpinToMap()
        {
            InitializeComponent();
            //Set focus on mapl
            Map.Focus();
        }

        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Disables the default mouse double-click action.
            e.Handled = true;
            // Determin the location to place the pushpin at on the map.
            //Get the mouse click coordinates
            Point mousePosition = e.GetPosition(this);
            //Convert the mouse coordinates to a locatoin on the map
            Location pinLocation = Map.ViewportPointToLocation(mousePosition);
            // The pushpin to add to the map.
            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;
            // Adds the pushpin to the map.
            Map.Children.Add(pin);
        }
    }
}
