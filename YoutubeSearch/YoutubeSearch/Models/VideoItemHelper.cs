using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeSearch.Models
{
    public class VideoItemHelper
    {
        public static string cull(string strSource, string strStart, string strEnd)
        {
            int Start, End;

            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);

                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}
