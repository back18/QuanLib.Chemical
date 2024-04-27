using HtmlAgilityPack;
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

            File.WriteAllText(Path.Combine(FileHelper.ResourcesDirectory, "ElementSymbol.cs"), BuildElementSymbolEnum(elementContexts));
        }

        public static string BuildElementSymbolEnum(Dictionary<string, ElementContext> elementContexts)
        {
            ArgumentNullException.ThrowIfNull(elementContexts, nameof(elementContexts));

            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine("using System;");
            stringBuilder.AppendLine("using System.Collections.Generic;");
            stringBuilder.AppendLine("using System.Linq;");
            stringBuilder.AppendLine("using System.Text;");
            stringBuilder.AppendLine("using System.Threading.Tasks;");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine("namespace QuanLib.Chemical");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("    public enum ElementSymbol");
            stringBuilder.AppendLine("    {");

            List<string> fields = [];
            foreach (ElementContext elementContext in elementContexts.Values)
            {
                stringBuilder.AppendLine("        /// <summary>");
                stringBuilder.AppendFormat("        /// {0} ({1})", elementContext.BaikeElementIntroduction.ChineseName, elementContext.PubchemElement.Name);
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("        /// </summary>");
                stringBuilder.AppendFormat("        {0} = {1},", elementContext.PubchemElement.Symbol, elementContext.PubchemElement.AtomicNumber);
                stringBuilder.AppendLine();
                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");

            return stringBuilder.ToString();
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
