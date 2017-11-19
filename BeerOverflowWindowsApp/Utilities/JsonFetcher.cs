using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflowWindowsApp.Utilities
{
    public class JsonFetcher : IHttpFetcher
    {
        public async Task<string> GetHttpStreamAsync(string uri)
        {
            string result = null;
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        public string GetHttpStream(string uri)
        {
            var webClient = new WebClient { Encoding = Encoding.UTF8 };
            var response = webClient.DownloadString(uri);
            return response;
        }
    }
}