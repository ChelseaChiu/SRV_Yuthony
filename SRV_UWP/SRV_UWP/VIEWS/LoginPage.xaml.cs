using SRV_UWP.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SRV_UWP.views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            App._Usertype = null;
        }
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            DBConnection dbCon = new DBConnection();
            if (dbCon.IsConnect())
            {
                //need method to identify if the user is a student or a lecturer

                /* disable login function for database collabration 26/08/2019 - Yuchun
                 * at this stage, only validate database connection 
                 */ 
                string userid = txtId.Text;
                //string password = txtPassword.Password.ToString();
                User user = new User();
                Lecturer lecturer = new Lecturer();
                if (!userid.All(char.IsDigit))  //if user login as string
                {
                    if (lecturer.IsLecturerNameLogIn(userid))   //check if it is lecturer
                    {
                        App._Usertype = "lecturer";
                        Frame.Navigate(typeof(SearchStudent));
                    }
                    else
                    {
                        var message = new MessageDialog("Please enter valid ID");
                        await message.ShowAsync();
                    }
                }
                else
                {
                    if (lecturer.IsLecturerLogIn(userid))  //call Login method from User class
                    {
                        App._Usertype = "lecturer";
                        Frame.Navigate(typeof(SearchStudent));      //If user is lecturer
                    }
                    else if (user.Login(userid))
                    {
                        App._Usertype = "student";
                        Frame.Navigate(typeof(Result_Student_View), userid);    //If user is student 
                    }

                    else
                    {
                        var message = new MessageDialog("Please enter valid ID");
                        await message.ShowAsync();
                    }
                }
            }
          
            else
            {
                var message = new MessageDialog("Database Connection Error!");
                await message.ShowAsync();
            }

        }
    }
}
