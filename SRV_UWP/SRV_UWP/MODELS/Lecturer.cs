using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class Lecturer:User
    {
        public string Department { get; }
        private void UpdateCompetency(string newCompletionStatus)
        {
            Competency.CompletionStatus = newCompletionStatus;
        }
        private Student SearchStudentById(string studentId)
        {
            Student student = new Student();
            if (student.UserID == studentId)  //TODO
            {
                return student;
            }
            else { return null; }
        }
        private void ApproveParchment()
        {
            Parchment.ApprovalDate = DateTime.Now.ToString("dd/MMMM/yyyy");
            Parchment.ApproveBy = UserID;
        }
    }
}
