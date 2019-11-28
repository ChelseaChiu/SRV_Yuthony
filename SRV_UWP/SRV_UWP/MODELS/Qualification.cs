using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class Qualification
    {
        public string QualCode { get; set; }
        public string NationalQualCode { get; set; }
        public string TafeQualCode { get; set; }
        public string QualName { get; set; }
        public int TotalUnits { get; set; }
        public int CoreUnits { get; set; }
        public int ElectedUnits { get; set; }
        public int ReqListedElectedUnits { get; set; }
        public string CompletionStatus { get; set; }
        //public ObservableCollection<Competency> Competencies { get; set; }

        public int DoneC { get; set; }
        public int DoneE { get; set; }
        public int DoneLE { get; set; }
        public int DoneTotal { get; set; }

        public static bool IsCompleted(Qualification sQual)
        {
            int core = sQual.CoreUnits;
            int elective = sQual.ElectedUnits;
            int listedE = sQual.ReqListedElectedUnits;
            int total = sQual.TotalUnits;
            int afterCore = total - sQual.DoneC;
            if (sQual.DoneC < core)
            {
                return false;
            }
            else if (sQual.DoneE + sQual.DoneLE < afterCore)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public static List<Qualification> GetQualificationList(string studentID)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                List<Qualification> qualificationList = new List<Qualification>();

                //below is the query for provided db
                string query = String.Format("select * from qualification inner join student_studyplan on qualification.QualCode = student_studyplan.QualCode where student_studyplan.StudentID = " + studentID);
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Qualification qualification = new Qualification();
                    qualification.QualCode = reader.GetString(0);
                    qualification.NationalQualCode = reader.GetString(1);
                    qualification.TafeQualCode = reader.GetString(2);
                    qualification.QualName = reader.GetString(3);
                    qualification.TotalUnits = Int32.Parse(reader.GetString(4));
                    qualification.CoreUnits = Int32.Parse(reader.GetString(5));
                    qualification.ElectedUnits = Int32.Parse(reader.GetString(6));
                    qualification.ReqListedElectedUnits = Int32.Parse(reader.GetString(7));
                    qualification.Competencies = Competency.GetCompetencyList(studentID, qualification.QualCode);
                    qualification.DoneC = qualification.Competencies.Where(c => c.Results == "PA").Count(c => c.TrainingPakckageUsage == "C");
                    qualification.DoneE = qualification.Competencies.Where(c => c.Results == "PA").Count(c => c.TrainingPakckageUsage == "E");
                    qualification.DoneLE = qualification.Competencies.Where(c => c.Results == "PA").Count(c => c.TrainingPakckageUsage == "LE") + qualification.Competencies.Where(c => c.Results == "PA").Count(c => c.TrainingPakckageUsage == "C_SUP");
                    if (IsCompleted(qualification))
                    {
                        qualification.DoneTotal = qualification.TotalUnits;
                    }
                    else
                    {
                        qualification.DoneTotal = qualification.DoneC + qualification.DoneE + qualification.DoneLE;
                    }
                    qualificationList.Add(qualification);
                }
                dbCon.Close();
                return qualificationList;
            }
            else { return null; }
          

        }

        public List<Competency> Competencies { get; set; }

        public Qualification GetQualification(string qualCode)
        {
            Qualification qualification = new Qualification();
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                string query = String.Format("select * from qualification where qualification.QualCode = '{0}'", qualCode);
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    qualification.QualCode = reader.GetString(0);
                    qualification.NationalQualCode = reader.GetString(1);
                    qualification.TafeQualCode = reader.GetString(2);
                    qualification.QualName = reader.GetString(3);
                    qualification.TotalUnits = Int32.Parse(reader.GetString(4));
                    qualification.CoreUnits = Int32.Parse(reader.GetString(5));
                    qualification.ElectedUnits = Int32.Parse(reader.GetString(6));
                    qualification.ReqListedElectedUnits = Int32.Parse(reader.GetString(7));

                }
                dbCon.Close();
                return qualification;
            }
            else return null;

        }
    }
}
