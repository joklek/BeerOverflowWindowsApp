using System.Threading.Tasks;

namespace BeerOverflowWindowsApp.Utilities
{
    public interface IHttpFetcher
    {
        Task<string> GetHttpStreamAsync(string uri);
        string GetHttpStream(string uri);
    }
}