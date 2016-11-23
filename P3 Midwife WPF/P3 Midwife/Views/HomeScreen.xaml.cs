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

namespace P3_Midwife
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Window
    {
        public HomeScreen()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (msg.Notification == "ShowMainView")
            {
                var MainWindow = new MainWindow();
                MainWindow.Show();
                this.Close();
            }
            if (msg.Notification == "AddPatientMsg")
            {
                
            }
        }
    }
}
