using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public readonly struct Position(int period, int group) : IEquatable<Position>
    {
        public static readonly Position LanthanidePosition = new(6, 3);

        public static readonly Position ActinidePosition = new(7, 3);

        public static readonly Position StartPosition = new(1, 1);

        public static readonly Position EndPosition = new(7, 18);

        public int Period { get; } = period;

        public int Group { get; } = group;

        public Position Offset(int periodOffset, int groupOffset)
        {
            return new(Period + periodOffset, Group + groupOffset);
        }

        public bool Equals(Position other)
        {
            return Period == other.Period && Group == other.Group;
        }

        public override bool Equals(object? obj)
        {
            return obj is Position other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Group, Period);
        }

        public override string ToString()
        {
            return $"[Period={Period}, Group={Group}]";
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !left.Equals(right);
        }
    }
}
