using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace P3_Midwife
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(InitialiseAccounts()));
        }

        private static List<Employee> InitialiseAccounts()
        {
            Stream AccountFile = File.Open("C:\\Users\\GryPetersen\\Desktop\\hej.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (StreamReader sr = new StreamReader(AccountFile))
            {
                string _tempString;
                while ((_tempString = sr.ReadLine()) != null)
                {
                    //needs implementation;
                }
            }
        }
    }
}
