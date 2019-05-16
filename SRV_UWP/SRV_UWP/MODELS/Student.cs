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
            DBConnection dbCon = new DBConnection
            {
                Database = "admin_it_studies_dev"
            };
            if (dbCon.IsConnect())
            {
                Student student = new Student();
                string query = "SELECT * FROM student WHERE StudentId=" + studentId;
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string someStringFromColumnZero = reader.GetString(0);
                    string someStringFromColumnOne = reader.GetString(1);
                    string someStringFromColumnTwo = reader.GetString(2);
                    student.UserID = someStringFromColumnZero;
                    student.FirstName = someStringFromColumnOne;
                    student.LastName = someStringFromColumnTwo;
                }
                dbCon.Close();
                return student;
            }
            else return null;
        }
    }
}
