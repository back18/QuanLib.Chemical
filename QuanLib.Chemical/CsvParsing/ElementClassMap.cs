using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.CsvParsing
{
    public class ElementClassMap : ClassMap<Element>
    {
        public ElementClassMap()
        {
            //AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Symbol);
            Map(m => m.EnglishName);
            Map(m => m.ChineseName);
            Map(m => m.PinYin);
            Map(m => m.AtomicMass);
            Map(m => m.AtomicRadius);
            Map(m => m.Density);
            Map(m => m.MeltingPoint);
            Map(m => m.BoilingPoint);
            Map(m => m.IonizationEnergy);
            Map(m => m.ElectronAffinity);
            Map(m => m.Electronegativity);
            Map(m => m.ElectronConfiguration).Name("ElectronConfiguration").TypeConverter<ElectronConfigurationConverter>();
            Map(m => m.OxidationStates).Name("OxidationStates").TypeConverter<IntReadOnlyCollectionConverter>();
            Map(m => m.GroupBlock);
            Map(m => m.StandardState);
            Map(m => m.CPKColor).Name("CPKColor").TypeConverter<Rgb24Converter>();
            Map(m => m.YearDiscovered);
        }
    }
}
