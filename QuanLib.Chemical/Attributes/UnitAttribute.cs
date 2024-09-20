using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class UnitAttribute : Attribute
    {
        public string Unit { get; }

        public UnitAttribute(string unit)
        {
            ArgumentException.ThrowIfNullOrEmpty(unit, nameof(unit));

            Unit = unit;
        }
    }
}
