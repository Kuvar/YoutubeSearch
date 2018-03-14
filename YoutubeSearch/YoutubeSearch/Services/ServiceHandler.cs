namespace YoutubeSearch.Services
{
    using System;
    using ModernHttpClient;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using YoutubeSearch.Models;
    using System.Net;
    using System.Text.RegularExpressions;

    public class ServiceHandler
    {
        static HttpClient Client = new HttpClient(new NativeMessageHandler());
        static WebClient webclient = new WebClient();

        public static async Task<T> GetAjaxAsync<T>(string url)
        {
            T item = default(T);
            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri(url)));
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                item = JsonConvert.DeserializeObject<T>(json);
            }
            return item;
        }

        public static async Task<List<VideoInformation>> SearchQuery(string querystring, int querypages)
        {
            List<VideoInformation>  items = new List<VideoInformation>();
            string title;
            string author;
            string description;
            string duration;
            string url;
            string thumbnail;

            for (int i = 1; i <= querypages; i++)
            {
                Uri uri = new Uri("https://www.youtube.com/results?search_query=" + querystring + "&page=" + i);
                // Search address
                string html = await webclient.DownloadStringTaskAsync(uri);
                
                // Search string
                string pattern = "<div class=\"yt-lockup-content\">.*?title=\"(?<NAME>.*?)\".*?</div></div></div></li>";
                MatchCollection result = Regex.Matches(html, pattern, RegexOptions.Singleline);

                for (int ctr = 0; ctr <= result.Count - 1; ctr++)
                {
                    // Title
                    title = result[ctr].Groups[1].Value;

                    // Author
                    author = VideoItemHelper.cull(result[ctr].Value, "/user/", "class").Replace('"', ' ').TrimStart().TrimEnd();

                    // Description
                    description = VideoItemHelper.cull(result[ctr].Value, "dir=\"ltr\" class=\"yt-uix-redirect-link\">", "</div>");

                    // Duration
                    duration = VideoItemHelper.cull(VideoItemHelper.cull(result[ctr].Value, "id=\"description-id-", "span"), ": ", "<").Replace(".", "");

                    // Url
                    url = string.Concat("http://www.youtube.com/watch?v=", VideoItemHelper.cull(result[ctr].Value, "watch?v=", "\""));

                    // Thumbnail
                    thumbnail = "https://i.ytimg.com/vi/" + VideoItemHelper.cull(result[ctr].Value, "watch?v=", "\"") + "/mqdefault.jpg";

                    // Remove playlists
                    if (title != "__title__")
                    {
                        if (duration != "")
                        {
                            // Add item to list
                            items.Add(new VideoInformation() { Title = title, Author = author, Description = description, Duration = duration, Url = url, Thumbnail = thumbnail, });
                        }
                    }
                }

            }

            return items;
        }
    }
}
