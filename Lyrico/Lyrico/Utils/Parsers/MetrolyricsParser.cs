using HtmlAgilityPack;
using Lyrico.Extensions;
using Lyrico.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lyrico.Utils.Parsers
{
    public class MetrolyricsParser : HtmlParser
    {
        
        public MetrolyricsParser()
        {
            BaseUrl = "https://www.metrolyrics.com/printlyric/";
        }


        public override async Task<string> ParseHtml(string artist, string title)
        {
            string html = await GetHtml(BaseUrl + title.ToUrlSafe("-")+ "-lyrics-" + artist.ToUrlSafe("-") + ".html");
            if (string.IsNullOrEmpty(html)) return null;

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//p[contains(@class, 'verse')]");
            List<string> verses = new List<string>();
            foreach(HtmlNode node in nodes)
            {
                string inner = node.InnerText.Trim();
                verses.Add(inner);
            }

            return CleanUp(verses);
        }
    }
}
