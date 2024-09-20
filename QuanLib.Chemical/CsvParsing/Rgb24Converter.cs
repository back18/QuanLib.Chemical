using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.CsvParsing
{
    public class Rgb24Converter : DefaultTypeConverter
    {
        public override object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
        {
            if (string.IsNullOrEmpty(text))
                return null;

            return Color.ParseHex(text).ToPixel<Rgb24>();
        }

        public override string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is not Rgb24 rgb24)
                return null;

            return new Color(rgb24).ToHex()[..6];
        }
    }
}
