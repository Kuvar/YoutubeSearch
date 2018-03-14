using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeSearch.Models
{
    public class VideoInformation
    {
        public VideoInformation()
        {
            this.IsSelected = false;
        }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Duration { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public bool IsSelected { get; set; } 
    }
}
