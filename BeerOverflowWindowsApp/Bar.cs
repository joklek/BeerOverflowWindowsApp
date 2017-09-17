using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflowWindowsApp
{
    class Bar
    {
        private string Name;
        private string Vendor;

        public Bar ()
        {
            Name = "";
            Vendor = "";
        }
        public Bar (string name, string vendor)
        {
            Name = name;
            Vendor = vendor;
        }

        public string GetName ()
        {
            return Name;
        }

        public string GetVendor ()
        {
            return Vendor;
        }

        public void SetName (string name)
        {
            Name = name;
        }

        public void setVendor (string vendor)
        {
            Vendor = vendor;
        }
    }
}
