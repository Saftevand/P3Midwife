﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace P3_Midwife
{
    public class HomeScreenViewModel : DependencyObject
    {
        private List<Patient> _patientList;
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand OpenTextWindow { get; }
        public RelayCommand FindPatientCommand { get; }
        public RelayCommand GetCurrentPatientList { get; }
        public RelayCommand OpenAddPatientCommand { get; }
        public RelayCommand OpenPatientCommand { get; }
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(HomeScreenViewModel));
        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(CurrentPatient), typeof(Patient), typeof(HomeScreenViewModel));
        public static DependencyProperty CPRProperty = DependencyProperty.Register(nameof(CPR), typeof(string), typeof(HomeScreenViewModel));
        public static DependencyProperty SelectedPatientProperty = DependencyProperty.Register(nameof(SelectedPatient), typeof(Patient), typeof(HomeScreenViewModel));
        public ObservableCollection<Patient> _currentPatients = new ObservableCollection<Patient>();

        public ObservableCollection<Patient> CurrentPatients
        {
            get { return _currentPatients; }
        }

            

        public Employee CurrentEmployee
        {
            get { return (Employee)this.GetValue(EmployeeProperty); }
            set
            {
                foreach (Patient item in value.CurrentPatients)
                {
                    if (!_currentPatients.Contains(item))
                    {
                        _currentPatients.Add(item);
                    }
                }
                this.SetValue(EmployeeProperty, value);  }
        }

        public Patient CurrentPatient
        {
            get { return (Patient)this.GetValue(PatientProperty); }
            set { this.SetValue(PatientProperty, value); }
        }

        public string CPR
        {
            get { return (string)this.GetValue(CPRProperty); }
            set { this.SetValue(CPRProperty, value); }
        }

        public Patient FindPatient(string CPR)
        {
            return Ward.Patients.Find(x => x.CPR == CPR);
        }

        public Patient SelectedPatient
        {
            get { return (Patient)this.GetValue(SelectedPatientProperty); }
            set
            {
                this.SetValue(SelectedPatientProperty, value);
                Messenger.Default.Send(new NotificationMessage("ShowPatientView"));
                Messenger.Default.Send(SelectedPatient);
            }
        }

        public HomeScreenViewModel()
        {
            Messenger.Default.Register<Employee>(this, "ActiveUser", (ActiveUser) => { CurrentEmployee = ActiveUser; });
            Messenger.Default.Register<Employee>(this, "ReturnEmployee", (ActiveUser) => { CurrentEmployee = ActiveUser; });
            this.LogOutCommand = new RelayCommand(parameter =>
            {
                CurrentEmployee = null;
                Messenger.Default.Send(new NotificationMessage("ShowMainView"));
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.FindPatientCommand = new RelayCommand(parameter => 
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ShowPatientView"));
                Messenger.Default.Send<Patient>(FindPatient(CPR), "Patient");
            });
            this.OpenAddPatientCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("AddPatientMsg"));
                Messenger.Default.Send<Employee>(CurrentEmployee, "Employee");
            });
            this.OpenPatientCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ShowPatientView"));
                Messenger.Default.Send<Patient>(SelectedPatient, "Patient");
            });
            
        }
    }
}
