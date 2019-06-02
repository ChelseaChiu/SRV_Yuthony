using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
                string query = String.Format("select * from qualification inner join student_qualification on " +
                    "qualification.QualificationID=student_qualification.QualificationID where StudentID=" + studentID);
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string qualID = reader.GetString(0);
                    string nationalCode = reader.GetString(1);
                    string qualName = reader.GetString(2);
                    string totalUnits = reader.GetString(3);
                    string core = reader.GetString(4);
                    string elective = reader.GetString(5);
                    string listedElective = reader.GetString(6);
                    Qualification qualification = new Qualification();
                    qualification.QualCode = qualID;
                    qualification.QualName = qualName;
                    qualification.NationalQualCode = nationalCode;
                    qualification.TotalUnits = Int32.Parse(totalUnits);
                    qualification.CoreUnits = Int32.Parse(core);
                    qualification.ElectedUnits = Int32.Parse(elective);
                    qualification.ReqListedElectedUnits = Int32.Parse(listedElective);
                    qualificationList.Add(qualification);
                }
                dbCon.Close();
                return qualificationList;
            }
            else { return null; }
        }

        public List<Competency> Competencies { get; set; }


    }
}
