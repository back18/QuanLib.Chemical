using QuanLib.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public class ElectronConfiguration : IParsable<ElectronConfiguration>
    {
        public ElectronConfiguration(ElementSymbol? basedOn, IList<Orbital> orbitals)
        {
            ArgumentNullException.ThrowIfNull(orbitals, nameof(orbitals));

            BasedOn = basedOn;
            Orbitals = orbitals.AsReadOnly();
        }

        public ElementSymbol? BasedOn { get; }

        public ReadOnlyCollection<Orbital> Orbitals { get; }

        public int GetElectronCount(int shell)
        {
            return Orbitals.Where(i => i.Shell == shell).Sum(i => i.Count);
        }

        public static ElectronConfiguration Parse(string s) => Parse(s, null);

        public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out ElectronConfiguration result) => TryParse(s, out result);

        public static ElectronConfiguration Parse(string s, IFormatProvider? provider)
        {
            ArgumentException.ThrowIfNullOrEmpty(s, nameof(s));

            string[] items = s.Split(' ');
            ElementSymbol? basedOn = null;
            List<Orbital> orbitals = [];

            if (items.Length > 0)
            {
                string item0 = items[0];
                if (item0.StartsWith('[') && item0.EndsWith(']'))
                {
                    item0 = item0[1..^1];
                    items = items.RemoveAt(0);
                    basedOn = (ElementSymbol)Enum.Parse(typeof(ElementSymbol), item0, true);
                }

                foreach (string item in items)
                    orbitals.Add(Orbital.Parse(item));
            }

            return new(basedOn, orbitals);
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out ElectronConfiguration result)
        {
            if (string.IsNullOrEmpty(s))
            {
                result = default;
                return false;
            }

            string[] items = s.Split(' ');
            ElementSymbol? basedOn = null;
            List<Orbital> orbitals = [];

            if (items.Length > 0)
            {
                string item0 = items[0];
                if (item0.StartsWith('[') && item0.EndsWith(']'))
                {
                    item0 = item0[1..^1];
                    items = items.RemoveAt(0);
                    if (!Enum.TryParse(typeof(ElementSymbol), item0, true, out var basedOnResult))
                    {
                        result = default;
                        return false;
                    }

                    basedOn = (ElementSymbol)basedOnResult;
                }

                foreach (string item in items)
                {
                    if (!Orbital.TryParse(item, out var orbital))
                    {
                        result = default;
                        return false;
                    }
                    orbitals.Add(orbital);
                }
            }

            result = new(basedOn, orbitals);
            return true;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            if (BasedOn.HasValue)
                stringBuilder.AppendFormat("[{0}]", BasedOn.Value);
            if (Orbitals.Count > 0)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(' ');
                stringBuilder.AppendJoin(' ', Orbitals);
            }
            return stringBuilder.ToString();
        }
    }
}
