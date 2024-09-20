using QuanLib.Chemical.Attributes;
using QuanLib.Core;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public class Element
    {
        public const double PROTON_MASS = 1.007277;

        public const double NEUTRON_MASS = 1.008665;

        static Element()
        {
            Type type = typeof(Element);
            _properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            _units = [];

            foreach (PropertyInfo property in _properties)
            {
                UnitAttribute? unitAttribute = property.GetCustomAttribute<UnitAttribute>();
                if (unitAttribute is not null)
                    _units.Add(property.Name, unitAttribute.Unit);
            }
        }

        private readonly static PropertyInfo[] _properties;

        private readonly static Dictionary<string, string> _units;

        /// <summary>
        /// 元素符号
        /// </summary>
        public required ElementSymbol Symbol { get; init; }

        /// <summary>
        /// 原子序数
        /// </summary>
        public int AtomicNumber => (int)Symbol;

        /// <summary>
        /// 英文名称
        /// </summary>
        public required string EnglishName { get; init; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public required string ChineseName { get; init; }

        /// <summary>
        /// 汉语拼音
        /// </summary>
        public required string PinYin { get; init; }

        /// <summary>
        /// 质子数量
        /// </summary>
        public int ProtonCount => AtomicNumber;

        /// <summary>
        /// 相对原子质量
        /// </summary>
        [Unit("u")]
        public double AtomicMass { get; init; }

        /// <summary>
        /// 原子半径
        /// </summary>
        [Unit("pm")]
        public double? AtomicRadius { get; init; }

        /// <summary>
        /// 密度
        /// </summary>
        [Unit("g/cm³")]
        public double? Density { get; init; }

        /// <summary>
        /// 熔点
        /// </summary>
        [Unit("K")]
        public double? MeltingPoint { get; init; }

        /// <summary>
        /// 沸点
        /// </summary>
        [Unit("K")]
        public double? BoilingPoint { get; init; }

        /// <summary>
        /// 电离能
        /// </summary>
        [Unit("eV")]
        public double? IonizationEnergy { get; init; }

        /// <summary>
        /// 电子亲和能
        /// </summary>
        [Unit("eV")]
        public double? ElectronAffinity { get; init; }

        /// <summary>
        /// 电负性
        /// </summary>
        public double? Electronegativity { get; init; }

        /// <summary>
        /// 电子构型
        /// </summary>
        public required ElectronConfiguration ElectronConfiguration { get; init; }

        /// <summary>
        /// 氧化状态
        /// </summary>
        public required ReadOnlyCollection<int> OxidationStates { get; init; }

        /// <summary>
        /// 族块
        /// </summary>
        public required GroupBlock GroupBlock { get; init; }

        /// <summary>
        /// 标准状态
        /// </summary>
        public required StandardState StandardState { get; init; }

        /// <summary>
        /// CPK颜色
        /// </summary>
        public required Rgb24 CPKColor { get; init; }

        /// <summary>
        /// 发现年份
        /// </summary>
        public required int YearDiscovered { get; init; }

        /// <summary>
        /// 在元素周期表中的位置
        /// </summary>
        public Position PeriodicPosition => PeriodicGrid.GetPosition(Symbol);

        /// <summary>
        /// 在元素周期表中的分区
        /// </summary>
        public Block PeriodicBlock => PeriodicGrid.GetBlock(Symbol);

        public string GetPropertiesInfo()
        {
            int length = _properties.Max(i => i.Name.Length) + 2;
            StringBuilder stringBuilder = new();

            foreach (PropertyInfo property in _properties)
            {
                stringBuilder.Append(property.Name);
                stringBuilder.Append(new string('-', length - property.Name.Length));

                object? value = property.GetValue(this, null);
                stringBuilder.Append(ObjectFormatter.Format(value));

                if (value is not null && _units.TryGetValue(property.Name, out var unit))
                    stringBuilder.Append(unit);

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public static string? GetPropertyUnit(string property)
        {
            ArgumentException.ThrowIfNullOrEmpty(property, nameof(property));

            _units.TryGetValue(property, out var unit);
            return unit;
        }

        public override string ToString()
        {
            return $"{Symbol} ({AtomicNumber})";
        }
    }
}
