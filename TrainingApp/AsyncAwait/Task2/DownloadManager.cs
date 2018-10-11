using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace AsyncAwait.Task2
{
    public partial class DownloadManager : Form
    {
        private  CancellationTokenSource cts = new CancellationTokenSource();
        public DownloadManager()
        {
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            UrlsManager.AddUrlToPendingList(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.Write("Started");
            CancellationToken token = cts.Token;
            while (UrlsManager.Urls.TryTake(out string urlItem, -1))
            {
                DownloadService.DownloadAndSave(urlItem, token);
            }
            Debug.Write("Finished");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
}
