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
            isNotClosed = false;
            this.thisID = RecordShow.ThisRecordID;
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
            if (thisID == CurrentRecord.ThisRecordID)
            {
                if (msg.Notification == "ToRecord" && !isNotClosed && CurrentRecord.IsActive)
                {
                    Show();
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
            }
            else if (msg.Notification != "NewChildDialog")
            {
                this.Hide();
            }
        }
    }
}
