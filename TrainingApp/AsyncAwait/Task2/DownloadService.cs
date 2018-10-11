using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncAwait.Task2
{
    static class DownloadService
    {
        private static IDataProvider _provider = new DataProvider();
        public static Task<byte[]> DownloadFile(string url, CancellationToken token)
        {
            return _provider.DownloadData(url, token);
        }

        public static void SaveFile(object state)
        {
            byte[] file = (byte[]) state;
            Task.Run(() => File.WriteAllBytes($"{DateTime.Now.ToFileTime()}", file));
        }

        public static async Task<bool> DownloadAndSave(string url, CancellationToken token)
        {
            byte[] file = await DownloadFile(url, token);
            if (token.IsCancellationRequested)
            {
                return false;
            }
            else
            {
                await Task.Run(() => SaveFile(file));
                return true;
            }
        }
    }
}
