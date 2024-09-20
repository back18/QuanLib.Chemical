using QuanLib.Core;
using QuanLib.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public readonly struct Orbital(int shell, OrbitalType type, int count) : IParsable<Orbital>, IEquatable<Orbital>
    {
        public int Shell { get; } = shell;

        public OrbitalType Type { get; } = type;

        public int Count { get; } = count;

        public static Orbital Merge(Orbital left, Orbital right)
        {
            if (left.Shell != right.Shell || left.Type != right.Type)
                throw new InvalidOperationException("无法合并层或类型不一致的两个轨道");

            return new(left.Shell, left.Type, left.Count + right.Count);
        }

        public static Orbital Parse(string s) => Parse(s, null);

        public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out Orbital result) => TryParse(s, null, out result);

        public static Orbital Parse(string s, IFormatProvider? provider)
        {
            ArgumentException.ThrowIfNullOrEmpty(s, nameof(s));

            string[] items = s.Split(Enum.GetNames<OrbitalType>().Select(i => i.ToLowerInvariant()).ToArray(), 2, StringSplitOptions.RemoveEmptyEntries);
            if (items.Length != 2)
                throw new FormatException();

            string item1 = items[0];
            string item2 = s[item1.Length].ToString();
            string item3 = items[1];

            int shell = int.Parse(item1);
            OrbitalType type = (OrbitalType)Enum.Parse(typeof(OrbitalType), item2, true);
            int count = int.Parse(item3.ToString());

            return new(shell, type, count);
        }

        public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out Orbital result)
        {
            if (!string.IsNullOrEmpty(s) &&
                s.Length == 3 &&
                int.TryParse(s[0].ToString(), out var shell) &&
                Enum.TryParse(typeof(OrbitalType), s[1].ToString(), true, out var type) &&
                int.TryParse(s[2].ToString(), out var count))
            {
                result = new(shell, (OrbitalType)type, count);
                return true;
            }
            else
            {
                result = default;
                return false;
            }
        }

        public bool Equals(Orbital other)
        {
            return Shell == other.Shell && Type == other.Type && Count == other.Count;
        }

        public override bool Equals(object? obj)
        {
            return obj is Orbital other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Shell, Type, Count);
        }

        public override string ToString()
        {
            return $"{Shell}{Type.ToString().ToLowerInvariant()}{Count}";
        }

        public static bool operator ==(Orbital left, Orbital right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Orbital left, Orbital right)
        {
            return !left.Equals(right);
        }
    }
}
