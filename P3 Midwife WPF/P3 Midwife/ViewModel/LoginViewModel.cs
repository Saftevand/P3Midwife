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
            _employees.Add(new Employee(1, "Gitte", "kode123", 42660666, "palminde@hotmail.com"));
            this.LoginCommand = new RelayCommand(parameter =>
            {
                if (_employees.Exists(x => x.Email.ToUpper() == Email.ToUpper() && x.Password.Equals(Password)))
                {
                    Employee SendEmp = _employees.Find(x => x.Email.ToUpper() == Email.ToUpper() && x.Password.Equals(Password));
                    HomeScreenViewModel HSViewModel = new HomeScreenViewModel();
                    Messenger.Default.Send(new NotificationMessage("ShowHomeScreen"));
                    Messenger.Default.Send(SendEmp);
                    
                }
                else
                {
                    MessageBox.Show("Ugyldigt login", "Fejl");
                }
            });
        }
        
    }
}
