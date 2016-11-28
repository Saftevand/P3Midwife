using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace P3_Midwife
{
    public class LoginViewModel : DependencyObject
    {
        private List<Employee> _employees = new List<Employee>();
        public RelayCommand LoginCommand { get; }
        public static DependencyProperty EmailProperty = DependencyProperty.Register(nameof(Email), typeof(string), typeof(LoginViewModel));
        public string Email
        {
            get { return (string)this.GetValue(EmailProperty); }
            set { this.SetValue(EmailProperty, value); }
        }
        public string Password {private get; set; }



        public LoginViewModel()
        {
            //_employees.Add(new Midwife(1, "Gitte", "x", 42660666, "x"));
            //_employees.First().CurrentPatients.Add(new Patient("1234567890", "TestName"));
            //_employees.First().CurrentPatients.Add(new Patient("0987654321", "NameTest"));

            this.LoginCommand = new RelayCommand(parameter =>
            {
                //TODO: Crashes if no email is entered and login pressed or if only numbers are entered
                if (Ward.Employees.Exists(x => x.Email.ToUpper() == Email.ToUpper() && x.Password.Equals(Password)))
                {
                    Employee SendEmp = Ward.Employees.Find(x => x.Email.ToUpper() == Email.ToUpper() && x.Password.Equals(Password));
                    Messenger.Default.Send(new NotificationMessage("ShowHomeScreen"));
                    Messenger.Default.Send(SendEmp,"ActiveUser");
                    

                }
                else
                {
                    MessageBox.Show("Ugyldigt login", "Fejl");
                }
            });
        }
        
    }
}
