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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SRV_UWP.views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FurtherAction_Lecturer : Page
    {
        UpdateViewModel viewModel;
        Student Student;
        Qualification sQual;
        public FurtherAction_Lecturer()
        {
            this.InitializeComponent();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Competency sComp = (Competency)e.Parameter;
            if (viewModel == null)
            {
                viewModel = new UpdateViewModel(sComp);
                this.Student = viewModel.Student;
                this.sQual = viewModel.Qualification;
                App.tempComp = sComp;
                stackComp.DataContext = sComp;
                stackStudentDetail.DataContext = Student;
                tbkQual.Text = sQual.QualName;
                listView1.ItemsSource = viewModel.Qualifications;
                foreach (var qual in viewModel.Qualifications)
                {
                    List<Competency> compList = new List<Competency>();
                    compList = Competency.GetCompetencyList(Student.UserID, qual.QualCode).Where(c=>c.CompletionStatus=="C").ToList();
                    qual.Competencies = compList;
                }
            }
            else
            {
                //Frame.Navigate(typeof(views.MainPage));

            }

        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {

            Frame.Navigate(typeof(Result_Lecturer_View),Student.UserID); //Back to Prevoius Page
            App.tempQual = null;
            App.tempComp = null;
        }

        private async void BtnUpdateData_Click(object sender, RoutedEventArgs e)
        {
            Competency sComp = App.tempComp;
            string studentID = Student.UserID;
            string qualID = sQual.QualCode;
            string compID = sComp.TafeCode;
            string status;
            if (comboStatus.SelectedIndex!=-1)
            {
                status = ((ComboBoxItem)comboStatus.SelectedItem).Content.ToString();
            }
            else
            {
                status = sComp.CompletionStatus;
            }
            string comment = txbComment.Text;
            if (sComp.UpdateToDB(studentID,qualID,compID,status,comment))
            {
                var message = new MessageDialog("Update successful.");
                await message.ShowAsync();
            }

            else
            {
                var message = new MessageDialog("Update failed. Please contact admin staff");
                await message.ShowAsync();

            }
        }
    }
}
