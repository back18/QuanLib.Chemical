using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.CsvParsing
{
    public class IntReadOnlyCollectionConverter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
                return ReadOnlyCollection<int>.Empty;

            text = text.Replace(" ", string.Empty);
            string[] items = text.Split(',');
            List<int> result = [];

            foreach (string item in items)
            {
                if (!string.IsNullOrEmpty(item))
                    result.Add(int.Parse(item));
            }

            return result.AsReadOnly();
        }

        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is not IEnumerable<int> items)
                return null;

            List<string> texts = [];
            foreach (int item in items)
            {
                string text = item.ToString();
                if (!text.StartsWith('-'))
                    text = '+' + text;
                texts.Add(text);
            }

            return string.Join(", ", texts);
        }
    }
}
