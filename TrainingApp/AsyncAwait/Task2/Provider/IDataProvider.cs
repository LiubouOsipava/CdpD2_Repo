using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait.Task2
{
    interface IDataProvider
    {
        Task<byte[]> DownloadData(string url, CancellationToken token);

    }
}
