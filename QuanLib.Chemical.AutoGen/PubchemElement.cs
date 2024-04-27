using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public class PubchemElement
    {
        public required string AtomicNumber { get; set; }

        public required string Symbol { get; set; }

        public required string Name { get; set; }

        public required string AtomicMass { get; set; }

        public required string CPKHexColor { get; set; }

        public required string ElectronConfiguration { get; set; }

        public required string Electronegativity { get; set; }

        public required string AtomicRadius { get; set; }

        public required string IonizationEnergy { get; set; }

        public required string ElectronAffinity { get; set; }

        public required string OxidationStates { get; set; }

        public required string StandardState { get; set; }

        public required string MeltingPoint { get; set; }

        public required string BoilingPoint { get; set; }

        public required string Density { get; set; }

        public required string GroupBlock { get; set; }

        public required string YearDiscovered { get; set; }
    }
}
