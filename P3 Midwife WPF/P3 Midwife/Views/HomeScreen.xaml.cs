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


namespace P3_Midwife
{ 
    public partial class HomeScreen : Window
    {
        int count = 0;
        private int AutoLogoutTimer = 100000;
        public HomeScreen()
        {
            InitializeComponent();
            Messenger.Default.Register<NotificationMessage>(this, NotificationMessageRecieved);
            bw.RunWorkerAsync();
            //bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        [DllImport("user32.dll")]
        static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        // Struct we'll need to pass to the function
        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        BackgroundWorker bw = new BackgroundWorker();

        private int GetLastInput()
        {
            int systemUptime = Environment.TickCount;
            // The tick at which the last input was recorded
            int LastInputTicks = 0;
            // The number of ticks that passed since last input
            int IdleTicks = 0;

            // Set the struct
            LASTINPUTINFO LastInputInfo = new LASTINPUTINFO();
            LastInputInfo.cbSize = (uint)Marshal.SizeOf(LastInputInfo);
            LastInputInfo.dwTime = 0;

            // If we have a value from the function
            if (GetLastInputInfo(ref LastInputInfo))
            {
                // Get the number of ticks at the point when the last activity was seen
                LastInputTicks = (int)LastInputInfo.dwTime;
                // Number of idle ticks = system uptime ticks - number of ticks at last input
                IdleTicks = systemUptime - LastInputTicks;
            }
            return IdleTicks / 1000;
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (true)
            {
                if (GetLastInput() > AutoLogoutTimer)
                {
                    e.Cancel = true;
                    worker.CancelAsync();
                    break;
                }
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                bw.DoWork -= new DoWorkEventHandler(bw_DoWork);
                //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                bw.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                Messenger.Default.Send(new NotificationMessage("ShowMainView"));
            }
        }

       
        private void NotificationMessageRecieved(NotificationMessage msg)
        {
            if (msg.Notification == "FromHomeToLogIn")
            {
                MainWindow MainWindow = new MainWindow();
                MainWindow.Show();
                this.Close();
            }

            else if (msg.Notification == "FromHomeToDialog")
            {
                var AddPatientDialogWindow = new DialogWindow();
                AddPatientDialogWindow.Show();
            }
            else if (msg.Notification == "FromHomeToPatient")
            {
                var PatientView = new PatientWindow();
                PatientView.Show();
                this.Close();
            }
        }

       
    }
}
