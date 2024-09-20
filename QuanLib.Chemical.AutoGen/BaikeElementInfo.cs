using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public class BaikeElementInfo
    {
        public required string Name { get; set; }

        public required string Description { get; set; }

        public required ReadOnlyDictionary<string, string> Properties { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
