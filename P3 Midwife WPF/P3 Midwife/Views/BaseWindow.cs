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
            base.OnClosing(e);
            if (cancel == true)
            {
                e.Cancel = true;
            }
            else
            {
                //Filemanagement.SaveToDatabase();
                Application.Current.Shutdown();
            }
            cancel = false;
        }
    }
}
