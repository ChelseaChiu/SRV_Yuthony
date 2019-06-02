using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class Lecturer : User
    {
        public string Department { get; }
        /*      private void UpdateCompetency(string newCompletionStatus)
              {
                  Competency.CompletionStatus = newCompletionStatus;
              }
          */
        private Student SearchStudentById(string studentId)
        {
            Student student = new Student();
            if (student.UserID == studentId)  //TODO
            {
                return student;
            }
            else { return null; }
        }

        public bool IsLecturerLogIn(string inUserId)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                Lecturer myLecturer = new Lecturer();
                string query = String.Format("SELECT * FROM lecturer WHERE LecturerID=" + inUserId);
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string stringUserID = reader.GetString(0);
                    myLecturer.UserID = stringUserID;
                }
                dbCon.Close();
                if (!string.IsNullOrEmpty(myLecturer.UserID))
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
        public bool IsParchmentInDB(Student student, string qualificationID)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                //check if the record already exists
                string studentID = student.UserID;
                string query2 = String.Format("select ParchmentID from parchement where StudentID = '{0}' and QualificationID = '{1}'", studentID, qualificationID);
                var cmd2 = new MySqlCommand(query2, dbCon.Connection);
                var reader = cmd2.ExecuteReader();
                string result = null;
                while (reader.Read())
                {
                    result = reader.GetString(0);
                }
                dbCon.Close();
                if (String.IsNullOrEmpty(result))  //no record in the database 
                {
                    return false;
                }
                else { return true; }
            }
            else { return false; }
        }
        public void ApproveParchment(Student student, string qualificationID)
        {

            DBConnection dbCon = new DBConnection();
            Lecturer lecturer = new Lecturer();
            if (dbCon.IsConnect())
            {
                string studentID = student.UserID;
                if (!lecturer.IsParchmentInDB(student, qualificationID)) //if no record, insert a new one
                {
                    string date = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                    string query1 = String.Format("insert into parchement (IssueDate,StudentID,QualificationID) values('{0}','{1}','{2}')", date, studentID, qualificationID);
                    var cmd1 = new MySqlCommand(query1, dbCon.Connection);
                    try
                    {
                        var c1 = cmd1.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    // TODO
                }
                dbCon.Close();
            }

        }
        public void DisApproveParchment(Student student, string qualificationID)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                string studentID = student.UserID;
                string query1 = String.Format("update requestparchment set IsRequested ='0' WHERE StudentID = '{0}' AND QualificationID = '{1}'", studentID, qualificationID);
                var cmd1 = new MySqlCommand(query1, dbCon.Connection);
                try
                {
                    var c1 = cmd1.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw;
                }
                dbCon.Close();
            }
        }
    }
}
