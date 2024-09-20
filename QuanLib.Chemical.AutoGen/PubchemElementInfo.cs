using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public class PubchemElementInfo
    {
        public required int AtomicNumber { get; set; }

        public required string Name { get; set; }

        public required ReadOnlyCollection<PubchemIsotope> Isotopes { get; set; }
    }
}
