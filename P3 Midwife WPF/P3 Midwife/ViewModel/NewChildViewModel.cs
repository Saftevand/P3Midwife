using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P3_Midwife.ViewModel
{
    public class NewChildViewModel : DependencyObject
    {

        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand BackCommand { get; }
        public static DependencyProperty PropertyPatient = DependencyProperty.Register(nameof(CurrentPatient), typeof(Patient), typeof(RecordViewModel));
        public static DependencyProperty PropertyEmployee = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(RecordViewModel));
        
        public Patient CurrentPatient
        {
            get { return (Patient)this.GetValue(PropertyPatient); }
            set { this.SetValue(PropertyPatient, value); }
        }
        public Employee CurrentEmployee
        {
            get { return (Employee)this.GetValue(PropertyEmployee); }
            set { this.SetValue(PropertyEmployee, value); }
        }

        public NewChildViewModel()
        {
            Messenger.Default.Register<Patient>(this, "PatientToNewChildView", (ActivePatient) => { CurrentPatient = ActivePatient; });
            Messenger.Default.Register<Employee>(this, "EmployeetoNewChildView", (ActiveEmployee) => { CurrentEmployee = ActiveEmployee; });

            this.LogOutCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ShowLoginView"));
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.BackCommand = new RelayCommand(Parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ReturnToRecordView"));
            });
        }
    }
}
