using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task2
{
    static class UrlsManager
    {
        private static BlockingCollection<string> _urlsForDownload = new BlockingCollection<string>();

        public static void AddUrlToPendingList(string url)
        {
            Task.Run(()=>_urlsForDownload.TryAdd(url));
        }

        public static BlockingCollection<string> Urls => _urlsForDownload;
    }
}
