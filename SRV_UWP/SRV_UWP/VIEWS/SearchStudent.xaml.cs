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
    public sealed partial class SearchStudent : Page
    {
        public SearchStudent()
        {
            this.InitializeComponent();
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string studentId = txbStudentId.Text;
            if (!string.IsNullOrEmpty(studentId))
            {
                Student student = new Student();
                student = student.GetStudentById(studentId);
                if (string.IsNullOrEmpty(student.FirstName))
                {
                    var message = new MessageDialog("Invalid Student ID, not in database");
                    await message.ShowAsync();
                }
                else
                {
                    Frame.Navigate(typeof(Result_Lecturer_View), studentId);
                }
            }
            else
            {
                var message = new MessageDialog("Please enter student ID");
                await message.ShowAsync();
            }


        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

            //need log out functionality here

            Frame.Navigate(typeof(LoginPage)); //Log out and navigate to log in page

        }
    }
}
