using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using P3_Midwife.Views;
using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace P3_Midwife
{ 
    public partial class HomeScreen : BaseWindow
    {
        private Employee CurrentEmployee;
        public Button targetButton;
        public HomeScreen()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            Messenger.Default.Register<Employee>(this, "ActiveUser", validateUser);
            new DialogWindow();
            new PatientWindow();
            new FinalRecordWindow();
            new ChildInfoDialogWindow();
        }

        private void show()
        {
            if (CurrentEmployee is SOSU)
            {
                AddPatientbtn.Visibility = Visibility.Hidden;
            }
            CPRTextbox.Clear();
            CPRTextbox.Focus();
            Show();
        }



        private void validateUser(Employee emp)
        {
            CurrentEmployee = emp; 
        } 

        private void NotificationMessageRecieved(NotificationMessage msg)
        {                       
            if (msg.Notification == "ToHome")
            {
                show();
            }
            else if (msg.Notification == "NoCPRInput")
            {
                MessageBox.Show("Intet CPR registreret!\nIndtast venligst et CPR nummer i tekstboksen");
            }            
            else if(msg.Notification != "ToDialog")
            {
                Hide();
            }                
        }

        private void ActivePatientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filemanagement.ReadBirthRecords((Patient)chosenPatient.SelectedItem);
            Messenger.Default.Send<Patient>((Patient)chosenPatient.SelectedItem, "Patient");
            Messenger.Default.Send<Employee>((Employee)chosenPatient.Tag, "Employee");
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToPatient"));

        }

        private void CPRTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            FindPatientBtn.IsDefault = true;
        }

        private void CPRTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            FindPatientBtn.IsDefault = false;
        }
    }
}
