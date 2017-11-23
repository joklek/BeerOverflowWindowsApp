using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflowWindowsApp.DataModels
{
    public class UserAndBarsModel
    {
        public User User { get; set; }
        public List<BarData> Bars { get; set; }
    }
}
