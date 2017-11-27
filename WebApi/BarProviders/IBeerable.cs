using System.Collections.Generic;
using System.Threading.Tasks;
using BarData = WebApi.DataModels.BarData;

namespace WebApi.BarProviders
{
    interface IBeerable
    {
        string ProviderName { get; }
        List<BarData> GetBarsAround(double latitude, double longitude, double radius);
        Task<List<BarData>> GetBarsAroundAsync(double latitude, double longitude, double radius);
    }
}
