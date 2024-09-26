using CsvHelper;
using Newtonsoft.Json;
using QuanLib.Chemical.CsvParsing;
using QuanLib.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public static class PeriodicTable
    {
        static PeriodicTable()
        {
            _elements = LoadElements();
            _isotopes = LoadIsotopes();
        }

        private static readonly Dictionary<ElementSymbol, Element> _elements;

        private static readonly Dictionary<ElementSymbol, Isotope[]> _isotopes;

        private static Dictionary<ElementSymbol, Element> LoadElements()
        {
            using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("QuanLib.Chemical.Data.PeriodicTable.csv");
            if (stream is null)
                return [];

            using StreamReader streamReader = new(stream);
            using CsvReader csvReader = new(streamReader, CultureInfo.InvariantCulture);
            csvReader.Context.RegisterClassMap<ElementClassMap>();
            var elements = csvReader.GetRecords<Element>();

            Dictionary<ElementSymbol, Element> result = [];
            foreach (Element element in elements)
                result.Add(element.Symbol, element);

            return result;
        }

        private static Dictionary<ElementSymbol, Isotope[]> LoadIsotopes()
        {
            using Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("QuanLib.Chemical.Data.IsotopeTable.json");
            if (stream is null)
                return [];

            return JsonConvert.DeserializeObject<Dictionary<ElementSymbol, Isotope[]>>(stream.ReadAllText()) ?? [];
        }

        public static Element GetElement(ElementSymbol symbol) => _elements[symbol];

        public static Isotope[] GetIsotopes(ElementSymbol symbol)
        {
            Isotope[] isotopes = _isotopes[symbol];
            if (isotopes.Length == 0)
            {
                Element element = _elements[symbol];
                return [new()
                {
                    Symbol = element.Symbol.ToString(),
                    MassNumber = (int)Math.Round(element.AtomicMass),
                    AtomicMass = element.AtomicMass,
                    Abundance = 0
                }];
            }

            return isotopes.Clone<Isotope>();
        }

        public static Element[] GetElements() => _elements.Values.ToArray();
    }
}
