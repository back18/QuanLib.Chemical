using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public class Isotope
    {
        public required string Symbol { get; init; }

        public required int MassNumber { get; init; }

        public required double AtomicMass { get; init; }

        public required double Abundance { get; init; }

        public override string ToString()
        {
            return $"{MassNumber}{Symbol} ({Math.Round(Abundance * 100, 2)}%)";
        }
    }
}
