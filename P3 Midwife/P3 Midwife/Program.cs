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
                string[] _subStrings;
                List<Employee> _tempEmployees = new List<Employee>();
                while ((_tempString = sr.ReadLine()) != null)
                {
                    _subStrings = _tempString.Split(' ');
                    if(_subStrings[5] == "Mid")
                    {
                        _tempEmployees.Add(new Midwife(Convert.ToInt32(_subStrings[0]), _subStrings[1], _subStrings[2], Convert.ToInt32(_subStrings[3]), _subStrings[4]));
                    }
                    else _tempEmployees.Add(new SOSU(Convert.ToInt32(_subStrings[0]), _subStrings[1], _subStrings[2], Convert.ToInt32(_subStrings[3]), _subStrings[4]));
                }
                return _tempEmployees;
            }
        }
    }
}
