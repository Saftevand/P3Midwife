using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;

namespace P3_Midwife.Views
{
    /// <summary>
    /// Description for ChildInfoDialogWindow.
    /// </summary>
    public partial class ChildInfoDialogWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the ChildInfoDialogWindow class.
        /// </summary>
        public ChildInfoDialogWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            Closing += ClosingHandler;
        }
        private void ClosingHandler(object sender, CancelEventArgs e)
        {
            e.Cancel = false;
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

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ConfirmBtn.IsEnabled = true;
        }
    }
}