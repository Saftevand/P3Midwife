using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
        public RelayCommand OpenRecordCommand { get; }
        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(PatientCurrent), typeof(Patient), typeof(PatientViewModel));
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(PatientViewModel));
        public static DependencyProperty SelectedRecord = DependencyProperty.Register(nameof(RecordSelected), typeof(Record), typeof(PatientViewModel)); 
        private ObservableCollection<Patient> _children = new ObservableCollection<Patient>();
        private ObservableCollection<Record> _records = new ObservableCollection<Record>();

        public ObservableCollection<Patient> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public ObservableCollection<Record> Records
        {
            get { return _records; }
            set { _records = value; }
        }

        public Record RecordSelected
        {
            get { return (Record)this.GetValue(SelectedRecord); }
            set { this.SetValue(SelectedRecord, value); }
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
                    Records.Clear();
                    //Records.AddRange(PatientCurrent.RecordList.Where(x => !Records.Contains(x)));
                    Records.AddRange(PatientCurrent.RecordList);

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
            //Messenger.Default.Register<Record>(this, "FromPatientToRecordWithOldRecord", (OldRecord) =>
            //{
            //    Messenger.Default.Send(new NotificationMessage("ToRecord"));
            //    Messenger.Default.Send(PatientCurrent, "PatientToRecordView");
            //    Messenger.Default.Send(CurrentEmployee, "EmployeetoRecordView");
            //    Messenger.Default.Send((OldRecord), "NewRecordToRecordView");
            //});
            this.LogOutCommand = new RelayCommand(parameter =>
            {
                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.BackCommand = new RelayCommand(Parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ToHome"));
                Messenger.Default.Send<Employee>(CurrentEmployee, "ReturnEmployee");
            });
            this.AdmitPatientCommand = new RelayCommand(Parameter =>
            {
                CurrentEmployee.CurrentPatients.Add(PatientCurrent);
            });
            this.DischargePatientCommand = new RelayCommand(Parameter =>
            {
                if (CurrentEmployee.CurrentPatients.Contains(PatientCurrent))
                {
                    CurrentEmployee.CurrentPatients.Remove(PatientCurrent);
                }
            });
            this.CreateRecordCommand = new RelayCommand(parameter =>
            {
                Record tempRecord = new Record(PatientCurrent);
                PatientCurrent.RecordList.Add(tempRecord);
                new P3_Midwife.Views.RecordWindow(tempRecord);
                //new P3_Midwife.Views.FinalRecordWindow(tempRecord);
                Messenger.Default.Send(PatientCurrent, "PatientToRecordView");
                Messenger.Default.Send(CurrentEmployee, "EmployeetoRecordView");
                Messenger.Default.Send(tempRecord, "NewRecordToRecordView");
                Messenger.Default.Send(new NotificationMessage("ToRecord"));
            });
            //this.OpenRecordCommand = new RelayCommand(parameter =>
            //{
            //    if (RecordSelected.IsActive == true)
            //    {
            //        Messenger.Default.Send(new NotificationMessage("ToRecord"));
            //    }
            //    else
            //    {
            //        Messenger.Default.Send(new NotificationMessage("ToFinalRecord"));
            //    }
            //    Messenger.Default.Send(PatientCurrent, "PatientToRecordView");
            //    Messenger.Default.Send(CurrentEmployee, "EmployeetoRecordView");
            //    Messenger.Default.Send(RecordSelected, "NewRecordToRecordView");
            //});
        }
    }
}
