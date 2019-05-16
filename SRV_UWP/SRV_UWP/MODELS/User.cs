using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV_UWP.models
{
    public class User
    {
        public string FirstName { get;set; }
        public string LastName { get; set; }
        public string Email { get; }
        public string Phone { get; }
        public string Address { get; }
        public string DOB { get; }
        public string UserID { get; set; }
        public string Password { get; set; }

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
