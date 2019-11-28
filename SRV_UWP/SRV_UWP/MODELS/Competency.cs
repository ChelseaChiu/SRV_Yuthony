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
        //get competencies which are graded
        public static List<Competency> GetCompetencyList(string studentID, string qualificationID)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                List<Competency> competencyList = new List<Competency>();

                //for provided DB 19/10/2019 yuchun
                //below are queries for customise db
                string query = String.Format("select SG.TermCode, SG.TermYear, SG.Grade,CD.SubjectCode, CD.TafeCompCode, C.NationalCompCode, C.CompetencyName, CQ.CompTypeCode " +
                    "from student_grade AS SG inner join crn_detail AS CD on SG.CRN = CD.CRN left join competency AS C ON CD.TafeCompCode = C.TafeCompCode " +
                    "inner join competency_qualification AS CQ on CQ.NationalCompCode = C.NationalCompCode where SG.StudentID = '{0}' AND CQ.QualCode = '{1}' " +
                    "order by TermYear, TermCode, CompTypeCode;", studentID, qualificationID);
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    //string status = reader.GetString(0);
                    string result = reader.GetString(2);
                    string semYear = string.Format("{0}S{1}",reader.GetString(1),reader.GetString(0));
                    string subjectID = reader.GetString(3);
                    string tafeCompID = reader.GetString(4);
                    string trainingUsage = reader.GetString(7);
                    string nationalCompID = reader.GetString(5);
                    string compName = reader.GetString(6);
                    //string comment = reader.GetString(8);
                    Competency comp = new Competency();
                    
                    comp.Results = result;
                    comp.StudyPlan = semYear;
                    comp.SubjectCode = subjectID;
                    comp.TrainingPakckageUsage = trainingUsage;
                    comp.TafeCode = tafeCompID;
                    comp.NationalCode = nationalCompID;
                    comp.CompetencyName = compName;
                    //comp.AdditionalComments = comment;
                    if (result == "PA")
                    {
                        comp.BackColor = "LightBlue";
                    }
                    else { comp.BackColor = "Gray"; }
                    competencyList.Add(comp);
                }
                dbCon.Close();
                return competencyList;
            }

            else { return null; }
        }

        //get not graded competencies under particular qualification
        public static List<Competency> GetNotGradedCompetencies(string studentID, string qualificationID)
        {
            Qualification qualification = Qualification.GetQualificationList(studentID).First(q => q.QualCode == qualificationID);
            if (Qualification.IsCompleted(qualification))  //if the qualification is completed, return null for not graded competencies
            {
                return null;
            }
            else  //if the qualification is not yet completed, return all competencies excluded graded ones
            {
                //get graded competencies
                List<Competency> gradedCompetencies = GetCompetencyList(studentID, qualificationID).ToList();

                //get all potential competencies
                List<Competency> allCompetencies = new List<Competency>();
                DBConnection dbCon = new DBConnection();
                if (dbCon.IsConnect())
                {
                    //get all competencies under particular qualification
                    string query = String.Format("select SC.SubjectCode, C.TafeCompCode, C.NationalCompCode, CQ.CompTypeCode, C.CompetencyName " +
                                                "from subject_competency AS SC inner join competency AS C on SC.TafeCompCode = C.TafeCompCode" +
                                                " inner join competency_qualification AS CQ on C.NationalCompCode = CQ.NationalCompCode where CQ.QualCode ='{0}'", qualificationID);
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Competency comp = new Competency();
                        comp.SubjectCode = reader.GetString(0);
                        comp.TafeCode = reader.GetString(1);
                        comp.NationalCode = reader.GetString(2);
                        comp.TrainingPakckageUsage = reader.GetString(3);
                        comp.CompetencyName = reader.GetString(4);
                        comp.Results = "N/A";
                        comp.StudyPlan = "N/A";
                        allCompetencies.Add(comp);
                    }
                    dbCon.Close();
                    List<Competency> notGradedCompetencies = allCompetencies.Where(x => !gradedCompetencies.Any(e => x.NationalCode.Equals(e.NationalCode))).GroupBy(p=>p.NationalCode).Select(a=>a.FirstOrDefault()).OrderBy(c=>c.TrainingPakckageUsage).ToList();
                    return notGradedCompetencies;
                }
                else return null;
            }

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
