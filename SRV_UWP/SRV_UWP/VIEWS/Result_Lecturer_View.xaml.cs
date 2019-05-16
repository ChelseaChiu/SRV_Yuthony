using MySql.Data.MySqlClient;
using SRV_UWP.models;
using SRV_UWP.viewmodels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Result_Lecturer_View : Page
    {
        DetailsViewModel viewModel;
        public Result_Lecturer_View()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            string id = (string)e.Parameter;

            if (viewModel == null)
            {
                viewModel = new DetailsViewModel(id);
                DataContext = viewModel.Student;
            }
            //else
            //{
            //    Frame.Navigate(typeof(views.MainPage));

            //}
        }
        private void btnFurtherAction_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to FurtherAction page
            Frame.Navigate(typeof(FurtherAction_Lecturer));  //need to pass student ID parameter for the future develop
        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

            //need log out functionality here

            Frame.Navigate(typeof(LoginPage)); //Log out and navigate to log in page

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchStudent)); //Navigate to Search Student Page
        }
    }
}
