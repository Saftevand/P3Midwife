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
        public RelayCommand CreateRecordCommand { get; }
        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(PatientCurrent), typeof(Patient), typeof(PatientViewModel));
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(PatientViewModel));
        public static DependencyProperty SelectedChildProperty = DependencyProperty.Register(nameof(SelectedChild), typeof(Patient), typeof(PatientViewModel));
        private ObservableCollection<Patient> _children = new ObservableCollection<Patient>();

        public ObservableCollection<Patient> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public Patient SelectedChild
        {
            get { return (Patient)this.GetValue(SelectedChildProperty); }
            set { this.SetValue(SelectedChildProperty, value); }
        }

        public Patient PatientCurrent
        {
            get { return (Patient)this.GetValue(PatientProperty); }
            set
            {
                if (value!= null)
                {
                    foreach (var item in value.Children)
                    {
                        Children.Add(item);
                    }
                    { this.SetValue(PatientProperty, value); }
                } 
            }
        } 

        public Employee CurrentEmployee
        {
            get { return (Employee)this.GetValue(EmployeeProperty); }
            set { this.SetValue(EmployeeProperty, value); }
        }

        public PatientViewModel()
        {
            Messenger.Default.Register<Patient>(this, "Patient", (ActivePatient) => { PatientCurrent = ActivePatient; });
            Messenger.Default.Register<Employee>(this, "Employee", (ActiveUser) => { CurrentEmployee = ActiveUser; });
            this.LogOutCommand = new RelayCommand(parameter =>
            {                
                Messenger.Default.Send(new NotificationMessage("ShowMainViewPatient"));
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.BackCommand = new RelayCommand(Parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ShowHomeView"));
                Messenger.Default.Send<Employee>(CurrentEmployee, "ReturnEmployee");
            });
            this.AdmitPatientCommand = new RelayCommand(Parameter =>
            {
                // TODO - Needs to be implemented
            });
            this.DischargePatientCommand = new RelayCommand(Parameter =>
            {
                // TODO - Needs to be implemented
            });
            this.CreateRecordCommand = new RelayCommand(parameter =>
            {
                PatientCurrent.RecordList.Add(new Record(PatientCurrent));
                Messenger.Default.Send(new NotificationMessage("ShowRecordView"));
                Messenger.Default.Send(PatientCurrent, "PatientToRecordView");
                Messenger.Default.Send(CurrentEmployee, "EmployeetoRecordView");
            });
        }
    }
}
