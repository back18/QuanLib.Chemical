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

            _csvReader = new(new StringReader(csv), CultureInfo.InvariantCulture);
        }

        private readonly CsvReader _csvReader;

        public Dictionary<string, PubchemPeriodicTableItem> GetElements()
        {
            return _csvReader.GetRecords<PubchemPeriodicTableItem>().ToDictionary(item => item.Symbol, item => item);
        }
    }
}
