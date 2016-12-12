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
using System.Windows.Controls.Primitives;

namespace P3_Midwife.Views
{
    public partial class PatientWindow : BaseWindow
    {
        public PatientWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (msg.Notification == "ToPatient")
                Show();
            else
                Hide();
        }

        //    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //    {
        //        //var item = ((FrameworkElement)e.OriginalSource).DataContext as Track;
        //        //if (item != null)
        //        //{
        //            Messenger.Default.Send(sender, "FromPatientToRecordWithOldRecord");
        //        //}
        //    }
        //}
    }
}
