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
    public partial class DialogWindow : Window
    {
        bool isNotClosed;
        public DialogWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            isNotClosed = false;
            TextBoxEnteredCPR.Focus();
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (msg.Notification == "ToDialog" && !isNotClosed)
            {
                Show();
            }
            else if (msg.Notification == "NoPersonWithCPR")
            {
                MessageBox.Show("Der er ingen personer med det CPR nummer");
            }
            else if (msg.Notification == "DialogSave")
            {
                Close();
                isNotClosed = true;
                new DialogWindow();
            }
            else
                Hide();            
        }
    }
}
