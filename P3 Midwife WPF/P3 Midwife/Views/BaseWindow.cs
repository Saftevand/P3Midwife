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
using P3_Midwife.Views;
using System.Threading;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace P3_Midwife.Views
{
    public class BaseWindow : Window
    {
        public static bool cancel;
        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Data er muligvis ikke blevet gemt. \n Vil du gemme inden programmet lukkes?", "Gem og afslut", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            base.OnClosing(e);
            if (cancel == true)
            {
                e.Cancel = true;
            }
            cancel = false;
            if (result == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
            else if (result == MessageBoxResult.Yes)
            {
                Messenger.Default.Send<String>("SaveToDatabase", "SaveToDatabase");
            }
            else if(result == MessageBoxResult.Cancel || result == MessageBoxResult.None)
            {
                e.Cancel = true;
            }
        }
        
    }
}
