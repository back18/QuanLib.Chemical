using QuanLib.Chemical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public static class ElementUtil
    {
        static ElementUtil()
        {
            _elementSymbols = new string[ELEMENT_TOTAL];
            for (int i = 1; i <= ELEMENT_TOTAL; i++)
                _elementSymbols[i - 1] = ((ElementSymbol)i).ToString();
        }

        public const int ELEMENT_TOTAL = 118;

        private static readonly string[] _elementSymbols;

        public static string[] GetElementSymbolStrings()
        {
            string[] result = new string[ELEMENT_TOTAL];
            _elementSymbols.CopyTo(result, 0);
            return result;
        }
    }
}
