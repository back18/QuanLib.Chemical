using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public class ElementContext
    {
        public ElementContext(PubchemElement pubchemElement, BaikeElementIntroduction baikeElementIntroduction, BaikeElement baikeElement)
        {
            ArgumentNullException.ThrowIfNull(pubchemElement, nameof(pubchemElement));
            ArgumentNullException.ThrowIfNull(baikeElementIntroduction, nameof(baikeElementIntroduction));
            ArgumentNullException.ThrowIfNull(baikeElement, nameof(baikeElement));

            PubchemElement = pubchemElement;
            BaikeElementIntroduction = baikeElementIntroduction;
            BaikeElement = baikeElement;
        }

        public PubchemElement PubchemElement { get; }

        public BaikeElementIntroduction BaikeElementIntroduction { get; }

        public BaikeElement BaikeElement { get; }
    }
}
