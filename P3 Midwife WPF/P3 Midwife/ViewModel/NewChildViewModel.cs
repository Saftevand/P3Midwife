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
        public static DependencyProperty PropertyPatient = DependencyProperty.Register(nameof(CurrentPatient), typeof(Patient), typeof(NewChildViewModel));
        public static DependencyProperty PropertyRecord = DependencyProperty.Register(nameof(CurrentRecord), typeof(Record), typeof(NewChildViewModel));
        public static DependencyProperty PropertyNewChild = DependencyProperty.Register(nameof(CurrentNewChild), typeof(Patient), typeof(NewChildViewModel));
        public static DependencyProperty PropertyEmployee = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(NewChildViewModel));
        public static DependencyProperty PropertyRoom = DependencyProperty.Register(nameof(CurrentRoom), typeof(DeliveryRoom), typeof(NewChildViewModel));
        

        public Record CurrentRecord
        {
            get { return (Record)this.GetValue(PropertyRecord); }
            set { this.SetValue(PropertyRecord, value); }
        }
        public Patient CurrentNewChild
        {
            get { return (Patient)this.GetValue(PropertyNewChild); }
            set { this.SetValue(PropertyNewChild, value); }
        }
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
        public DeliveryRoom CurrentRoom
        {
            get { return Ward.DeliveryRooms.Find(x => x.PatientsInRoom.Find(z => z.CPR == CurrentPatient.CPR) != null); }
        }

        public NewChildViewModel()
        {
            Messenger.Default.Register<Patient>(this, "PatientToNewChildView", (ActivePatient) => { CurrentPatient = ActivePatient; });
            Messenger.Default.Register<Employee>(this, "EmployeetoNewChildView", (ActiveEmployee) => { CurrentEmployee = ActiveEmployee; });
            CurrentNewChild = new Patient();


            this.LogOutCommand = new RelayCommand(parameter =>
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.BackCommand = new RelayCommand(Parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ToRecord"));
                Messenger.Default.Send(CurrentPatient, "PatientToRecordView");
                Messenger.Default.Send(CurrentEmployee, "EmployeetoRecordView");
            });
        }
    }
}
