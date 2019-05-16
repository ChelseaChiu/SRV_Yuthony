using SRV_UWP.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace SRV_UWP.viewmodels
{
    public class DetailsViewModel:INotifyPropertyChanged
    {
        private Student student;
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
        public DetailsViewModel(string studentId)
        {
            Student s = new Student();
            this.Student = s.GetStudentById(studentId);

        }
    }

}
