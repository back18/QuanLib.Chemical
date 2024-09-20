using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public static class DataBuilder
    {
        private static readonly string[] _droupBlocks = [
            "AlkaliMetal",
            "AlkalineEarthMetal",
            "TransitionMetal",
            "PostTransitionMetal",
            "Metalloid",
            "NonMetal",
            "Halogen",
            "NobleGas",
            "Lanthanide",
            "Actinide"];

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
                stringBuilder.AppendFormat("        /// {0} ({1})", elementContext.BaikePeriodicTableItem.ChineseName, elementContext.PubchemPeriodicTableItem.Name);
                stringBuilder.AppendLine();
                stringBuilder.AppendLine("        /// </summary>");
                stringBuilder.AppendFormat("        {0} = {1},", elementContext.PubchemPeriodicTableItem.Symbol, elementContext.PubchemPeriodicTableItem.AtomicNumber);
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
                PubchemPeriodicTableItem pubchemPeriodicTableItem = elementContext.PubchemPeriodicTableItem;
                List<string> items = [];

                items.Add(pubchemPeriodicTableItem.Symbol);
                items.Add(pubchemPeriodicTableItem.Name);
                items.Add(elementContext.BaikePeriodicTableItem.ChineseName);
                items.Add(elementContext.BaikePeriodicTableItem.PinYin);
                items.Add(pubchemPeriodicTableItem.AtomicMass);
                items.Add(pubchemPeriodicTableItem.AtomicRadius);
                items.Add(pubchemPeriodicTableItem.Density);
                items.Add(pubchemPeriodicTableItem.MeltingPoint);
                items.Add(pubchemPeriodicTableItem.BoilingPoint);
                items.Add(pubchemPeriodicTableItem.IonizationEnergy);
                items.Add(pubchemPeriodicTableItem.ElectronAffinity);
                items.Add(pubchemPeriodicTableItem.Electronegativity);
                items.Add(FormatElectronConfiguration(pubchemPeriodicTableItem.ElectronConfiguration));
                items.Add($"\"{pubchemPeriodicTableItem.OxidationStates}\"");
                items.Add(ToGroupBlock(pubchemPeriodicTableItem.GroupBlock));
                items.Add(pubchemPeriodicTableItem.StandardState.Replace("Expected to be a ", string.Empty));
                items.Add(string.IsNullOrEmpty(pubchemPeriodicTableItem.CPKHexColor) ? "000000" : pubchemPeriodicTableItem.CPKHexColor);
                items.Add(pubchemPeriodicTableItem.YearDiscovered == "Ancient" ? "0" : pubchemPeriodicTableItem.YearDiscovered);

                stringBuilder.AppendJoin(',', items);
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public static string BuildIsotopeTableJson(IDictionary<string, ElementContext> elementContexts)
        {
            ArgumentNullException.ThrowIfNull(elementContexts, nameof(elementContexts));

            Dictionary<string, ReadOnlyCollection<PubchemIsotope>> table = elementContexts.ToDictionary(i => i.Key, i => i.Value.PubchemElementInfo.Isotopes);
            return JsonConvert.SerializeObject(table, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }

        private static string FormatElectronConfiguration(string text)
        {
            text = text.Replace("(calculated)", string.Empty).Replace("(predicted)", string.Empty).TrimEnd();
            int index = text.IndexOf(']');
            if (index != -1 && index + 1 < text.Length && text[index + 1] != ' ')
                text = text.Insert(index + 1, " ");
            return text;
        }

        private static string ToGroupBlock(string text)
        {
            text = text.Replace(" ", string.Empty).Replace("-", string.Empty);
            string? key = _droupBlocks.Where(i => text.Equals(i, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException($"无法将“{text}”转换为GroupBlock");

            return key;
        }
    }
}
