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
    public sealed partial class Result_Lecturer_View : Page
    {
        DetailsViewModel viewModel;
        Student Student;
        Lecturer myLecturer = new Lecturer();

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
                stackStudentDetail.DataContext = viewModel.Student;
                Student = viewModel.Student;
                comboQual.ItemsSource = viewModel.Qualifications;
            }
            else
            {
                //Frame.Navigate(typeof(views.MainPage));

            }
        }
        private async void btnFurtherAction_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to FurtherAction page
            if (listBoxCompetency.SelectedIndex != -1)
            {
                Competency sComp = new Competency();
                sComp = listBoxCompetency.SelectedItem as Competency;
                App.tempStudentID = Student.UserID;
                Frame.Navigate(typeof(FurtherAction_Lecturer), sComp);
            }
            else
            {
                var message = new MessageDialog("Please select a competency for further action");
                await message.ShowAsync();
            }

        }
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(LoginPage)); //Log out and navigate to log in page
            App.tempQual = null;
            App.tempStudentID = null;
            App.tempComp = null;

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchStudent)); //Navigate to Search Student Page
            App.tempQual = null;
            App.tempStudentID = null;
            App.tempComp = null;
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
                App.tempQual = sQual;
            }

        }

        private async void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            Qualification qual = App.tempQual;
            if (!Qualification.IsCompleted(qual))
            {
                var message1 = new MessageDialog("Can not procced before complete total units", "Further Action Needed");
                await message1.ShowAsync();
            }
            else
            {
                try
                {
                    Lecturer.ApproveParchment(Student, qual.QualCode);
                    var message = new MessageDialog("Parchment has been approved", "Approved");
                    await message.ShowAsync();
                }
                catch (Exception)
                {

                    throw;
                }

            }

        }

        private async void btnDisApprove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Lecturer.DisApproveParchment(Student, App.tempQual.QualCode);
                var message = new MessageDialog("Parchment has not been approved", "Not Approved");
                await message.ShowAsync();
            }
            catch { throw; }
        }
    }
}
