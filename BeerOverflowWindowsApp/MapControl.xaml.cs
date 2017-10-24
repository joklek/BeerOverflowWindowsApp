using Microsoft.Maps.MapControl.WPF;
using System.Windows.Controls;


namespace BeerOverflowWindowsApp
{
    /// <summary>
    /// Interaction logic for MapControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        public MapControl()
        {
            InitializeComponent();
            Map.Mode = new AerialMode(true);
        }
    }
}
