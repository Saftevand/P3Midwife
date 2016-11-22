using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace P3_Midwife
{
    public class HomeScreenViewModel : DependencyObject
    {
        private List<Patient> _patientList;
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand OpenTextWindow { get; }
        public RelayCommand FindPatientCommand { get; }
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(HomeScreenViewModel));
        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(CurrentPatient), typeof(Patient), typeof(HomeScreenViewModel));
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

        public Patient FindPatient(string CPR)
        {
            return _patientList.Find(x => x.CPR == CPR);
        }

        Employee CurrentEmp;

        public HomeScreenViewModel()
        {
            Messenger.Default.Register<Employee>(this, (Emp) =>
            {
                CurrentEmp = Emp;
            });
            this.LogOutCommand = new RelayCommand(parameter =>
            {               
                CurrentEmp = null;                
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                //Current user til null
            });
            //this.FindPatientCommand = new RelayCommand(parameter => { FindPatient(); });

           // this.OpenTextWindow = new RelayCommand(parameter => { })
        }
    }
}
