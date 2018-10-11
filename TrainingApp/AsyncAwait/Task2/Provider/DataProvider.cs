using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task2
{
    class DataProvider:IDataProvider
    {
        private WebClient _client;
        public DataProvider()
        {
            _client = new WebClient();
        }
        
        public Task<byte[]> DownloadData(string url, CancellationToken token)
        {
            return Task.Run(() => _client.DownloadDataTaskAsync(url), token);
        }
    }
}
