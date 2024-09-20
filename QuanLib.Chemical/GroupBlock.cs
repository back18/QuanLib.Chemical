using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical
{
    public enum GroupBlock
    {
        /// <summary>
        /// 碱金属
        /// </summary>
        AlkaliMetal,

        /// <summary>
        /// 碱土金属
        /// </summary>
        AlkalineEarthMetal,

        /// <summary>
        /// 过度金属
        /// </summary>
        TransitionMetal,

        /// <summary>
        /// 后过度金属
        /// </summary>
        PostTransitionMetal,

        /// <summary>
        /// 准金属
        /// </summary>
        Metalloid,

        /// <summary>
        /// 非金属
        /// </summary>
        NonMetal,

        /// <summary>
        /// 卤族元素
        /// </summary>
        Halogen,

        /// <summary>
        /// 稀有气体
        /// </summary>
        NobleGas,

        /// <summary>
        /// 镧系元素
        /// </summary>
        Lanthanide,

        /// <summary>
        /// 锕系元素
        /// </summary>
        Actinide
    }
}
