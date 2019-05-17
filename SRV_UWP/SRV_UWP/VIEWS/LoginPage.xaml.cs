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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

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
        }
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //need method to identify if the user is a student or a lecturer
            string userid = txtId.Text;
            string password = txtPassword.Password.ToString();
            User user = new User();
            if (user.Login(userid,password))  //call Login method from User class
            {
                //Frame.Navigate(typeof(Result_Student_View));     //If user is student          
                Frame.Navigate(typeof(SearchStudent));      //If user is lecturer
            }
            else
            {
                var message = new MessageDialog("Please enter valid ID and password");
                await message.ShowAsync();
            }

        }
    }
}
