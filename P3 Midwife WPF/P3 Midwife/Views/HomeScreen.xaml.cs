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
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;
using P3_Midwife.Views;
using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace P3_Midwife
{ 
    public partial class HomeScreen : Window
    {
        public HomeScreen()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);           
        }     

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (msg.Notification == "FromHomeToDialog")
            {
                var AddPatientDialogWindow = new DialogWindow();
                AddPatientDialogWindow.Show();
            }
            else if (msg.Notification == "FromHomeToPatient")
            {
                var PatientView = new PatientWindow();
                PatientView.Show();
                Hide();
            }
        }

       
    }
}
