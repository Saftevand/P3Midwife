using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace P3_Midwife
{
    public class LoginViewModel : DependencyObject
    {
        private List<Employee> _employees = new List<Employee>();
        public RelayCommand LoginCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public static DependencyProperty EmailProperty = DependencyProperty.Register(nameof(Email), typeof(string), typeof(LoginViewModel));
        public string Email
        {
            get { return (string)this.GetValue(EmailProperty); }
            set { this.SetValue(EmailProperty, value); }
        }
        private string last = "FromLogInToHome";
        public string Password {private get; set; }

        public LoginViewModel()
        {
            Filemanagement.InitialiseFoldersAndFiles();
            this.LoginCommand = new RelayCommand(parameter =>
            {
                if (Ward.Employees.Exists(x => x.Email.ToUpper() == Email.ToUpper() && x.Password.Equals(Password)))
                {
                    Employee SendEmp = Ward.Employees.Find(x => x.Email.ToUpper() == Email.ToUpper() && x.Password.Equals(Password));
                    Messenger.Default.Send(new NotificationMessage("StartWorker"));
                    Messenger.Default.Send(new NotificationMessage(last));
                    Messenger.Default.Send(SendEmp,"ActiveUser");
                    Messenger.Default.Send(new NotificationMessage("ToHome"));
                }
                else
                {
                    MessageBox.Show("Ugyldigt login", "Fejl");
                }
            });
            this.LogOutCommand = new RelayCommand(parameter =>
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            });
            Messenger.Default.Register<NotificationMessage>(this, setLast);

        }

        private void setLast(NotificationMessage msg)
        {
            if (msg.Notification != "LogOut" && msg.Notification != "StartWorker")
            {
                last = msg.Notification;
            }
        }
    }
}
