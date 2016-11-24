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
        public RelayCommand AddPatientComand { get; }
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(HomeScreenViewModel));
        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(CurrentPatient), typeof(Patient), typeof(HomeScreenViewModel));
        public static DependencyProperty CPRProperty = DependencyProperty.Register(nameof(CPR), typeof(string), typeof(HomeScreenViewModel));
        public static DependencyProperty CPREnteredProperty = DependencyProperty.Register(nameof(CPREntered), typeof(string), typeof(HomeScreenViewModel));
        public ObservableCollection<Patient> _currentPatients = new ObservableCollection<Patient>();

        public ObservableCollection<Patient> CurrentPatients
        {
            get { return _currentPatients; }
        }

        public string CPREntered
        {
            get { return (string)this.GetValue(CPREnteredProperty); }
            set { this.SetValue(CPREnteredProperty, value); }
        }

        public Employee CurrentEmployee
        {
            get { return (Employee)this.GetValue(EmployeeProperty); }
            set { this.SetValue(EmployeeProperty, value);  }
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
            return _patientList.Find(x => x.CPR == CPR);
        }

        //Employee CurrentEmp;

        public HomeScreenViewModel()
        {
            Messenger.Default.Register<Employee>(this, (Emp) =>
            {                
                CurrentEmployee = Emp;

                if (CurrentEmployee.GetType() == typeof(Midwife))
                {
                    foreach (Patient patient in CurrentEmployee.CurrentPatients)
                    {
                        _currentPatients.Add(patient);
                    }
                }
            });
            this.LogOutCommand = new RelayCommand(parameter =>
            {               
                CurrentEmployee = null;
                Messenger.Default.Send(new NotificationMessage("ShowMainView"));              
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.FindPatientCommand = new RelayCommand(parameter => { FindPatient(CPR); });
            this.OpenAddPatientCommand = new RelayCommand(parameter =>
                {
                    Messenger.Default.Send(new NotificationMessage("AddPatientMsg"));
                }
            );
            this.AddPatientComand = new RelayCommand(parameter =>
                {
<<<<<<< HEAD
                    if (Ward.Patients.Find(x => x.CPR == CPREntered) != null)
                    {
                        CurrentEmployee.CurrentPatients.Add(Ward.Patients.Find(x => x.CPR == CPREntered));
                    }
                }
=======
                    //_currentPatients.Add();
                }            
>>>>>>> f4caf03c6609eb6879420abeac6a5781b920b5ae
            );
            
        }
    }
}
