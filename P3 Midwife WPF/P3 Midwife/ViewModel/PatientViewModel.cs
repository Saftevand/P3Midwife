﻿using System;
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
        #region Variables
        #region Relaycommands
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand BackCommand { get; }
        public RelayCommand CreateRecordCommand { get; }
        public RelayCommand OpenRecordCommand { get; }
        #endregion
        #region DependencyProperties
        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(PatientCurrent), typeof(Patient), typeof(PatientViewModel));
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(PatientViewModel));
        public static DependencyProperty SelectedRecord = DependencyProperty.Register(nameof(RecordSelected), typeof(Record), typeof(PatientViewModel));
        public static DependencyProperty PriorBirthComplicationsProperty = DependencyProperty.Register(nameof(PriorBirthComplications), typeof(bool), typeof(PatientViewModel));
        #endregion
        #region ObservableCollections
        private ObservableCollection<Patient> _children = new ObservableCollection<Patient>();
        private ObservableCollection<Record> _records = new ObservableCollection<Record>();
        #endregion
        #endregion

        #region Properties
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

        public bool PriorBirthComplications
        {
            get { return (bool)this.GetValue(PriorBirthComplicationsProperty); }
            set { this.SetValue(PriorBirthComplicationsProperty, value); }
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
                    this.SetValue(PatientProperty, value);
                    PriorBirthComplications = CurrentEmployee.PriorBirthComplications(PatientCurrent);
                    foreach (var item in value.Children)
                    {
                        if (Children.Contains(item) == false)
                        {
                            Children.Add(item);
                        }
                    }
                    Records.Clear();
                    Records.AddRange(PatientCurrent.RecordList);

                }
            }
        } 

        public Employee CurrentEmployee
        {
            get { return (Employee)this.GetValue(EmployeeProperty); }
            set { this.SetValue(EmployeeProperty, value); }
        }
        #endregion

        public PatientViewModel()
        {
            Messenger.Default.Register<Employee>(this, "Employee", (ActiveUser) => { CurrentEmployee = ActiveUser; });
            Messenger.Default.Register<Patient>(this, "Patient", (ActivePatient) => { PatientCurrent = ActivePatient;});
            //Command to log the user out of the system
            this.LogOutCommand = new RelayCommand(parameter =>
            {
                Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            });
            //Command to close to program
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            //Command to return to the previous view
            this.BackCommand = new RelayCommand(Parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ToHome"));
                Messenger.Default.Send<Employee>(CurrentEmployee, "ReturnEmployee");
            });
            //Command to create a new record, and open a record view to that record
            this.CreateRecordCommand = new RelayCommand(parameter =>
            {
                if (!CurrentEmployee.CurrentPatients.Contains(PatientCurrent))
                {
                    (CurrentEmployee as Midwife).AdmitPatient(PatientCurrent);
                }
                Record tempRecord = new Record(PatientCurrent);
                PatientCurrent.RecordList.Add(tempRecord);
                new P3_Midwife.Views.RecordWindow(tempRecord);
                Messenger.Default.Send(tempRecord, "NewRecordToRecordView");
                Messenger.Default.Send(CurrentEmployee, "EmployeetoRecordView");
                Messenger.Default.Send(PatientCurrent, "PatientToRecordView");
                Messenger.Default.Send(new NotificationMessage("ToRecord"));
            });

        }
    }
}
