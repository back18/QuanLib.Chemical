using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public class ElementContext
    {
        public ElementContext(PubchemPeriodicTableItem pubchemPeriodicTableItem, BaikePeriodicTableItem baikePeriodicTableItem, BaikeElementInfo baikeElementInfo)
        {
            ArgumentNullException.ThrowIfNull(pubchemPeriodicTableItem, nameof(pubchemPeriodicTableItem));
            ArgumentNullException.ThrowIfNull(baikePeriodicTableItem, nameof(baikePeriodicTableItem));
            ArgumentNullException.ThrowIfNull(baikeElementInfo, nameof(baikeElementInfo));

            PubchemPeriodicTableItem = pubchemPeriodicTableItem;
            BaikePeriodicTableItem = baikePeriodicTableItem;
            BaikeElementInfo = baikeElementInfo;
        }

        public PubchemPeriodicTableItem PubchemPeriodicTableItem { get; }

        public BaikePeriodicTableItem BaikePeriodicTableItem { get; }

        public BaikeElementInfo BaikeElementInfo { get; }
    }
}
