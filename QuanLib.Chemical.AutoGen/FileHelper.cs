using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public static class FileHelper
    {
        public const string BaikePeriodicTableUrl = "https://baike.baidu.com/item/元素周期表/282048";

        public static readonly string PubchemPeriodicTableUrl = "https://pubchem.ncbi.nlm.nih.gov/rest/pug/periodictable/csv";

        public static readonly string ResourcesDirectory = "resources";

        public static readonly string DownloadsDirectory = Path.Combine(ResourcesDirectory, "downloads");

        public static readonly string OutDirectory = Path.Combine(ResourcesDirectory, "out");

        public static readonly string PubchemPeriodicTableCsv = Path.Combine(DownloadsDirectory, "PubchemPeriodicTable.csv");

        public static readonly string BaikePeriodicTableHtml = Path.Combine(DownloadsDirectory, "BaikePeriodicTable.html");

        public static readonly string BaikeElementHtmlFormat = Path.Combine(DownloadsDirectory, "{0}.html");

        public static async Task DownloadAllFileAsync()
        {
            CheckDirectory();

            string pubchemPeriodicTableCsv;
            if (!File.Exists(PubchemPeriodicTableCsv))
                pubchemPeriodicTableCsv = await DownloadPubchemPeriodicTableCsvAsync();
            else
                pubchemPeriodicTableCsv = await ReadPubchemPeriodicTableCsvAsync();

            string baikePeriodicTableHtml;
            if (!File.Exists(BaikePeriodicTableHtml))
                baikePeriodicTableHtml = await DownloadBaikePeriodicTableHtmlAsync();
            else
                baikePeriodicTableHtml = await ReadBaikePeriodicTableHtmlAsync();

            BaikePeriodicTableHtmlParser baikePeriodicTableHtmlParser = new(baikePeriodicTableHtml);
            Dictionary<string, BaikePeriodicTableItem> baikePeriodicTableItems = baikePeriodicTableHtmlParser.GetPeriodicTableItems();
            Dictionary<string, string> baikeElementUrls = baikePeriodicTableHtmlParser.GetElementUrls();

            foreach (BaikePeriodicTableItem periodicTableItem in baikePeriodicTableItems.Values)
            {
                string url = baikeElementUrls[periodicTableItem.ChineseName];
                string path = string.Format(BaikeElementHtmlFormat, periodicTableItem.Symbol);

                if (!File.Exists(path))
                {
                    while (true)
                    {
                        string html = await DownloadTextAsync(url, path);
                        HtmlDocument htmlDocument = new();
                        htmlDocument.LoadHtml(html);

                        if (htmlDocument.DocumentNode.SelectSingleNode("//title").InnerText.Contains("百度百科-验证"))
                        {
                            File.Delete(path);
                            Console.WriteLine("网页需要验证！前往浏览器验证完成后，键入回车继续下载");
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = url,
                                UseShellExecute = true
                            });

                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) ;
                            continue;
                        }

                        Thread.Sleep(1000);
                        break;
                    }
                }
            }
        }

        public static async Task<string> DownloadPubchemPeriodicTableCsvAsync()
        {
            CheckDirectory();
            return await DownloadTextAsync(PubchemPeriodicTableUrl, PubchemPeriodicTableCsv);
        }

        public static async Task<string> DownloadBaikePeriodicTableHtmlAsync()
        {
            CheckDirectory();
            return await DownloadTextAsync(BaikePeriodicTableUrl, BaikePeriodicTableHtml);
        }

        public static async Task<string> DownloadBaikeElementHtmlAsync(string symbol)
        {
            string periodicTableHtml;
            if (!File.Exists(BaikePeriodicTableHtml))
                periodicTableHtml = await DownloadBaikePeriodicTableHtmlAsync();
            else
                periodicTableHtml = await ReadBaikePeriodicTableHtmlAsync();

            BaikePeriodicTableHtmlParser periodicTableHtmlParser = new(periodicTableHtml);
            Dictionary<string, BaikePeriodicTableItem> periodicTableItems = periodicTableHtmlParser.GetPeriodicTableItems();
            Dictionary<string, string> elementUrls = periodicTableHtmlParser.GetElementUrls();

            BaikePeriodicTableItem periodicTableItem = periodicTableItems[symbol];
            return await DownloadTextAsync(elementUrls[periodicTableItem.ChineseName], string.Format(BaikeElementHtmlFormat, periodicTableItem.Symbol));
        }

        public static async Task<string> DownloadTextAsync(string url, string path)
        {
            ArgumentException.ThrowIfNullOrEmpty(url, nameof(url));
            ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));

            HttpClient httpClient = new();
            string html = await httpClient.GetStringAsync(url);
            await File.WriteAllTextAsync(path, html, Encoding.UTF8);
            Console.WriteLine("Downloaded: " + url);
            return html;
        }

        public static string ReadPubchemPeriodicTableCsv()
        {
            return File.ReadAllText(PubchemPeriodicTableCsv, Encoding.UTF8);
        }

        public static async Task<string> ReadPubchemPeriodicTableCsvAsync()
        {
            return await File.ReadAllTextAsync(PubchemPeriodicTableCsv, Encoding.UTF8);
        }

        public static string ReadBaikePeriodicTableHtml()
        {
            return File.ReadAllText(BaikePeriodicTableHtml, Encoding.UTF8);
        }

        public static async Task<string> ReadBaikePeriodicTableHtmlAsync()
        {
            return await File.ReadAllTextAsync(BaikePeriodicTableHtml, Encoding.UTF8);
        }

        public static string ReadBaikeElementHtml(string symbols)
        {
            return File.ReadAllText(string.Format(BaikeElementHtmlFormat, symbols), Encoding.UTF8);
        }

        public static async Task<string> ReadBaikeElementHtmlAsync(string symbols)
        {
            return await File.ReadAllTextAsync(string.Format(BaikeElementHtmlFormat, symbols), Encoding.UTF8);
        }

        public static Dictionary<string, string> ReadAllBaikeElementHtml(string[] symbols)
        {
            ArgumentNullException.ThrowIfNull(symbols, nameof(symbols));

            Dictionary<string, string> result = [];
            foreach (string symbol in symbols)
                result.Add(symbol, ReadBaikeElementHtml(symbol));

            return result;
        }

        public static async Task<Dictionary<string, string>> ReadAllBaikeElementHtmlAsync(string[] symbols)
        {
            ArgumentNullException.ThrowIfNull(symbols, nameof(symbols));

            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string symbol in symbols)
                result.Add(symbol, await ReadBaikeElementHtmlAsync(symbol));

            return result;
        }

        private static void CheckDirectory()
        {
            if (!Directory.Exists(ResourcesDirectory))
                Directory.CreateDirectory(ResourcesDirectory);

            if (!Directory.Exists(DownloadsDirectory))
                Directory.CreateDirectory(DownloadsDirectory);
        }
    }
}
