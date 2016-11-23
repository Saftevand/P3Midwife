using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace P3_Midwife
{
    public static class Filemanagement
    {
        public static string _exePath = AppDomain.CurrentDomain.BaseDirectory;
        public static List<string> _Files = new List<string>();

        public static void CreateDirectory(string NameOfDirectory)
        {
            Directory.CreateDirectory(NameOfDirectory);
        }

        public static void CreateFile(string Directory, string NameOfFile)
        {
            File.Create(Path.Combine(Directory, NameOfFile));
            _Files.Add(Path.Combine(Directory, NameOfFile));
        }

        public static void ReadEmployees(string Directory, string NameOfFile)
        {
            int i = 1;
            Stream AccountFile = File.Open(Path.Combine(Directory, NameOfFile),FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
            using (StreamReader sr = new StreamReader(AccountFile))
            {
                string _tempString;
                string[] _subStrings;

                while ((_tempString = sr.ReadLine()) != null)
                {
                    _subStrings = _tempString.Split(' ');
                    if (_subStrings[4] == "1")
                    {
                        Ward.Employees.Add(new Midwife(i, _subStrings[0], _subStrings[1], Convert.ToInt32(_subStrings[2]), _subStrings[3].ToUpper()));
                    }
                    else if (_subStrings[4] == "2")
                    {
                        Ward.Employees.Add(new SOSU(i, _subStrings[0], _subStrings[1], Convert.ToInt32(_subStrings[2]), _subStrings[3].ToUpper()));
                    }
                    i++;
                }
            }
        }

        public static void ReadPatients(string Directory, string NameOfFile)
        {
            Stream AccountFile = File.Open(Path.Combine(Directory, NameOfFile), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (StreamReader sr = new StreamReader(AccountFile))
            {
                string _tempString;
                string[] _subStrings;

                while ((_tempString = sr.ReadLine()) != null)
                {
                    _subStrings = _tempString.Split(' ');
                    Ward.Patients.Add(new Patient(_subStrings[0], _subStrings[1]));
                }
            }
        }

        public static void AddToFile()
        {

        }
    }
}
