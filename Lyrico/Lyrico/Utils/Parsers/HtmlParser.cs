﻿using Lyrico.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lyrico.Utils.Parsers
{
    public abstract class HtmlParser
    {
        protected string BaseUrl { get; set; }

        public HtmlParser() { }

        protected virtual async Task<string> GetHtml(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    return await client.GetStringAsync(url).ConfigureAwait(false);
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine(e.ToString() + "@" + url);
                }
            }

            return null;
        }

        protected virtual string CleanUp(List<string> parts)
        {
            StringBuilder cleanData = new StringBuilder();
            foreach(string str in parts)
            {
                cleanData.Append(str.Replace("<br>", Environment.NewLine).Trim() + Environment.NewLine + Environment.NewLine);
            }
            return cleanData.ToString();
        }

        public abstract Task<string> ParseHtml(string artist, string title);

    }
}
