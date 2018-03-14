using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeSearch.Models
{
    public class YouTubeMp3
    {
        HttpClient Client = new HttpClient();
        private WebClient downloadMp3 = new WebClient();

        public long bytesDownloaded = 0;
        public long totalBytes = 0;

        public YouTubeMp3()
        {
            
        }

        public async Task DownloadMP3(string youtubeUrl, string directory)
        {
            try
            {
                downloadMp3.DownloadProgressChanged += downloadMp3_DownloadProgressChanged;
                downloadMp3.DownloadFileCompleted += downloadMp3_DownloadFileCompleted;

                downloadMp3.DownloadFileAsync(new Uri(string.Format(youtubeUrl)), directory);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void downloadMp3_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            downloadMp3.Dispose();
        }

        private void downloadMp3_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            bytesDownloaded = e.BytesReceived;
            totalBytes = e.TotalBytesToReceive;
        }
    }
}
