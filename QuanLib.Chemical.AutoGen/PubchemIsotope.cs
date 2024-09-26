using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public class PubchemIsotope : IComparable<PubchemIsotope>
    {
        public required string Symbol { get; set; }

        public required int MassNumber { get; set; }

        public required double AtomicMass { get; set; }

        public required double Abundance { get; set; }

        public int CompareTo(PubchemIsotope? other)
        {
            if (other is null)
                return 1;

            return MassNumber.CompareTo(other.MassNumber);
        }
    }
}
