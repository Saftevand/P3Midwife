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
        public static DependencyProperty CPREnteredProperty = DependencyProperty.Register(nameof(CPREntered), typeof(string), typeof(DialogWindowViewModel));
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(CurrentEmployee), typeof(Employee), typeof(DialogWindowViewModel));



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



        public DialogWindowViewModel()
        {
            this.AddPatientComand = new RelayCommand(parameter =>
            {
                if (Ward.Patients.Find(x => x.CPR == CPREntered) != null)
                {
                    Midwife temp = CurrentEmployee as Midwife;
                    temp.AdmitPatient(temp.FindPatient(CPREntered));
                    MessageBox.Show(Ward.Patients.Find(x => x.CPR == CPREntered).Name + " er blevet tilføjet");
                    Messenger.Default.Send<Employee>(CurrentEmployee, "ReturnEmployee");
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("DialogSave"));
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToHome"));
                }
                else
                {
                    Messenger.Default.Send<NotificationMessage>(new NotificationMessage("NoPersonWithCPR"));
                }

            });
            Messenger.Default.Register<Employee>(this, "Employee", (ActiveUser) => { CurrentEmployee = ActiveUser; });

        }
    }
}
