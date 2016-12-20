using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using System.Windows;

namespace P3_Midwife.ViewModel
{
    class DialogWindowViewModel : DependencyObject
    {
        public RelayCommand AddPatientComand { get; }
        public RelayCommand Cancel { get; }
        public static DependencyProperty CPREnteredProperty = DependencyProperty.Register(nameof(CPREntered), typeof(string), typeof(DialogWindowViewModel));
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(DialogWindowViewModel));

        #region Properties
        public Employee CurrentEmployee
        {
            get { return (Employee)this.GetValue(EmployeeProperty); }
            set { this.SetValue(EmployeeProperty, value); }
        }

        public string CPREntered
        {
            get { return (string)this.GetValue(CPREnteredProperty); }
            set { this.SetValue(CPREnteredProperty, value); }
        }
        #endregion

        public DialogWindowViewModel()
        {
            //Command to add a patient if the userinput matches a CPR number
            this.AddPatientComand = new RelayCommand(parameter =>
            {
                if (Ward.Patients.Find(x => x.CPR == CPREntered) != null)
                {
                    (CurrentEmployee as Midwife).AdmitPatient(CurrentEmployee.FindPatient(CPREntered));
                    Filemanagement.ReadBirthRecords(CurrentEmployee.FindPatient(CPREntered));
                    MessageBox.Show(Ward.Patients.Find(x => x.CPR == CPREntered).Name + " er blevet tilføjet");
                    Messenger.Default.Send<Employee>(CurrentEmployee, "ReturnEmployee");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToHome"));
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("DialogSave"));
                }
                else
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("NoPersonWithCPR"));
                }

            });
            //Comman to return to the Homescreen, in case the user didn't want to add a patient anyway
            this.Cancel = new RelayCommand(parameter =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToHome"));
            });
            Messenger.Default.Register<Employee>(this, "Employee", (ActiveUser) => { CurrentEmployee = ActiveUser; });

        }
    }
}
