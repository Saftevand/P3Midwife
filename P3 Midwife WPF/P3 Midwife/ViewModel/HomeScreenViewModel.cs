﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;

namespace P3_Midwife
{
    public class HomeScreenViewModel : DependencyObject, INotifyPropertyChanged
    {

        private int AutoLogoutTimer = 30000;

        private List<Patient> _patientList;
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand OpenTextWindow { get; }
        public RelayCommand FindPatientCommand { get; }
        public RelayCommand GetCurrentPatientList { get; }
        public RelayCommand OpenAddPatientCommand { get; }
        public RelayCommand OpenPatientCommand { get; }
        public RelayCommand OpenPatientOnClick { get; }
        public RelayCommand PatientSelected { get; private set; }
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(HomeScreenViewModel));
        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(CurrentPatient), typeof(Patient), typeof(HomeScreenViewModel));
        public static DependencyProperty CPRProperty = DependencyProperty.Register(nameof(CPR), typeof(string), typeof(HomeScreenViewModel));
        public static DependencyProperty SelectedPatientProperty = DependencyProperty.Register(nameof(SelectedPatient), typeof(Patient), typeof(HomeScreenViewModel));
        public ObservableCollection<Patient> _currentPatients = new ObservableCollection<Patient>();

        public event PropertyChangedEventHandler PropertyChanged;

        //Userinactive der ikke virker bare lad det stå her lidt
        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        // Struct we'll need to pass to the function
        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        BackgroundWorker bw = new BackgroundWorker();

        private int GetLastInput()
        {
            int systemUptime = Environment.TickCount;
            int LastInputTicks = 0;
            int IdleTicks = 0;

            // Set the struct
            LASTINPUTINFO LastInputInfo = new LASTINPUTINFO();
            LastInputInfo.cbSize = (uint)Marshal.SizeOf(LastInputInfo);
            LastInputInfo.dwTime = 0;

            // If we have a value from the function
            if (GetLastInputInfo(ref LastInputInfo))
            {
                // Get the number of ticks at the point when the last activity was seen
                LastInputTicks = (int)LastInputInfo.dwTime;
                // Number of idle ticks = system uptime ticks - number of ticks at last input
                IdleTicks = systemUptime - LastInputTicks;
            }
            return IdleTicks / 1000;
        }

        private bool Cancel = false;

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {                                
                if (GetLastInput() > AutoLogoutTimer)
                {
                    Cancel = true;                    
                    e.Cancel = true;
                    break;
                }                
            }
        }       

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            if (e.Cancelled == true)
            {
                if (Cancel == true)
                {
                    Cancel = false;
                    Messenger.Default.Send(new NotificationMessage("LogOut"));
                    //logoutCommand();
                }
                bw.CancelAsync();
            }
        }

        void bw_StartWorker(NotificationMessage msg)
        {
            if (msg.Notification == "StartWorker")
            {
                bw.RunWorkerAsync();
            }
        }

        public void logoutCommand()
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        public ObservableCollection<Patient> CurrentPatients
        {
            get { return _currentPatients; }
        }            

        public Employee CurrentEmployee
        {
            get { return (Employee)this.GetValue(EmployeeProperty); }
            set
            {
                if (value.CurrentPatients != null)
                {
                    foreach (Patient item in value.CurrentPatients)
                    {
                        if (!_currentPatients.Contains(item))
                        {
                            _currentPatients.Add(item);
                        }
                    }
                }
                this.SetValue(EmployeeProperty, value);
            }
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
            }
        }

        private void OnPropertyChanged(string info)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }        

        public HomeScreenViewModel()
        {
            Messenger.Default.Register<Employee>(this, "ActiveUser", (ActiveUser) => { CurrentEmployee = ActiveUser; });
            Messenger.Default.Register<Employee>(this, "ReturnEmployee", (ActiveUser) => { CurrentEmployee = ActiveUser; });
            Messenger.Default.Register<NotificationMessage>(this, bw_StartWorker);

            this.LogOutCommand = new RelayCommand(parameter =>
            {
                logoutCommand();
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.FindPatientCommand = new RelayCommand(parameter => 
            {
                SelectedPatient = FindPatient(CPR);
                if (SelectedPatient != null)
                {
                    Messenger.Default.Send<Patient>(SelectedPatient, "Patient");
                    Messenger.Default.Send<Employee>(CurrentEmployee, "Employee");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToPatient"));
                }
                else
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("NoCPRInput"));
                }
            });
            this.OpenAddPatientCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ToDialog"));
                Messenger.Default.Send<Employee>(CurrentEmployee, "Employee");
            });
            this.OpenPatientCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send<Patient>(SelectedPatient, "Patient");
                Messenger.Default.Send<Employee>(CurrentEmployee, "Employee");
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToPatient"));
            });
            this.OpenPatientOnClick = new RelayCommand(parameter =>
            {                                               
                if (SelectedPatient != null)
                {
                    Filemanagement.ReadBirthRecords(SelectedPatient);
                    Messenger.Default.Send<Patient>(SelectedPatient, "Patient");
                    Messenger.Default.Send<Employee>(CurrentEmployee, "Employee");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToPatient"));
                }
            });
            bw.RunWorkerAsync();
            //bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }
    }
}