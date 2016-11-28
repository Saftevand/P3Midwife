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
        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(PatientCurrent), typeof(Patient), typeof(PatientViewModel));
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(PatientViewModel));

        public Patient PatientCurrent
        {
            get { return (Patient)this.GetValue(PatientProperty); }
            set { this.SetValue(PatientProperty, value); }
        } 

        public Employee CurrentEmployee
        {
            get { return (Employee)this.GetValue(EmployeeProperty); }
            set { this.SetValue(EmployeeProperty, value); }
        }

        public PatientViewModel()
        {
            Messenger.Default.Register<Patient>(this, "Patient", (ActivePatient) => { PatientCurrent = ActivePatient; });
            Messenger.Default.Register<Employee>(this, "ReturnEmployee", (ActiveUser) => { CurrentEmployee = ActiveUser; });
            this.LogOutCommand = new RelayCommand(parameter =>
            {
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
