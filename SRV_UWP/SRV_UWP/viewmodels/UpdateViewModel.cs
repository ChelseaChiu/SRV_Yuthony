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
    public class UpdateViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propname)
        {
            var eh = PropertyChanged;
            if (eh != null)
                eh(this, new PropertyChangedEventArgs(propname));
        }
        private Competency competency;
        public Competency Competency
        {
            get { return competency; }

            set
            {
                if (competency != value)
                {
                    competency = value;
                    RaisePropertyChanged("Competency");

                }
            }
        }
        public Qualification Qualification;
        private Student student = new Student();
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
        string studentID = App.tempStudentID;
        Qualification sQual = App.tempQual;
        public UpdateViewModel(Competency sComp)
        {
            
            this.Student = student.GetStudentById(studentID);
            this.Qualification = sQual;
            this.Qualifications = new ObservableCollection<Qualification>(Qualification.GetQualificationList(Student.UserID).ToList().OrderBy(q => q.QualName));
            this.Competency = sComp;        
        }

    }
}
