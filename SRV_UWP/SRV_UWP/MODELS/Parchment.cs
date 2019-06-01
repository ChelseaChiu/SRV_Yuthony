using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class Parchment
    {
        public string ApprovalDate { get; set; }
        public string ApproveBy { get; set; }
        public Student Student { get; set; }
        public Qualification Qualification { get; set; }

    }
}
