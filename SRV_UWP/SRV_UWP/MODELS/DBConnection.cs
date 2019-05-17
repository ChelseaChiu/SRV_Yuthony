using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Windows.UI.Popups;

namespace SRV_UWP.models
{
    public class DBConnection
    {
        private MySqlConnection connection = null;
        private string server;
        private string database = String.Empty;
        public string Database
        {
            get { return database; }
            set { database = value; }
        }
        private string user;
        private string password;
        private string port;
        private string sslM;

        public MySqlConnection Connection { get { return connection; } }
        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }
        public bool IsConnect()
        {
            //   connect to default database on student server 
            /*   
               server = "studentserver.com.au";
               database = "admin_it_studies_dev";
               user = "admin_srv-ya";
               password = "Passw0rd!@#";
            */

            //   connect to custimized database on localhost
            server = "localhost";
            database = "srv_db";
            user = "root";
            password = "mysql";
            //
            port = "3306";
            sslM = "none";
            bool result = false;

            if (Connection == null)
            {
                if (String.IsNullOrEmpty(database))
                {
                    return false;
                }

                try
                {
                    string connString = String.Format
                        ("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);
                    connection = new MySqlConnection(connString);
                    connection.Open();
                    result = true;
                }
                catch (MySqlException ex)
                {
                    return false;                   
                }

            }
            return result;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
