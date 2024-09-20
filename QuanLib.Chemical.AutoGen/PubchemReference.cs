using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLib.Chemical.AutoGen
{
    public class PubchemReference
    {
        public required int ReferenceNumber { get; set; }

        public required string SourceName { get; set; }

        public required string SourceID { get; set; }

        public required string Description { get; set; }

        public required string URL { get; set; }

        public string? LicenseNote { get; set; }

        public string? LicenseURL { get; set; }

        public int? ANID { get; set; }
    }
}
