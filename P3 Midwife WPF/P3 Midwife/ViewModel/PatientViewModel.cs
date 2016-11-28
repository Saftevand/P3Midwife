using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;


namespace P3_Midwife.ViewModel
{
    public class PatientViewModel : DependencyObject
    {
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand BackCommand { get; }
        public RelayCommand AdmitPatientCommand { get; }
        public RelayCommand DischargePatientCommand { get; }
        public static DependencyProperty PatientNameProperty = DependencyProperty.Register(nameof(PatientCurrent), typeof(Patient), typeof(PatientViewModel));

        public Patient PatientCurrent
        {
            get { return (Patient)this.GetValue(PatientNameProperty); }
            set { this.SetValue(PatientNameProperty, value); }
        } 

        public PatientViewModel()
        {
            Messenger.Default.Register<Patient>(this, "Patient", (ActivePatient) => { PatientCurrent = ActivePatient; });
            this.LogOutCommand = new RelayCommand(parameter =>
            {
                //CurrentEmployee = null; //Fixe
                Messenger.Default.Send(new NotificationMessage("ShowMainView"));
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.BackCommand = new RelayCommand(Parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ShowHomeView"));
            });
            this.AdmitPatientCommand = new RelayCommand(Parameter =>
            {
                // TODO - Needs to be implemented
            });
            this.DischargePatientCommand = new RelayCommand(Parameter =>
            {
                // TODO - Needs to be implemented
            });
        }
    }
}
