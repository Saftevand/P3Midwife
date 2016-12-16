using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using P3_Midwife.Views;
using System.ComponentModel;


namespace P3_Midwife
{ 
    public partial class HomeScreen : Window
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
            Closing += ClosingHandler;
        }
        private void ClosingHandler(object sender, CancelEventArgs e)
        {
           Application.Current.Shutdown();
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
            else if(msg.Notification != "ToDialog" && msg.Notification != "DialogSave")
            {
                Hide();
            }                
        }

        private void CPRTextbox_GotFocus(object sender, RoutedEventArgs e)
        {
            FindPatientBtn.IsDefault = true;
        }

        private void CPRTextbox_LostFocus(object sender, RoutedEventArgs e)
        {
            FindPatientBtn.IsDefault = false;
        }

        private void chosenPatient_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Filemanagement.ReadBirthRecords((Patient)chosenPatient.SelectedItem);
            Messenger.Default.Send<Employee>((Employee)chosenPatient.Tag, "Employee");
            Messenger.Default.Send<Patient>((Patient)chosenPatient.SelectedItem, "Patient");            
            Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToPatient"));

        }
    }
}
