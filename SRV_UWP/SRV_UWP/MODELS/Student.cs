using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class Student:User
    {
        public string Qualification { get; }
        private void RequestParchment()
        {
            ParchmentRequest.RequestDate = DateTime.Now.ToString("dd/MMMM/yyyy");
            ParchmentRequest.RequestedStatus = true;
        }

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
                    string stringStudentFirstName= reader.GetString(1);
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
    }
}
