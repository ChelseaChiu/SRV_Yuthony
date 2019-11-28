using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; }
        public string Address { get; }
        public string DOB { get; }
        public string UserID { get; set; }
        public string Password { get; set; }

        public static string DEFAULT_PASSWORD = "srv";

        public bool Login(string id)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                User user = new User();
                string query = String.Format("SELECT * FROM student WHERE StudentID='{0}'", id);
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    user.UserID = reader.GetString(0);
                }
                dbCon.Close();
                if (!string.IsNullOrEmpty(user.UserID))
                {
                    return true;
                }
                else
                {
                    return false;
                } //end validate return user id
            }
            else
            {
                return false;
            } // end validate database connection
        }

        public Qualification SelectQualification(Qualification selectedQual)
        {
            return selectedQual.GetQualification(selectedQual.QualCode);
        }
    }
}
