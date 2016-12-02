﻿using GalaSoft.MvvmLight.Messaging;
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

namespace P3_Midwife.Views
{
    public partial class NewChildWindow : Window
    {
        public NewChildWindow()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
        }

        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (msg.Notification == "FromNewChildToRecord")
            {
                var RecordScreenView = new RecordWindow();
                RecordScreenView.Show();
                this.Close();
            }
            else if (msg.Notification == "FromNewChildToLogIn")
            {
                var MainWindow = new MainWindow();
                MainWindow.Show();
                this.Close();
            }
        }
    }
}
