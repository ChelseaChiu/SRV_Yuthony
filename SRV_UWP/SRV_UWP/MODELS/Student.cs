using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class Student : User
    {
        public Student GetStudentById(string studentId)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                Student student = new Student();
                string query = "SELECT * FROM student WHERE StudentId=" + studentId;
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string stringStudentID = reader.GetString(0);
                    string stringStudentFirstName = reader.GetString(1);
                    string stringStudentLastName = reader.GetString(2);
                    student.UserID = stringStudentID;
                    student.FirstName = stringStudentFirstName;
                    student.LastName = stringStudentLastName;
                }
                dbCon.Close();
                return student;
            }
            else return null;
        }
        public bool IsRequestParchmentInDB(Student student, string qualificationID)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                //check IsRequested field in the database
                string studentID = student.UserID;
                string query3 = String.Format("select StudentID from requestparchment WHERE StudentID = '{0}' AND QualificationID = '{1}'", studentID, qualificationID);
                var cmd3 = new MySqlCommand(query3, dbCon.Connection);
                var reader = cmd3.ExecuteReader();
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
        public void RequestParchment(Student student, string qualificationID)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                string date = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                string studentID = student.UserID;
                if (student.IsRequestParchmentInDB(student, qualificationID)) // if there is a record in database //update record
                {
                    string query2 = String.Format("update requestparchment set IsRequested = '1' WHERE StudentID = '{0}' AND QualificationID = '{1}'", studentID, qualificationID);
                    var cmd2 = new MySqlCommand(query2, dbCon.Connection);
                    try
                    {
                        var c2 = cmd2.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else  //insert a new record
                {
                    string query1 = String.Format("insert into requestparchment (requestDate,IsRequested,StudentID,QualificationID)values('{0}','1','{1}','{2}')", date, studentID, qualificationID);
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
                dbCon.Close();
            }

        }

        public bool IsParchmentRequested(Student student, string qualificationID)  //if the value of IsRequested is true 
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                string studentID = student.UserID;
                string query = String.Format("select IsRequested from requestparchment WHERE StudentID='{0}' AND QualificationID='{1}'", studentID, qualificationID);
                try
                {
                    var cmd = new MySqlCommand(query, dbCon.Connection);
                    var reader = cmd.ExecuteReader();
                    int isRequested = 0;
                    while (reader.Read())
                    {
                        string result = reader.GetString(0);
                        if (result != "False")
                        {
                            isRequested = 1;
                        }

                    }
                    dbCon.Close();
                    if (isRequested != 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    throw;
                }

            }
            else
            {
                return false;
            }
        }
        public Qualification Qualification { get; set; }


    }
}
