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
    }
}
