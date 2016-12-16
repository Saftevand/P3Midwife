using System;
using System.Windows;
using System.ComponentModel;
using GalaSoft.MvvmLight.Messaging;

namespace P3_Midwife.Views
{
    public partial class FinalRecordWindow : Window
    {
        bool isNotClosed;
        //int thisID;
        Patient CurrentPatient;
        Record CurrentRecord;
        
        public FinalRecordWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<Patient>(this, "PatientToRecordView", patientValidation);
            Messenger.Default.Register<Record>(this, "NewRecordToRecordView", recordValidation);
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            isNotClosed = false;
            Closing += ClosingHandler;
        }
        private void ClosingHandler(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
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
            if (msg.Notification == "ToFinalRecord" && !isNotClosed && CurrentRecord.IsActive == false)
            { 
                Show();
            }
            else
            {
                this.Hide();
            }
        }
    }
}
