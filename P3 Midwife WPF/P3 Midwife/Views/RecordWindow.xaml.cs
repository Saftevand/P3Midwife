using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;


namespace P3_Midwife.Views
{
    public partial class RecordWindow : Window
    {
        public RecordWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);

        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (msg.Notification == "FromRecordToLogIn")
            {
                var MainWindow = new MainWindow();
                MainWindow.Show();
                this.Close();
            }
            else if (msg.Notification == "FromRecordToPatient")
            {
                var PatientView = new PatientWindow();
                PatientView.Show();
                this.Close();
            }
            else if (msg.Notification == "FromRecordToCNewChild")
            {
                var NewChildView = new NewChildWindow();
                NewChildView.Show();
                this.Close();
            }
        }
    }
}
