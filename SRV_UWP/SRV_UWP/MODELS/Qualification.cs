using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class Qualification
    {
        public string QualCode { get; }
        public string NationalQualCode { get; }
        public string TafeQualCode { get; }
        public string QualName { get; }
        public int TotalUnits { get; }
        public int CoreUnits { get; }
        public int ElectedUnits { get; }
        public int ReqListedElectedUnits { get; }
        public string CompletionStatus { get; set; }
    }
}
