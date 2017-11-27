using System.Threading.Tasks;

namespace WebApi.Utilities
{
    public interface IHttpFetcher
    {
        Task<string> GetHttpStreamAsync(string uri);
        string GetHttpStream(string uri);
    }
}