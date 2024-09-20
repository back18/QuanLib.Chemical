using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public class PubchemPeriodicTableCsvParser
    {
        public PubchemPeriodicTableCsvParser(string csv)
        {
            ArgumentException.ThrowIfNullOrEmpty(csv, nameof(csv));

            using StringReader stringReader = new StringReader(csv);
            using CsvReader csvReader = new(stringReader, CultureInfo.InvariantCulture);
            _items = csvReader.GetRecords<PubchemPeriodicTableItem>().ToDictionary(item => item.Symbol, item => item);
        }

        private readonly Dictionary<string, PubchemPeriodicTableItem> _items;

        public Dictionary<string, PubchemPeriodicTableItem> GetPeriodicTableItems()
        {
            return new Dictionary<string, PubchemPeriodicTableItem>(_items);
        }

        public Dictionary<string, string> GetElementUrls()
        {
            Dictionary<string, string> result = [];
            foreach (var item in _items.Values)
                result.Add(item.AtomicNumber, $"https://pubchem.ncbi.nlm.nih.gov/rest/pug_view/data/element/{item.AtomicNumber}/JSON");
            return result;
        }
    }
}
