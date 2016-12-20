using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace P3_Midwife
{
    public partial class MainWindow : Window
    {
        private HomeScreen HomeScreenView = null;

        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            Username.Focus();
            Show();
            Closing += ClosingHandler;
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            Hide();
            if (msg.Notification == "FromLogInToHome")
            {
                if (HomeScreenView == null)
                {
                    HomeScreenView = new HomeScreen();
                }
                else
                    HomeScreenView.Show();
            }
            else if (msg.Notification == "LogOut")
            {
                Password.Clear();
                Username.IsReadOnly = true;
                btnLogOut.Visibility = Visibility.Visible;
                Show();
            }           
        }
        #region EventHandling
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }

        private void ClosingHandler(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
    }
}
