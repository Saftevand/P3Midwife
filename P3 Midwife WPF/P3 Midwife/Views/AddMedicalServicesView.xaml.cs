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

namespace P3_Midwife.Views
{
    /// <summary>
    /// Interaction logic for AddMedicalServicesView.xaml
    /// </summary>
    public partial class AddMedicalServicesView : Window
    {
        public AddMedicalServicesView()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
        }

        private void NotificationMessageReceived(NotificationMessage message)
        {
            if (message.Notification == "AddMedicalServices")
            {
                Show();
            }
            else if (message.Notification == "CloseMedicalService")
            {
                Hide();
            }
        }
    }
}
