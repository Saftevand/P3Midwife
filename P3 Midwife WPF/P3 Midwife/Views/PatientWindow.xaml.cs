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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls.Primitives;

namespace P3_Midwife.Views
{
    public partial class PatientWindow : BaseWindow
    {
        Employee CurrentEmployee;

        public PatientWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            Messenger.Default.Register<Employee>(this, "Employee", validateUser);

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

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Record TempRecord = chosenRecord.SelectedItem as Record;
            if (TempRecord != null)
            {
                if (TempRecord.IsActive == true)
                {
                    Messenger.Default.Send<Record>((Record)chosenRecord.SelectedItem, "NewRecordToRecordView");
                    Messenger.Default.Send<Employee>((Employee)chosenRecord.Tag, "EmployeetoRecordView");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToRecord"));
                }
                else if(TempRecord.IsActive == false)
                {
                    Messenger.Default.Send<Record>((Record)chosenRecord.SelectedItem, "NewRecordToRecordView");
                    Messenger.Default.Send<Employee>((Employee)chosenRecord.Tag, "EmployeetoRecordView");
                    Patient tempPatient = Ward.Patients.Find(x => x.RecordList.Contains(chosenRecord.SelectedItem));
                    Messenger.Default.Send<Patient>(tempPatient, "PatientToRecordView");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToFinalRecord"));

                }
            }
        }
    }
}
