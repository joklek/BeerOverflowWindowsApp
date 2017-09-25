using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp
{
    interface IWritable
    {
        void SaveData(BarDataModel barData);
    }
}
