using HtmlAgilityPack;
using QuanLib.Chemical.Extensions;
using QuanLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            FileHelper.DownloadAllFileAsync().Wait();
            Dictionary<string, ElementContext> elementContexts = GetElementContexts();

            Release(DataBuilder.BuildElementSymbolEnum(elementContexts), "ElementSymbol.cs", FileHelper.OutDirectory, "..\\..\\..\\..\\QuanLib.Chemical");
            Release(DataBuilder.BuildPeriodicTableCsv(elementContexts), "PeriodicTable.csv", FileHelper.OutDirectory, "..\\..\\..\\..\\QuanLib.Chemical\\Data");

            Console.ReadLine();

            Element[] elements = PeriodicTable.GetElements();
            foreach (Element element in elements)
            {
                Console.Clear();
                Console.WriteLine(element.GetPropertiesInfo());
                Console.ReadLine();
            }
        }

        public static void Release(string content, string fileName, string outPath, string targetPath)
        {
            ArgumentException.ThrowIfNullOrEmpty(content, nameof(content));
            ArgumentException.ThrowIfNullOrEmpty(fileName, nameof(fileName));
            ArgumentException.ThrowIfNullOrEmpty(outPath, nameof(outPath));
            ArgumentException.ThrowIfNullOrEmpty(targetPath, nameof(targetPath));

            string outFile = Path.Combine(outPath, fileName);
            string targetFile = Path.Combine(targetPath, fileName);
            string outContent = File.Exists(outFile) ? File.ReadAllText(outFile, Encoding.UTF8) : string.Empty;
            string targetContent = File.Exists(targetFile) ? File.ReadAllText(targetFile, Encoding.UTF8) : string.Empty;

            Console.WriteLine("已构建文件：" + fileName);
            Console.WriteLine("是否与上一次输出的文件一致：" + StringComparer.Ordinal.Equals(content, outContent));
            Console.WriteLine("是否与目标文件一致：" + StringComparer.Ordinal.Equals(content, targetContent));

            if (!Directory.Exists(outPath))
                Directory.CreateDirectory(outPath);
            File.WriteAllText(outFile, content, Encoding.UTF8);

            Console.WriteLine("文件已保存到" + outFile);
            Console.WriteLine();
        }

        public static Dictionary<string, ElementContext> GetElementContexts()
        {
            Dictionary<string, ElementContext> result = [];
            Dictionary<string, PubchemElement> pubchemElements = GetPubchemElements();
            Dictionary<string, BaikeElementIntroduction> baikeElementIntroductions = GetBaikeElementIntroductions();
            Dictionary<string, BaikeElement> baikeElements = GetBaikeElements(pubchemElements.Keys.ToArray());

            foreach (string symbols in pubchemElements.Keys)
                result.Add(symbols, new(pubchemElements[symbols], baikeElementIntroductions[symbols], baikeElements[symbols]));

            return result;
        }

        public static Dictionary<string, PubchemElement> GetPubchemElements()
        {
            return new PubchemPeriodicTableCsvParser(FileHelper.ReadPubchemPeriodicTableCsv()).GetElements();
        }

        public static Dictionary<string, BaikeElementIntroduction> GetBaikeElementIntroductions()
        {
            return new BaikePeriodicTableHtmlParser(FileHelper.ReadBaikePeriodicTableHtml()).GetElementIntroductions();
        }

        public static Dictionary<string, BaikeElement> GetBaikeElements(string[] symbols)
        {
            ArgumentNullException.ThrowIfNull(symbols, nameof(symbols));

            Dictionary<string, BaikeElement> result = [];
            foreach (var item in FileHelper.ReadAllBaikeElementHtml(symbols))
                result.Add(item.Key, new BaikeElementHtmlParser(item.Value).GetElement());

            return result;
        }
    }
}
