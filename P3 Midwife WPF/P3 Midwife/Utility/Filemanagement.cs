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

        //TODO: bliver ikke brugt endnu
        public static void CreateDirectory(string NameOfDirectory)
        {
            Directory.CreateDirectory(NameOfDirectory);
        }

        //TODO: bliver ikke brugt endnu
        public static void CreateFile(string Directory, string NameOfFile)
        {
            File.Create(Path.Combine(Directory, NameOfFile));
            _Files.Add(Path.Combine(Directory, NameOfFile));
        }

        public static void ReadRecords(string Directory, string NameOfFile)
        {
            Stream RecordFile = File.Open(Path.Combine(Directory, NameOfFile), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (StreamReader sr = new StreamReader(RecordFile))
            {
                string _tempString;
                string[] _subStrings;

                while ((_tempString = sr.ReadLine()) != null)
                {
                    _subStrings = _tempString.Split(' ');
                    //Patrick tager den her fra
                }
            }

        }
        //TODO: directory er ikke nødvendige som parametre. alle filer er i samme mappe
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
                        Ward.Employees.Add(new Midwife(i, _subStrings[0], _subStrings[1], Convert.ToInt32(_subStrings[2]), _subStrings[3].ToUpper(), Convert.ToInt32(_subStrings[4])));
                    }
                    else if (_subStrings[4] == "2")
                    {
                        Ward.Employees.Add(new SOSU(i, _subStrings[0], _subStrings[1], Convert.ToInt32(_subStrings[2]), _subStrings[3].ToUpper(), Convert.ToInt32(_subStrings[4])));
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
                string tempName;
                int counter;
                Patient tempPatient;
                Patient tempPatientChildren;

                while ((_tempString = sr.ReadLine()) != null)
                {
                    _subStrings = _tempString.Split(' ');
                    tempName = "";
                    counter = 0;

                    foreach (string substring in _subStrings)
                    {
                        if (!(substring.Contains("1") || substring.Contains("2") || substring.Contains("3") || substring.Contains("4") || substring.Contains("5") || substring.Contains("6") || substring.Contains("7") || substring.Contains("8") || substring.Contains("9") || substring.Contains("0") ))
                        {
                            tempName += substring;
                            counter++;
                        }
                    }

                    tempPatient = new Patient(_subStrings[0], tempName);
                    for (int i = counter+1; i < _subStrings.Length; i++)
                    {
                        tempPatientChildren = new Patient(_subStrings[i]);
                        tempPatient.Children.Add(tempPatientChildren);
                        Ward.Patients.Add(tempPatientChildren);
                    }

                    Ward.Patients.Add(tempPatient);

                }
            }
        }

        public static void ReadRooms()
        {
            Stream AccountFile = File.Open(Environment.CurrentDirectory + "\\PersonInfo\\Delivery_rooms.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (StreamReader sr = new StreamReader(AccountFile))
            {
                string _tempString;
                string[] _subStrings;
                int RoomID;
                DeliveryRoom currentRoom;

                while ((_tempString = sr.ReadLine()) != null)
                {                 
                    _subStrings = _tempString.Split(' ');
                    RoomID = Convert.ToInt32(_subStrings[0]);
                    bool occupied = false;

                    if (_subStrings[1] == "t")
                        occupied = true;

                    currentRoom = new DeliveryRoom(RoomID, occupied);
                    Ward.DeliveryRooms.Add(currentRoom);

                    for (int i = 2; i < _subStrings.Length; i++)
                    {
                        if (Ward.Patients.Any(x => x.CPR == _subStrings[i]))
                            currentRoom.PatientsInRoom.Add(Ward.Patients.Find(x => x.CPR == _subStrings[i]));
                        else
                            throw new Exception("Patient doesnt exist");
                    }
                }
            }
        }

        public static void AddPatientOrEmployeeToFile(object _person)
        {
            string _nameOfFile = GetPersonFilePath(_person);

            string AccountFile = (Path.Combine(Environment.CurrentDirectory + "\\PersonInfo", _nameOfFile));
            using (StreamWriter sw = File.AppendText(AccountFile))
            {
                sw.WriteLine(_person.ToString());
            }
        }

        public static void RemovePatientFromRoomFile(Patient _person)
        {
            string AccountFile = (Environment.CurrentDirectory + "\\PersonInfo\\Delivery_rooms.txt");
            string text = File.ReadAllText(AccountFile);
            text = text.Replace(" " + _person.CPR, null);
            File.WriteAllText(AccountFile, text);
        }

        public static void RemovePatientFromFile(Patient _person)
        {
            string _nameOfFile = GetPersonFilePath(_person);
            string AccountFile = (Environment.CurrentDirectory + "\\PersonInfo\\Patient_Info.txt");

            string [] originalLines = File.ReadAllLines(AccountFile);
            List<string> updated = new List<string>();

            foreach (string item in originalLines)
            {
                if(!item.Contains(_person.CPR))
                {
                    updated.Add(item);
                }
            }
            File.WriteAllLines(AccountFile, updated);
        }

        private static string GetPersonFilePath(object _person)
        {
            if (_person is Employee)
                return "Employee_info.txt";
            else if (_person is Patient)
                return "Patient_Info.txt";
            else
                throw (new Exception("Object is not patient or employee"));
        }

        public static void ReadMedicalServiceFromFile()
        {
            Stream ServicesFile = File.Open(Environment.CurrentDirectory + "\\PersonInfo\\Medical_Services.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (StreamReader sr = new StreamReader(ServicesFile))
            {
                string _tempString;
                string[] _subStrings;

                while ((_tempString = sr.ReadLine()) != null)
                {
                    _subStrings = _tempString.Split(' ');
                    MedicalService currentService = new MedicalService(Convert.ToDecimal(_subStrings[2]), _subStrings[1], _subStrings[0]);
                    Ward.MedicalServicesList.Add(currentService);
                    TextEditor.values.Add(_subStrings[0], _subStrings[1]);
                }
            }
        }
    }
}
