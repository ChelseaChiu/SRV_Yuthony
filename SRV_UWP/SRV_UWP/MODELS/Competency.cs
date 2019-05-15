using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class Competency
    {
        public static string CompletionStatus { get; set; }
        public string SubjectCode { get; }
        public string TafeCode { get; }
        public string NationalCode { get; }
        public string SubjectName { get; }
        public int Credits { get; }
        public string TrainingPakckageUsage { get; }
        public string AdditionalComments { get; set; }
        public void AddComment(string comment)
        {
            AdditionalComments = comment;
        }
    }
}
