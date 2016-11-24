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

        //public static DependencyProperty PatientNameProperty = DependencyProperty.Register(nameof(PatientName), typeof(Patient), typeof(HomeScreenViewModel));
        //public static DependencyProperty PatientCPRProperty = DependencyProperty.Register(nameof(PatientCPR), typeof(Patient), typeof(HomeScreenViewModel));
        //public static DependencyProperty PatientGenderProperty = DependencyProperty.Register(nameof(PatientGender), typeof(Patient), typeof(HomeScreenViewModel));
        //public static DependencyProperty PatientAgeProperty = DependencyProperty.Register(nameof(PatientAge), typeof(Patient), typeof(HomeScreenViewModel));
        //public ObservableCollection<Patient> _patientChildren = new ObservableCollection<Patient>();
        public static DependencyProperty PatientNameProperty = DependencyProperty.Register(nameof(PatientCurrent), typeof(Patient), typeof(HomeScreenViewModel));

        public Patient PatientCurrent
        {
            get { return (Patient)this.GetValue(PatientNameProperty); }
    
            set { this.SetValue(PatientNameProperty, value); }
        } 


//public string PatientName
//{
//    get {return (string)this.GetValue(PatientNameProperty); }
//    set { this.SetValue(PatientNameProperty, value); }
//}

//public string PatientCPR
//{
//    get { return (string)this.GetValue(PatientCPRProperty); }
//    set { this.SetValue(PatientCPRProperty, value); }
//}

//public string PatientGender
//{
//    get { return (string)this.GetValue(PatientGenderProperty); }
//    set { this.SetValue(PatientGenderProperty, value); }
//}

//public int PatientAge
//{
//    get { return (int)this.GetValue(PatientAgeProperty); }
//    set { this.SetValue(PatientAgeProperty, value); }
//}

//public ObservableCollection<Patient> PatientChildren
//{
//    get { return _patientChildren; }
//}


public PatientViewModel()
        {
            Messenger.Default.Register<Patient>(this, (SelectedPatient) =>
                {
                    PatientCurrent = SelectedPatient;
                });
            this.LogOutCommand = new RelayCommand(parameter =>
            {
                //CurrentEmployee = null; //Fixe
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
        }
    }
}
