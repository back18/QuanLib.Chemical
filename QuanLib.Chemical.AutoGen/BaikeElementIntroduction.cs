using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public class BaikeElementIntroduction
    {
        public required string AtomicNumber { get; set; }

        public required string Symbol { get; set; }

        public required string EnglishName { get; set; }

        public required string ChineseName { get; set; }

        public required string PinYin { get; set; }

        public required string AtomicMass { get; set; }

        public required string Introduction { get; set; }

        public override string ToString()
        {
            return $"{Symbol}({AtomicNumber})";
        }
    }
}
