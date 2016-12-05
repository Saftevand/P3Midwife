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
        private static string _exePath = AppDomain.CurrentDomain.BaseDirectory;
        private static string _DatabasePath = Path.Combine(_exePath, "DatabaseFiles");
        private static string _AdminPath = Path.Combine(_DatabasePath, "Administraties");
        private static string _PatientsPath = Path.Combine(_DatabasePath, "Patients");

        //TODO: bliver ikke brugt endnu
        public static void CreateDirectory(string NameOfDirectory)
        {
            Directory.CreateDirectory(NameOfDirectory);
        }
        //TODO: bliver ikke brugt endnu
        public static void CreateFile(string Directory, string NameOfFile)
        {
            File.Create(Path.Combine(Directory, NameOfFile + ".txt")).Close();
            File.Create(Path.Combine(Directory, NameOfFile)).Close();
        }

        public static void InitialiseFoldersAndFiles()
        {
            InitialiseMainFolders();
            InitialiseAdminFiles();
            WriteInitialEmployeeFile();
            WriteInitialMedicalServicesFile();
            WriteInitialRoomFile();
        }

        private static void InitialiseMainFolders()
        {
            CreateDirectory(Path.Combine(_exePath, "DatabaseFiles"));
            CreateDirectory(Path.Combine(_AdminPath));
            CreateDirectory(Path.Combine(_exePath, "DatabaseFiles", "Patients"));
        }

        private static void InitialiseAdminFiles()
        {
            CreateFile(_AdminPath, "Employee_info");
            WriteInitialEmployeeFile();
            CreateFile(_AdminPath, "Room_info");
            WriteInitialRoomFile();
            CreateFile(_AdminPath, "MedicalServices");
            WriteInitialMedicalServicesFile();
        }

        private static void WriteInitialRoomFile()
        {
            int NumberOfRooms = 15;
            if (String.IsNullOrWhiteSpace(File.ReadAllText(Path.Combine(_AdminPath, "Room_info.txt"))))
            {
                StreamWriter file = new StreamWriter(Path.Combine(_AdminPath, "Room_info.txt"));
                for (int i = 0; i < NumberOfRooms; i++)
                {
                    file.WriteLine(i.ToString() + " f");
                }
                file.Close();
            }
            else
            {
                ReadRooms(Path.Combine(_AdminPath, "Room_info.txt"));
            }
        }

        private static void WriteInitialEmployeeFile()
        {
            if(String.IsNullOrWhiteSpace(File.ReadAllText(Path.Combine(_AdminPath, "Employee_info.txt"))))
            {
                StreamWriter file = new StreamWriter(Path.Combine(_AdminPath, "Employee_info.txt"));
                file.WriteLine("1 Gitte Bredahl 123 56189416 palminde@hotmail.com 1");
                file.WriteLine("2 Pernille Johansen kode 18748643 PernilleJ@hotmail.com 1");
                file.WriteLine("3 Amalie Knudsen sosu 89435135 AmalieK@hotmail.com 2");
                file.WriteLine("4 Mette Hansen 321 49846516 Metteh@hotmail.com 2");
                file.Close();
            }
            else
            {
                ReadEmployees(Path.Combine(_AdminPath, "Employee_info.txt"));
            }
        }

        private static void WriteInitialMedicalServicesFile()
        {
            if (String.IsNullOrWhiteSpace(File.ReadAllText(Path.Combine(_AdminPath, "MedicalServices.txt"))))
            {
                StreamWriter file = new StreamWriter(Path.Combine(_AdminPath, "MedicalServices.txt"));
                file.WriteLine("BS BloodSample 129,00");
                file.WriteLine("MG Mucogel 49,00");
                file.WriteLine("PT Peptac 23,00");
                file.WriteLine("RH Ranitidine_hydrochloride(75mg) 26,00");
                file.WriteLine("FG Fybogel 91,00");
                file.WriteLine("LL Lactulose 13,00");
                file.WriteLine("AP Anusol_Plus 16,00");
                file.WriteLine("CS Canestan 78,00");
                file.WriteLine("FA Folic_acid_(400mcg) 28,00");
                file.WriteLine("FS Ferrous_Sulphate 67,00");
                file.WriteLine("PD Pregaday 20,00");
                file.Close();
                
            }
            else
            {
                ReadMedicalServiceFromFile(Path.Combine(_AdminPath, "MedicalServices.txt"));
            }
            
        }

        public static void CreatePatientFolderAndFile(Patient patient)
        {
            Directory.CreateDirectory(Path.Combine(_PatientsPath, patient.CPR.ToString()));
            File.Create(Path.Combine(_PatientsPath, patient.CPR.ToString(), "_info"));
            StreamWriter file = new StreamWriter(Path.Combine(_PatientsPath, patient.CPR.ToString(), "_info.txt"));
            file.WriteLine(patient.Name + " " + patient.CPR.ToString());
            foreach (Patient child in patient.Children)
            {
                file.Write(" " + child.CPR.ToString());
            }
            file.Close();
        }


        public static void ReadBirthRecords(Patient patient)
        {
            StreamReader sr;
            List<List<string>> information = new List<List<string>>();
            int CIVDripCounter = 0;
            int vgExpCounter = 0;
            int micturitionCounter = 0;
            int fetusObsCounter = 0;
            int birthInfoCounter = 0;
            int variCounter = 0;
            Record recordToBeAdded = new Record(patient);
            string lortstreng;
            string[] filer = Directory.GetFiles(Path.Combine(_PatientsPath, patient.CPR.ToString())).Where(x => !x.Contains("info")).ToArray();
            foreach (string fil in filer)
            {
                sr = new StreamReader(fil);
                information.Add(sr.ReadToEnd().Split('_').ToList());
                information[0].RemoveAt(0);
                foreach (string paragraph in information[0])
                {
                    lortstreng = paragraph.Split('|')[0];
                    switch (lortstreng)
                    {
                        case "contractionIVDrip":
                            CIVDripCounter++;
                            break;
                        case "vaginalExp":
                            vgExpCounter++;
                            break;
                        case "micturition":
                            micturitionCounter++;
                            break;
                        case "fetusObservation":
                            fetusObsCounter++;
                            break;
                        case "birthInformation":
                            birthInfoCounter++;
                            break;
                        case "variables":
                            variCounter++;
                            break;
                        default:
                            break;
                    }
                }
                int i = 0;
                if (CIVDripCounter != 0)
                {
                    string[] CIVDripInfo;
                    for (; i < CIVDripCounter; i++)
                    {
                        CIVDripInfo = information[0][i].Split('|');
                        Record._contractionIVDrip temp = new Record._contractionIVDrip();
                        temp.Time = DateTime.ParseExact(CIVDripInfo[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        temp.NumberOfContractionsPerMinute = Convert.ToInt32(CIVDripInfo[2]);
                        temp.SDripMlPerHour = Convert.ToInt32(CIVDripInfo[3]);
                        temp.Note = CIVDripInfo[4];
                        recordToBeAdded.ContractionIVDripList.Add(temp);
                    }
                }
                if (vgExpCounter != 0)
                {
                    string[] vagExpInfo;
                    int loopCounter = vgExpCounter + i;
                    for (; i < loopCounter; i++)
                    {
                        vagExpInfo = information[0][i].Split('|');
                        Record._vaginalExploration temp = new Record._vaginalExploration();
                        temp.Time = DateTime.ParseExact(vagExpInfo[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        temp.Collum = Convert.ToInt32(vagExpInfo[2]);
                        temp.Dialation = Convert.ToInt32(vagExpInfo[3]);
                        temp.Position = vagExpInfo[4];
                        temp.Rotation = Convert.ToInt32(vagExpInfo[5]);
                        temp.Consistency = vagExpInfo[6];
                        temp.Location = vagExpInfo[7];
                        temp.AmnioticFluid = vagExpInfo[8];
                        temp.Note = vagExpInfo[9];
                        recordToBeAdded.VaginalExplorationList.Add(temp);
                    }
                }
                if (micturitionCounter != 0)
                {
                    string[] MictuInfo;
                    int loopCounter = micturitionCounter + i;
                    for (; i < loopCounter; i++)
                    {
                        MictuInfo = information[0][i].Split('|');
                        Record._micturition temp = new Record._micturition();
                        temp.Time = DateTime.ParseExact(MictuInfo[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        temp.MicturitionNote = MictuInfo[2];
                        recordToBeAdded.MicturitionList.Add(temp);
                    }
                }
                if (fetusObsCounter != 0)
                {
                    string[] fetusObsInfo;
                    int loopCounter = fetusObsCounter + i;
                    for (; i < loopCounter; i++)
                    {
                        fetusObsInfo = information[0][i].Split('|');
                        Record._fetusObservation temp = new Record._fetusObservation();
                        temp.Time = DateTime.ParseExact(fetusObsInfo[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        temp.HearthFrequency = fetusObsInfo[2];
                        temp.CTG = fetusObsInfo[3];
                        temp.CTGClassification = fetusObsInfo[4];
                        temp.STAN = fetusObsInfo[5];
                        temp.ScalppH = Convert.ToDouble(fetusObsInfo[6]);
                        temp.ScalpLactate = Convert.ToDouble(fetusObsInfo[7]);
                        temp.Note = fetusObsInfo[8];
                        recordToBeAdded.FetusObservationList.Add(temp);
                    }
                }
                if (birthInfoCounter != 0)
                {
                    string[] birthInfo;
                    int loopCounter = birthInfoCounter + i;
                    for (; i < loopCounter; i++)
                    {
                        birthInfo = information[0][i].Split('|');
                        Record._birthInformation temp = new Record._birthInformation();
                        temp.Time = DateTime.ParseExact(birthInfo[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        temp.Result = birthInfo[2];
                        temp.AmnioticFluid = birthInfo[3];
                        temp.AmountOfFluid = birthInfo[4];
                        temp.BloodAmount = Convert.ToDouble(birthInfo[5]);
                        temp.BleedingCause = birthInfo[6];
                        temp.BirthPosition = birthInfo[7];
                        temp.Note = birthInfo[8];
                        recordToBeAdded.BirthInformationList.Add(temp);
                    }
                }
                if (variCounter != 0)
                {
                    string[] VariInfo;
                    int loopCounter = variCounter + i;
                    for (; i < loopCounter; i++)
                    {
                        VariInfo = information[0][i].Split('|');
                        recordToBeAdded.ThisRecordID = Convert.ToInt32(VariInfo[1]);
                        recordToBeAdded.TimeOfBirth = DateTime.ParseExact(VariInfo[2], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        recordToBeAdded.CircumferenceHead = Convert.ToDouble(VariInfo[3]);
                        recordToBeAdded.CircumferenceStomach = Convert.ToDouble(VariInfo[4]);
                        recordToBeAdded.BloodSugar = Convert.ToDouble(VariInfo[5]);
                        recordToBeAdded.GA = VariInfo[6];
                        recordToBeAdded.NavelpHVenous = Convert.ToDouble(VariInfo[7]);
                        recordToBeAdded.NavelpHArterial = Convert.ToDouble(VariInfo[8]);
                        recordToBeAdded.NavelBaseExcessArterial = Convert.ToDouble(VariInfo[9]);
                        recordToBeAdded.NavelBaseExcessVenous = Convert.ToDouble(VariInfo[10]);
                        recordToBeAdded.FetusPosition = Convert.ToInt32(VariInfo[11]);
                        recordToBeAdded.PlacentaWeight = Convert.ToDouble(VariInfo[12]);
                        recordToBeAdded.KVitamin = Convert.ToBoolean(VariInfo[13]);
                        recordToBeAdded.ApgarOneMinute = Convert.ToInt32(VariInfo[14]);
                        recordToBeAdded.ApgarFiveMinutes = Convert.ToInt32(VariInfo[15]);
                        recordToBeAdded.ApgarTenMinutes = Convert.ToInt32(VariInfo[16]);
                        recordToBeAdded.AO = Convert.ToInt32(VariInfo[17]);
                        recordToBeAdded.HO = Convert.ToInt32(VariInfo[18]);
                        recordToBeAdded.Weight = Convert.ToDouble(VariInfo[19]);
                        recordToBeAdded.Length = Convert.ToDouble(VariInfo[20]);
                        recordToBeAdded.NumberOfChildren = VariInfo[21];
                        recordToBeAdded.FurtherNotice = VariInfo[22];
                        recordToBeAdded.Sucking = Convert.ToBoolean(VariInfo[23]);
                        recordToBeAdded.Nose = Convert.ToBoolean(VariInfo[24]);
                        recordToBeAdded.Pharynx = Convert.ToBoolean(VariInfo[25]);
                        recordToBeAdded.Ventricle = Convert.ToBoolean(VariInfo[26]);
                        recordToBeAdded.Diagnosis = VariInfo[27];
                        for (int h = 28; h < VariInfo.Length; h++)
                        {
                            recordToBeAdded.Diseases.Add(VariInfo[h]);
                        }
                    }
                    sr.Close();
                }
                patient.RecordList.Add(recordToBeAdded);
            }
        }

        public static void ReadPatients()
        {
            StreamReader sr;
            foreach (string FolderName in Directory.GetDirectories(_PatientsPath))
            {
                sr = new StreamReader(Path.Combine(FolderName, "_info.txt"));
                string[] informationline = sr.ReadLine().Split(' ');
                string test = informationline[0] + " " + informationline[1];
                Ward.Patients.Add(new Patient(FolderName.Substring(_PatientsPath.Length+1,10), informationline[0] + " " + informationline[1]));
                sr.Close();
            }
        }

        public static void SaveToDatabase()
        {
            StreamWriter file = new StreamWriter(Path.Combine(_AdminPath, "Room_info.txt"));
            foreach (DeliveryRoom room in Ward.DeliveryRooms)
            {
                file.WriteLine(room.ToString());
                foreach (Patient patient in room.PatientsInRoom)
                {
                    file.Write(patient.CPR.ToString());
                }
            }
            file.Close();
        }

        #region Add to file
        public static void AddPatientOrEmployeeToFile(object _person)
        {
            string _nameOfFile = GetPersonFilePath(_person);

            string AccountFile = (Path.Combine(Environment.CurrentDirectory + "\\PersonInfo", _nameOfFile));
            using (StreamWriter sw = File.AppendText(AccountFile))
            {
                sw.WriteLine(_person.ToString());
            }
        }
        #endregion
        #region Remove from file
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
        #endregion
        #region File Path Operations
        private static string GetPersonFilePath(object _person)
        {
            if (_person is Employee)
                return "Employee_info.txt";
            else if (_person is Patient)
                return "Patient_Info.txt";
            else
                throw (new Exception("Object is not patient or employee"));
        }
        #endregion
        #region Read from file
        public static void ReadMedicalServiceFromFile(string FilePath)
        {
            Stream ServicesFile = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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
        public static void ReadRooms(string FilePath)
        {
            Stream AccountFile = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
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

        public static void ReadEmployees(string FilePath)
        {
            Stream AccountFile = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (StreamReader sr = new StreamReader(AccountFile))
            {
                string _tempString;
                string[] _subStrings;

                while ((_tempString = sr.ReadLine()) != null)
                {
                    _subStrings = _tempString.Split(' ');

                    if (_subStrings[6] == "1")
                    {
                        Ward.Employees.Add(new Midwife(Convert.ToInt32(_subStrings[0]), _subStrings[1] +" "+ _subStrings[2], _subStrings[3], Convert.ToInt32(_subStrings[4]), _subStrings[5]));
                    }
                    else if (_subStrings[6] == "2")
                    {
                        Ward.Employees.Add(new SOSU(Convert.ToInt32(_subStrings[0]), _subStrings[1] + " " + _subStrings[2], _subStrings[3], Convert.ToInt32(_subStrings[4]), _subStrings[5]));
                    }
                }
            }
        }
        #endregion
        public static void WriteBill(Bill bill)
        {
            CreateFile(Environment.CurrentDirectory + "\\PersonInfo", bill.BillsRecord.RecordsPatient.CPR + "_" + bill.RecordID.ToString() + ".txt");
            string AccountFile = (Path.Combine(Environment.CurrentDirectory + "\\PersonInfo", bill.BillsRecord.RecordsPatient.CPR + "_" + bill.RecordID.ToString() + ".txt"));

            using (StreamWriter sw = new StreamWriter(AccountFile))
            {
                foreach (MedicalService item in bill.BillItemList)
                {
                    sw.WriteLine(item.ToString());
                }
                sw.WriteLine("Total price : " + bill.TotalPrice.ToString());
            }
        }
    }
}
