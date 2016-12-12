using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace P3_Midwife.Views
{
    /// <summary>
    /// Description for ChildInfoDialogWindow.
    /// </summary>
    public partial class ChildInfoDialogWindow : BaseWindow
    {
        /// <summary>
        /// Initializes a new instance of the ChildInfoDialogWindow class.
        /// </summary>
        public ChildInfoDialogWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
        }
        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (msg.Notification == "NewChildDialog")
            {
                this.Show();
            }

            else if (msg.Notification == "SaveChildBasic")
            {
                this.Close();
            }
            else
            {
                this.Hide();
            }

        }

    }
}