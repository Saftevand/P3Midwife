using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace P3_Midwife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HomeScreen HomeScreenView = null;
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);          
            show();
            Closing += ClosingHandler;
        }
        private void ClosingHandler(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void show()
        {
            Username.Focus();
            Show();
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

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
        }
    }
}
