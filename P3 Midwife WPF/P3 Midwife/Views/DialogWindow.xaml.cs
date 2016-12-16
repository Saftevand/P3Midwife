using System;
using System.Windows;
using System.ComponentModel;
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
            Closing += ClosingHandler;
        }

        private void ClosingHandler(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
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
