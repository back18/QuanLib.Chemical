using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public static class DataBuilder
    {
        public static string BuildElementSymbolEnum(IDictionary<string, ElementContext> elementContexts)
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

        public static string BuildPeriodicTableCsv(IDictionary<string, ElementContext> elementContexts)
        {
            ArgumentNullException.ThrowIfNull(elementContexts, nameof(elementContexts));

            StringBuilder stringBuilder = new();
            stringBuilder.AppendJoin(',', [
                "Symbol",
                "EnglishName",
                "ChineseName",
                "PinYin",
                "AtomicMass",
                "AtomicRadius",
                "Density",
                "MeltingPoint",
                "BoilingPoint",
                "IonizationEnergy",
                "ElectronAffinity",
                "Electronegativity",
                "ElectronConfiguration",
                "OxidationStates",
                "GroupBlock",
                "StandardState",
                "CPKColor",
                "YearDiscovered"
                ]);
            stringBuilder.AppendLine();

            foreach (ElementContext elementContext in elementContexts.Values)
            {
                PubchemElement pubchemElement = elementContext.PubchemElement;
                List<string> items = [];

                items.Add(pubchemElement.Symbol);
                items.Add(pubchemElement.Name);
                items.Add(elementContext.BaikeElementIntroduction.ChineseName);
                items.Add(elementContext.BaikeElementIntroduction.PinYin);
                items.Add(pubchemElement.AtomicMass);
                items.Add(pubchemElement.AtomicRadius);
                items.Add(pubchemElement.Density);
                items.Add(pubchemElement.MeltingPoint);
                items.Add(pubchemElement.BoilingPoint);
                items.Add(pubchemElement.IonizationEnergy);
                items.Add(pubchemElement.ElectronAffinity);
                items.Add(pubchemElement.Electronegativity);
                items.Add(FormatElectronConfiguration(pubchemElement.ElectronConfiguration));
                items.Add($"\"{pubchemElement.OxidationStates}\"");
                items.Add(ToGroupBlock(pubchemElement.GroupBlock).ToString());
                items.Add(pubchemElement.StandardState.Replace("Expected to be a ", string.Empty));
                items.Add(string.IsNullOrEmpty(pubchemElement.CPKHexColor) ? "000000" : pubchemElement.CPKHexColor);
                items.Add(pubchemElement.YearDiscovered == "Ancient" ? "0" : pubchemElement.YearDiscovered);

                stringBuilder.AppendJoin(',', items);
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        private static string FormatElectronConfiguration(string text)
        {
            text = text.Replace("(calculated)", string.Empty).Replace("(predicted)", string.Empty).TrimEnd();
            int index = text.IndexOf(']');
            if (index != -1 && index + 1 < text.Length && text[index + 1] != ' ')
                text = text.Insert(index + 1, " ");
            return text;
        }

        private static GroupBlock ToGroupBlock(string text)
        {
            Dictionary<string, GroupBlock> map = Enum.GetValues<GroupBlock>().ToDictionary(i => i.ToString(), i => i);
            text = text.Replace(" ", string.Empty).Replace("-", string.Empty);
            string? key = map.Keys.Where(i => text.Equals(i, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException($"无法将“{text}”转换为GroupBlock");

            return map[key];
        }
    }
}
