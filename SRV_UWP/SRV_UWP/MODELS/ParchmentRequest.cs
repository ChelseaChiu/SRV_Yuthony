using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class ParchmentRequest

    {
        public Student Student { get; set; }
        public Qualification Qualification { get; set; }
        public string RequestDate { get; set; }
        public bool RequestedStatus { get; set; }
    }
}
