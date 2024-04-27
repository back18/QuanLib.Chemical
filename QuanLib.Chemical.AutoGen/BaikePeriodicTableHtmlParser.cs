using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace QuanLib.Chemical.AutoGen
{
    public partial class BaikePeriodicTableHtmlParser
    {
        public BaikePeriodicTableHtmlParser(string html)
        {
            ArgumentException.ThrowIfNullOrEmpty(html, nameof(html));

            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(html);
            _htmlDocument = htmlDocument;
        }

        private readonly HtmlDocument _htmlDocument;

        public Dictionary<string, BaikeElementIntroduction> GetElementIntroductions()
        {
            Dictionary<string, BaikeElementIntroduction> result = [];
            var table1 = ReadTableText(GetIntroductionTableNode1());
            var table2 = ReadTableText(GetIntroductionTableNode2());

            foreach (var item in table1)
            {
                BaikeElementIntroduction elementIntroduction = new()
                {
                    AtomicNumber = item.Value["原子序数"],
                    Symbol = item.Value["符号"],
                    EnglishName = item.Value["英文名"],
                    ChineseName = item.Value["中文"],
                    PinYin = item.Value["读音"],
                    AtomicMass = item.Value["相对原子质量"],
                    Introduction = item.Value["简介"]
                };

                result.Add(item.Key, elementIntroduction);
            }

            foreach (var item in table2)
            {
                BaikeElementIntroduction elementIntroduction = new()
                {
                    AtomicNumber = item.Value["原子序数"],
                    Symbol = item.Value["符号"],
                    EnglishName = item.Value["英文名"],
                    ChineseName = item.Value["简体中文"],
                    PinYin = item.Value["汉语拼音"],
                    AtomicMass = item.Value["相对原子质量"],
                    Introduction = item.Value["简介"]
                };

                result.Add(item.Key, elementIntroduction);
            }

            return result;
        }

        public Dictionary<string, string> GetElementUrls()
        {
            Dictionary<string, string> result = [];
            Dictionary<string, string> hyperlinks1 = ReadAllHyperlink(GetPeriodicTableNode1());
            Dictionary<string, string> hyperlinks2 = ReadAllHyperlink(GetPeriodicTableNode2());

            foreach (var item in hyperlinks1)
                result.TryAdd(item.Key, "https://baike.baidu.com" + item.Value.Replace("?fromModule=lemma_inlink", string.Empty));
            foreach (var item in hyperlinks2)
                result.TryAdd(item.Key, "https://baike.baidu.com" + item.Value.Replace("?fromModule=lemma_inlink", string.Empty));

            return result;
        }

        private HtmlNode GetIntroductionTableNode1()
        {
            return _htmlDocument.DocumentNode.SelectSingleNode("//table[caption='元素介绍']");
        }

        private HtmlNode GetIntroductionTableNode2()
        {
            return _htmlDocument.DocumentNode.SelectSingleNode("//table[caption='其余人造元素介绍']");
        }

        private HtmlNode GetPeriodicTableNode1()
        {
            return _htmlDocument.DocumentNode.SelectSingleNode("//table[caption='元素周期表']");
        }

        private HtmlNode GetPeriodicTableNode2()
        {
            return _htmlDocument.DocumentNode.SelectSingleNode("//table[caption='镧系和锕系元素']");
        }

        private Dictionary<string, Dictionary<string, string>> ReadTableText(HtmlNode htmlNode)
        {
            ArgumentNullException.ThrowIfNull(htmlNode, nameof(htmlNode));

            Dictionary<string, Dictionary<string, string>> result = [];
            HtmlNodeCollection trNodes = htmlNode.SelectNodes(".//tr");

            if (trNodes is null || trNodes.Count == 0)
                return result;

            string[] tableHead = ReadTableRowText(trNodes[0]);
            for (int i = 1; i < trNodes.Count; i++)
            {
                string[] tableRow = ReadTableRowText(trNodes[i]);
                Dictionary<string, string> items = [];
                for (int j = 0; j < tableRow.Length; j++)
                    items.Add(tableHead[j], tableRow[j]);

                string symbol = tableRow[Array.IndexOf(tableHead, "符号")];
                result.Add(symbol, items);
            }

            return result;
        }

        private static string[] ReadTableRowText(HtmlNode htmlNode)
        {
            ArgumentNullException.ThrowIfNull(htmlNode, nameof(htmlNode));

            List<string> result = [];
            foreach (HtmlNode thNode in htmlNode.SelectNodes(".//th") ?? Enumerable.Empty<HtmlNode>())
                result.Add(TrimReferences(thNode.InnerText));
            foreach (HtmlNode tdNode in htmlNode.SelectNodes(".//td") ?? Enumerable.Empty<HtmlNode>())
                result.Add(TrimReferences(tdNode.InnerText));

            return result.ToArray();
        }

        private static Dictionary<string, string> ReadAllHyperlink(HtmlNode htmlNode)
        {
            ArgumentNullException.ThrowIfNull(htmlNode, nameof(htmlNode));

            Dictionary<string, string> result = [];
            foreach (HtmlNode aNode in htmlNode.SelectNodes(".//a") ?? Enumerable.Empty<HtmlNode>())
            {
                string text = aNode.InnerText;
                string href = aNode.Attributes["href"].Value;
                result.Add(text, href);
            }

            return result;
        }

        private static string TrimReferences(string text)
        {
            ArgumentNullException.ThrowIfNull(text, nameof(text));

            return ReferencesRegex().Replace(text, string.Empty);
        }

        [GeneratedRegex(@"\s*\[(.*?)\]\s*")]
        private static partial Regex ReferencesRegex();
    }
}
