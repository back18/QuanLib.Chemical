using QuanLib.Chemical.Attributes;
using QuanLib.Chemical.Extensions;
using QuanLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public class ElementAddon
    {
        static ElementAddon()
        {
            Type type = typeof(ElementAddon);
            _properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToDictionary(i => i.Name, i => i);
            _units = [];

            foreach (PropertyInfo property in _properties.Values)
            {
                UnitAttribute? unitAttribute = property.GetCustomAttribute<UnitAttribute>();
                if (unitAttribute is not null)
                    _units.Add(property.Name, unitAttribute.Unit);
            }
        }

        public ElementAddon(Element element)
        {
            ArgumentNullException.ThrowIfNull(element, nameof(element));

            _element = element;
        }

        private readonly static Dictionary<string, PropertyInfo> _properties;

        private readonly static Dictionary<string, string> _units;

        private readonly Dictionary<string, object?> _values = [];

        private readonly Element _element;

        public ElectronConfiguration AllElectronConfiguration => _element.GetAllElectronConfiguration();

        public int[] ElectronsPerShell => _element.GetElectronsPerShell();

        public int ShellCount => _element.GetShellCount();

        public int ElectronCount => _element.GetElectronCount();

        public int ProtonCount => _element.GetProtonCount();

        public int NeutronCount => _element.GetNeutronCount();

        public Isotope StableIsotope => _element.GetStableIsotope();

        public string GetPropertiesInfo()
        {
            int length = _properties.Keys.Max(i => i.Length) + 2;
            StringBuilder stringBuilder = new();

            foreach (string propertyName in _properties.Keys)
            {
                stringBuilder.Append(propertyName);
                stringBuilder.Append(new string('-', length - propertyName.Length));
                stringBuilder.AppendLine(GetPropertyText(propertyName));
            }

            return stringBuilder.ToString();
        }

        public string? GetPropertyText(string propertyName)
        {
            object? value = GetPropertyValue(propertyName);
            if (value is null)
                return null;

            string text = value is string str ? str : ObjectFormatter.Format(value);
            if (_units.TryGetValue(propertyName, out var unit))
                text = $"{text} {unit}";

            return text;
        }

        public object? GetPropertyValue(string propertyName)
        {
            if (_values.TryGetValue(propertyName, out var value))
                return value;

            if (!_properties.TryGetValue(propertyName, out var property))
                return null;

            value = property.GetValue(this, null);
            _values.TryAdd(propertyName, value);
            return value;
        }

        public static string? GetPropertyUnit(string propertyName)
        {
            ArgumentException.ThrowIfNullOrEmpty(propertyName, nameof(propertyName));

            _units.TryGetValue(propertyName, out var unit);
            return unit;
        }

        public override string ToString()
        {
            return _element.ToString();
        }
    }
}
