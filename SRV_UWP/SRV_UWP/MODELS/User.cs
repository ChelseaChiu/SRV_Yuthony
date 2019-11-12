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
        /*
                public bool Login(string inUserId, string inPassword)
                {
                    DBConnection dbCon = new DBConnection();
                    if (dbCon.IsConnect())
                    {
                        User myUser = new User();
                        string query = String.Format("SELECT * FROM user WHERE UserID='{0}' AND Password='{1}'", inUserId, inPassword);
                        var cmd = new MySqlCommand(query, dbCon.Connection);
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string stringUserID = reader.GetString(0);
                            myUser.UserID = stringUserID;
                        }
                        dbCon.Close();
                        if (!string.IsNullOrEmpty(myUser.UserID))
                        {
                            return true;
                        }
                        else { return false; }
                    }
                    else { return false; }
                }
        */
        //new login for student ID only - 18/10/2019 Yuchun
        public bool Login(string inUserId)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                User myUser = new User();
                string query = String.Format("SELECT * FROM student WHERE StudentID='{0}'", inUserId);
                var cmd = new MySqlCommand(query, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string stringUserID = reader.GetString(0);
                    myUser.UserID = stringUserID;
                }
                dbCon.Close();
                if (!string.IsNullOrEmpty(myUser.UserID))
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
        public Qualification SelectQualification(Qualification selectedItem)
        {
            Qualification mySelectedItem = selectedItem;
            //TODO
            return mySelectedItem;
        }
    }
}
