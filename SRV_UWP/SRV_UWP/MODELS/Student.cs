using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class Student:User
    {
        public string Qualification { get; }
        private void RequestParchment()
        {
            ParchmentRequest.RequestDate = DateTime.Now.ToString("dd/MMMM/yyyy");
            ParchmentRequest.RequestedStatus = true;
        }
    }
}
