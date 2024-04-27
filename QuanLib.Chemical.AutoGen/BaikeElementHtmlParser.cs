using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public partial class BaikeElementHtmlParser
    {
        public BaikeElementHtmlParser(string html)
        {
            ArgumentException.ThrowIfNullOrEmpty(html, nameof(html));

            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(html);
            _htmlDocument = htmlDocument;
        }

        private readonly HtmlDocument _htmlDocument;

        public BaikeElement GetElement()
        {
            return new()
            {
                Name = GetName(),
                Description = GetDescription(),
                Propertys = GetPropertys().AsReadOnly()
            };
        }

        public string GetName()
        {
            HtmlNode h1Node = _htmlDocument.DocumentNode.SelectSingleNode("//h1[contains(@class, 'J-lemma-title')]");
            return h1Node.InnerText;
        }

        public string GetDescription()
        {
            HtmlNode divNode = _htmlDocument.DocumentNode.SelectSingleNode("//div[contains(@class, 'J-summary')]");
            return ReferencesRegex().Replace(divNode.InnerText, string.Empty);
        }

        public Dictionary<string, string> GetPropertys()
        {
            Dictionary<string, string> result = [];
            HtmlNodeCollection divNodes = _htmlDocument.DocumentNode.SelectNodes("//div[contains(@class, 'itemWrapper')]");

            foreach (var divNode in divNodes)
            {
                HtmlNode dtNode = divNode.SelectSingleNode("dt");
                HtmlNode ddNode = divNode.SelectSingleNode("dd");

                if (dtNode is null || ddNode is null)
                    continue;

                result.Add(dtNode.InnerText.Replace("&nbsp;", string.Empty), ReferencesRegex().Replace(ddNode.InnerText, string.Empty));
            }

            return result;
        }

        [GeneratedRegex(@"\s*\[(.*?)\]\s*")]
        private static partial Regex ReferencesRegex();
    }
}
