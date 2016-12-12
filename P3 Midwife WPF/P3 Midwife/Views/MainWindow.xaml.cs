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

namespace P3_Midwife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : P3_Midwife.Views.BaseWindow
    {
        HomeScreen HomeScreenView = null;
        public MainWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
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
