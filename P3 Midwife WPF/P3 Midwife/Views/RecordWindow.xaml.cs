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


namespace P3_Midwife.Views
{
    public partial class RecordWindow : BaseWindow
    {
        bool isNotClosed;
        int thisID;
        Patient CurrentPatient;
        Record CurrentRecord;
        Employee CurrentEmployee;
        public RecordWindow(Record RecordShow)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString());
            }
            Messenger.Default.Register<Patient>(this, "PatientToRecordView", patientValidation);
            Messenger.Default.Register<Record>(this, "NewRecordToRecordView", recordValidation);
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            Messenger.Default.Register<Employee>(this, "Employee", validateUser);

            isNotClosed = false;
            this.thisID = RecordShow.ThisRecordID;
        }

        private void show()
        {
            if (CurrentEmployee is SOSU)
            {
                NewChildBtn.Visibility = Visibility.Hidden;
            }
            this.Show();
        }

        private void validateUser(Employee emp)
        {
            CurrentEmployee = emp;
        }

        private void recordValidation(Record _currentRecord)
        {
            CurrentRecord = _currentRecord;
        }

        private void patientValidation(Patient _currentPatient)
        {
            CurrentPatient = _currentPatient;
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {

                if (msg.Notification == "ToRecord" && !isNotClosed && CurrentRecord.IsActive)
                {
                    if(this.thisID == CurrentRecord.ThisRecordID) show();
                }
                else if (msg.Notification == "RecordSave")
                {
                    BaseWindow.cancel = true;
                    this.isNotClosed = true;
                    this.Close();
                }
                else if (msg.Notification == "AccessDenied")
                {
                    MessageBox.Show("Adgang nægtet!");
                }
                else if (msg.Notification != "NewChildDialog")
                {
                    this.Hide();
                }

            else if (msg.Notification != "NewChildDialog")
            {
                this.Hide();
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox send = sender as TextBox;
            if (send.Text == "0")
            {
                send.Clear();
            }
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                Patient child = item as Patient;
                if (child.Mother.RecordList.Find(x => x.IsActive == true) != null)
                {
                    Messenger.Default.Send(child.Mother.RecordList.Find(x => x.IsActive == true), "ChildRecordToNewChildView");
                    Messenger.Default.Send(child.Mother, "PatientToNewChildView");
                    Messenger.Default.Send<Employee>((Employee)ChildrenListBox.Tag, "EmployeetoNewChildView");
                    Messenger.Default.Send(child, "ChildToNewChildView");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToNewChild"));
                }
              
            }
        }

        private void NewChildBtn_Click(object sender, RoutedEventArgs e)
        {
            NewChildBtn.Visibility = Visibility.Hidden;
        }
    }
}
