using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class Competency : INotifyPropertyChanged
    {
        private string completionStatus;
        public string CompletionStatus
        {
            get { return completionStatus; }

            set
            {
                if (completionStatus != value)
                {
                    completionStatus = value;
                    RaisePropertyChanged("CompletionStatus");

                }
            }
        }
        private string additionalComments;
        public string AdditionalComments
        {
            get { return additionalComments; }
            set
            {
                if (additionalComments != value)
                {
                    additionalComments = value;
                    RaisePropertyChanged("AdditionalComments");
                }
            }
        }
        public string SubjectCode { get; set; }
        public string TafeCode { get; set; }
        public string NationalCode { get; set; }
        public string SubjectName { get; set; }
        public string TrainingPakckageUsage { get; set; }
        public string Results { get; set; }

        public string StudyPlan { get; set; }
        public string CompetencyName { get; set; }
        private ObservableCollection<Competency> competencies;
        public ObservableCollection<Competency> Competencies
        {

            get { return competencies; }
            set
            {
                if (competencies != value)
                {
                    competencies = value;
                    RaisePropertyChanged("Competencies");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void RaisePropertyChanged(string propname)
        {
            var eh = PropertyChanged;
            if (eh != null)
                eh(this, new PropertyChangedEventArgs(propname));
        }
        public static ObservableCollection<Competency> GetCompetencyList(string studentID, string qualificationID)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                ObservableCollection<Competency> competencyList = new ObservableCollection<Competency>();
                string query = String.Format("select SC.Completion_Status, SC.GradeTypeID, SC.GradeSemYear, S.SubjectID, SC.CompetencyID_Tafe, QC.Training_Package_UsageID, C.NationalCompCode, C.CompetencName, SC.ResultComment from student_competency AS SC inner join qualification_competency AS QC on SC.CompetencyID_Tafe=QC.CompetencyID_Tafe AND SC.QualificationID=QC.QualificationID inner join competency AS C on QC.CompetencyID_Tafe=C.CompetencyID_Tafe inner join subject AS S on C.SubjectID=S.SubjectID WHERE SC.StudentID='{0}' AND SC.QualificationID='{1}' group by CompetencyID_Tafe order by Completion_Status, GradeSemYear,SubjectID", studentID, qualificationID);
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string status = reader.GetString(0);
                    string result = reader.GetString(1);
                    string semYear = reader.GetString(2);
                    string subjectID = reader.GetString(3);
                    string tafeCompID = reader.GetString(4);
                    string trainingUsage = reader.GetString(5);
                    string nationalCompID = reader.GetString(6);
                    string compName = reader.GetString(7);
                    string comment = reader.GetString(8);
                    Competency comp = new Competency();
                    comp.CompletionStatus = status;
                    comp.Results = result;
                    comp.StudyPlan = semYear;
                    comp.SubjectCode = subjectID;
                    comp.TrainingPakckageUsage = trainingUsage;
                    comp.TafeCode = tafeCompID;
                    comp.NationalCode = nationalCompID;
                    comp.CompetencyName = compName;
                    comp.AdditionalComments = comment;
                    if (status == "C")
                    {
                        comp.BackColor = "LightBlue";
                    }
                    else if (status == "P")
                    {
                        comp.BackColor = "LightGreen";
                    }
                    else if (status == "NS")
                    {
                        comp.BackColor = "LightGray";
                    }
                    else { comp.BackColor = "White"; }

                    competencyList.Add(comp);
                }
                dbCon.Close();
                return competencyList;
            }
            else { return null; }
        }

        public bool UpdateToDB(string studentID, string qualificationID, string competencyID, string status, string comments)
        {

            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                string query1 = String.Format("update student_competency set Completion_Status='{0}' where StudentID ='{1}' and QualificationID = '{2}' and CompetencyID_Tafe = '{3}'", status, studentID, qualificationID, competencyID);
                string query2 = String.Format("update student_competency set ResultComment = '{0}' where StudentID ='{1}' and QualificationID = '{2}' and CompetencyID_Tafe = '{3}'", comments, studentID, qualificationID, competencyID);
                var cmd1 = new MySqlCommand(query1, dbCon.Connection);
                var cmd2 = new MySqlCommand(query2, dbCon.Connection);
                try
                {
                    var c1 = cmd1.ExecuteNonQuery();
                    var c2 = cmd2.ExecuteNonQuery();

                    return true;

                }
                catch (Exception)
                {

                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public string BackColor
        { get; set; }
    }
}
