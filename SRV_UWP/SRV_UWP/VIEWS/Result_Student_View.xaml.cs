using SRV_UWP.models;
using SRV_UWP.viewmodels;
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
    public sealed partial class Result_Student_View : Page
    {
        DetailsViewModel viewModel;
        Student Student;

        public Result_Student_View()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            string id = (string)e.Parameter;

            if (viewModel == null)
            {
                viewModel = new DetailsViewModel(id);
                stackStudentDetail.DataContext = viewModel.Student;
                Student = viewModel.Student;
                comboQual.ItemsSource = viewModel.Qualifications;
            }
            else
            {
                //Frame.Navigate(typeof(views.MainPage));

            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

            //need log out functionality here

            Frame.Navigate(typeof(LoginPage)); //Log out and navigate to log in page

        }

        private void Select_Qualification(object sender, SelectionChangedEventArgs e)
        {
            Qualification sQual = comboQual.SelectedItem as Qualification;
            if (sQual != null)
            {
                stackComp.Visibility = Visibility.Visible;
                stackReqUnits.DataContext = sQual;
                stackStudentDetail.DataContext = Student;
                List<Competency> compList = new List<Competency>();
                compList = Competency.GetCompetencyList(Student.UserID, sQual.QualCode).ToList();
                listBoxCompetency.ItemsSource = compList;

                // set progress bar need to further coding
                sQual.DoneC = compList.Where(c => c.CompletionStatus == "C").Count(c => c.TrainingPakckageUsage == "C");
                progressC.Value = sQual.DoneC;
                sQual.DoneE = compList.Where(c => c.CompletionStatus == "C").Count(c => c.TrainingPakckageUsage == "E");
                progressE.Value = sQual.DoneE;
                sQual.DoneLE = compList.Where(c => c.CompletionStatus == "C").Count(c => c.TrainingPakckageUsage == "LE");
                progressLE.Value = sQual.DoneLE;

                if (Qualification.IsCompleted(sQual))
                {
                    sQual.DoneTotal = sQual.TotalUnits;
                    progressT.Value = sQual.DoneTotal;
                }
                else
                {
                    sQual.DoneTotal = sQual.DoneC + sQual.DoneE + sQual.DoneLE;
                    progressT.Value = sQual.DoneTotal;
                }
                stackUnits.DataContext = sQual;
                stackProg.DataContext = sQual;
                btnReqParche.Visibility = Visibility.Visible;
                Student.Qualification = sQual;

            }
        }

        private async void Req_Parchment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Student.IsParchmentRequested(Student, Student.Qualification.QualCode))
                {
                    var message1 = new MessageDialog("Your request is in progress", "Please wait");
                    await message1.ShowAsync();
                }
                else
                {
                    Student.RequestParchment(Student, Student.Qualification.QualCode);
                    var message = new MessageDialog("Your request is submitted.", "Success");
                    await message.ShowAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
