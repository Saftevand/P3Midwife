using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace P3_Midwife.Views
{
    public partial class PatientWindow : Window
    {
        #region Variables
        private Employee CurrentEmployee;
        #endregion

        public PatientWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            Messenger.Default.Register<Employee>(this, "Employee", validateUser);
            Closing += ClosingHandler;
        }

        private void show()
        {
            if (CurrentEmployee is SOSU)
            {
                NewRecordBtn.Visibility = Visibility.Hidden;
            }
            Show();
        }

        private void validateUser(Employee emp)
        {
            CurrentEmployee = emp;
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (msg.Notification == "ToPatient")
                show();
            else
                Hide();
        }
        #region EventHandling
        private void ClosingHandler(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Record TempRecord = chosenRecord.SelectedItem as Record;
            if (TempRecord != null)
            {
                if (TempRecord.IsActive == true)
                {
                    Messenger.Default.Send<Employee>((Employee)chosenRecord.Tag, "EmployeetoRecordView");
                    Patient tempPatient = Ward.Patients.Find(x => x.RecordList.Contains((Record)chosenRecord.SelectedItem));
                    Messenger.Default.Send<Record>((Record)chosenRecord.SelectedItem, "NewRecordToRecordView");
                    Messenger.Default.Send<Patient>(tempPatient, "PatientToRecordView");
                    
                    
                    
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToRecord"));
                }
                else if(TempRecord.IsActive == false)
                {
                    Messenger.Default.Send<Employee>((Employee)chosenRecord.Tag, "EmployeetoRecordView");
                    Patient tempPatient = Ward.Patients.Find(x => x.RecordList.Contains((Record)chosenRecord.SelectedItem));
                    Messenger.Default.Send<Patient>(tempPatient, "PatientToRecordView");
                    Messenger.Default.Send<Record>((Record)chosenRecord.SelectedItem, "NewRecordToRecordView");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToFinalRecord"));

                }
            }
        }


        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Patient child = item as Patient;
                if (child.Mother.RecordList.Find(x => x.ChildCPR == child.CPR) != null)
                {
                    Messenger.Default.Send(child.Mother.RecordList.Find(x => x.ChildCPR == child.CPR), "ChildRecordToNewChildView");
                    Messenger.Default.Send(child.Mother, "PatientToNewChildView");
                    Messenger.Default.Send<Employee>((Employee)ChildrenListBox.Tag, "EmployeetoNewChildView");
                    Messenger.Default.Send(child, "ChildToNewChildView");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToNewChild"));
                }
            }
        }
        #endregion
    }
}
