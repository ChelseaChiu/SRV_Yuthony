using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class User
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string Phone { get; }
        public string Address { get; }
        public string DOB { get; }
        public string UserID { get; set; }
        public string Password { get; }

        public bool Login(string id, string password)
        {
            if (UserID == id && Password == password)
            {
                return true;
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
