using SRV_UWP.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.viewmodels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        private Student student = new Student();

        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propname)
        {
            var eh = PropertyChanged;
            if (eh != null)
                eh(this, new PropertyChangedEventArgs(propname));
        }
        public Student Student
        {
            get { return student; }

            set
            {
                if (student != value)
                {
                    student = value;
                    RaisePropertyChanged("Student");

                }
            }
        }

        private ObservableCollection<Qualification> qualifications;
        public ObservableCollection<Qualification> Qualifications
        {

            get { return qualifications; }
            set
            {
                if (qualifications != value)
                {
                    qualifications = value;
                    RaisePropertyChanged("Qualifications");
                }
            }
        }
        public DetailsViewModel(string studentId)
        {
            // parameter studentId passed from prevoius page
            this.Student = student.GetStudentById(studentId);
            this.Qualifications = new ObservableCollection<Qualification>(Qualification.GetQualificationList(Student.UserID).ToList().OrderBy(q => q.QualName));
        }

    }
}
