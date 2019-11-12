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
                if (App._Usertype=="lecturer")
                {
                    studentButtonStackPanel.Visibility = Visibility.Collapsed;
                    lecturerButtonsStaclPanel.Visibility = Visibility.Visible;
                    btnSearch.Visibility = Visibility.Visible;
                }

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

                List<Competency> gradedCompetencies = Competency.GetCompetencyList(Student.UserID, sQual.QualCode).ToList();
                try
                {
                    List<Competency> notGradedCompetencies = Competency.GetNotGradedCompetencies(Student.UserID, sQual.QualCode).ToList();
                    if (notGradedCompetencies != null)
                    {
                        gradedCompetencies.AddRange(notGradedCompetencies);
                    }
                }
                catch (Exception)
                {

                    //throw;
                }
                
                listBoxCompetency.ItemsSource = gradedCompetencies;

                // set progress bar need to further coding
                //sQual.DoneC = compList.Where(c => c.Results == "PA").Count(c => c.TrainingPakckageUsage == "C");
                progressC.Value = sQual.DoneC;
               // sQual.DoneE = compList.Where(c => c.Results == "PA").Count(c => c.TrainingPakckageUsage == "E");
                progressE.Value = sQual.DoneE;
                //sQual.DoneLE = compList.Where(c => c.Results == "PA").Count(c => c.TrainingPakckageUsage == "LE") + compList.Where(c => c.Results == "PA").Count(c => c.TrainingPakckageUsage == "C_SUP");
                progressLE.Value = sQual.DoneLE;
                App.tempQual = sQual;

                if (Qualification.IsCompleted(sQual))
                {
                    sQual.DoneTotal = sQual.TotalUnits;
                    progressT.Value = sQual.DoneTotal;
                    btnReqParche.IsEnabled = true;
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
                //if (Student.IsParchmentRequested(Student, Student.Qualification.QualCode))
                //{
                //    var message1 = new MessageDialog("Your request is in progress", "Please wait");
                //    await message1.ShowAsync();
                //}
                //else
                //{
                    //Student.RequestParchment(Student, Student.Qualification.QualCode);
                    var message = new MessageDialog("Your request is submitted.", "Success");
                    await message.ShowAsync();
                //}

            }
            catch (Exception)
            {

                throw;
            }

        }


        //functionalities for lecturers
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
                    //Lecturer.ApproveParchment(Student, qual.QualCode);
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
                //Lecturer.DisApproveParchment(Student, App.tempQual.QualCode);
                var message = new MessageDialog("Parchment has not been approved", "Not Approved");
                await message.ShowAsync();
            }
            catch { throw; }
        }

        private async void btnFurtherAction_Click(object sender, RoutedEventArgs e)
        {
            //Navigate to FurtherAction page
            //if (listBoxCompetency.SelectedIndex != -1)
            //{
              //  Competency sComp = new Competency();
                //sComp = listBoxCompetency.SelectedItem as Competency;
                //App.tempStudentID = Student.UserID;
                //Frame.Navigate(typeof(FurtherAction_Lecturer), sComp);
            //}
            //else
            //{
                var message = new MessageDialog("Sent request to Admin for further action!","Request Sent");
                await message.ShowAsync();
            //}

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchStudent)); //Navigate to Search Student Page
            App.tempQual = null;
            App.tempStudentID = null;
            App.tempComp = null;
        }
    }
}
