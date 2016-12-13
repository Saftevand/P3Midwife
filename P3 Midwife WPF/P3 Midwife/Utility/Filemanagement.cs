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

        public static void CreateDirectory(string NameOfDirectory)
        {
            Directory.CreateDirectory(NameOfDirectory);
        }

        public static void CreateFile(string Directory, string NameOfFile)
        {
            if (!File.Exists(Path.Combine(Directory, NameOfFile + ".txt")))
            {
                File.Create(Path.Combine(Directory, NameOfFile + ".txt")).Close();
            }
        }

        public static void InitialiseFoldersAndFiles()
        {
            InitialiseMainFolders();
            ReadPatients();
            InitialiseAdminFiles();
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
                    file.WriteLine(i.ToString() + " False");
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
            if(String.IsNullOrEmpty(File.ReadAllText(Path.Combine(_AdminPath, "Employee_info.txt"))))
            {
                StreamWriter file = new StreamWriter(Path.Combine(_AdminPath, "Employee_info.txt"));
                file.WriteLine("1 Gitte Bredahl 123 56189416 palminde@hotmail.com 1");
                file.WriteLine("2 Pernille Johansen kode 18748643 PernilleJ@hotmail.com 1");
                file.WriteLine("3 Amalie Knudsen sosu 89435135 AmalieK@hotmail.com 2");
                file.WriteLine("4 Mette Hansen 321 49846516 Metteh@hotmail.com 2");
                file.WriteLine("5 x x x 1 x 1");
                file.Close();
            }
            else ReadEmployees(Path.Combine(_AdminPath, "Employee_info.txt"));
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
            CreateFile(Path.Combine(_PatientsPath, patient.CPR.ToString()), "_info");
            StreamWriter file = new StreamWriter(Path.Combine(_PatientsPath, patient.CPR.ToString(), "_info.txt"));
            file.WriteLine(patient.Name + " " + patient.CPR.ToString());
            foreach (Patient child in patient.Children)
            {
                file.Write(" " + child.CPR.ToString());
            }
            file.Close();
        }

        #region save files
        public static void SaveToDatabase()
        {
            SaveRoomFile();
            SaveEmployeeFile();
        }

        private static void SaveRoomFile()
        {
            StreamWriter file = new StreamWriter(Path.Combine(_AdminPath, "Room_info.txt"));
            foreach (DeliveryRoom room in Ward.DeliveryRooms)
            {
                file.Write(room.RoomID.ToString() + " " + room.Occupied.ToString());
                foreach (Patient patient in room.PatientsInRoom)
                {
                    file.Write(" " + patient.CPR.ToString());
                }
                file.WriteLine();
            }
            file.Close();
        }

        private static void SaveEmployeeFile()
        {
            StreamWriter file = new StreamWriter(Path.Combine(_AdminPath, "Employee_info.txt"));
            foreach (Employee employee in Ward.Employees)
            {
                file.Write(employee.ID.ToString() + " " + employee.Name + " " + employee.Password.ToString() + " " + employee.TelephoneNumber.ToString() + " " + employee.Email + " ");
                if (employee is Midwife)
                {
                    file.Write("1");
                }
                else file.Write("2");
                foreach (Patient patient in employee.CurrentPatients)
                {
                    file.Write(" " + patient.CPR.ToString());
                }
                file.WriteLine();
            }
            file.Close();
        }

        public static void SaveRecord(Record record)
        {
            StreamWriter file = new StreamWriter(Path.Combine(_PatientsPath, record.RecordsPatient.CPR.ToString(), "_Record" + record.ThisRecordID.ToString()));
            file.Write(record.ToFile());
            file.Close();
            SaveBill(record.CurrentBill);
        }

        private static void SaveBill(Bill bill)
        {
            string billpath = Path.Combine(_PatientsPath, Ward.Patients.Find(x => x.RecordList.Any(y => y.ThisRecordID == bill.RecordID)).CPR.ToString(), "_Bill" + bill.RecordID.ToString() + ".txt");
            CreateFile(Path.Combine(_PatientsPath, Ward.Patients.Find(x => x.RecordList.Any(y => y.ThisRecordID == bill.RecordID)).CPR.ToString()), "_Bill" + bill.RecordID.ToString());
            using (StreamWriter sw = new StreamWriter(billpath))
            {
                foreach (MedicalService item in bill.BillItemList)
                {
                    sw.WriteLine(item.ToString());
                }
                sw.WriteLine("Total price : " + bill.TotalPrice.ToString());
            }
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
                DeliveryRoom currentRoom;

                while ((_tempString = sr.ReadLine()) != null)
                {
                    _subStrings = _tempString.Split(' ');

                    currentRoom = new DeliveryRoom(Convert.ToInt32(_subStrings[0]), Convert.ToBoolean(_subStrings[1]));
                    Ward.DeliveryRooms.Add(currentRoom);
                    if (_subStrings.Length > 2)
                    {
                        for (int i = 2; i < _subStrings.Length; i++)
                        {
                            currentRoom.PatientsInRoom.Add(Ward.Patients.Find(x => x.CPR == _subStrings[i]));
                        }
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
                    if(_subStrings.Length > 7)
                    {
                        for (int i = 7; i < _subStrings.Length; i++)
                        {
                            Ward.Employees.Last().CurrentPatients.Add(Ward.Patients.Find(x => x.CPR == _subStrings[i]));
                        }
                    }
                    
                }
            }
        }
        public static void ReadBirthRecords(Patient patient)
        {
            StreamReader sr;
            List<List<string>> information = new List<List<string>>();
            int CIVDripCounter;
            int vgExpCounter;
            int micturitionCounter;
            int fetusObsCounter;
            int birthInfoCounter;
            int variCounter;
            int fileCounter = 0;
            Record recordToBeAdded;
            string tempString;
            string[] filer = Directory.GetFiles(Path.Combine(_PatientsPath, patient.CPR.ToString())).Where(x => !x.Contains("info")).ToArray();

            foreach (string fil in filer)
            {
                recordToBeAdded = new Record(patient);
                CIVDripCounter = 0;
                vgExpCounter = 0;
                micturitionCounter = 0;
                fetusObsCounter = 0;
                birthInfoCounter = 0;
                variCounter = 0;
                sr = new StreamReader(fil);
                information.Add(sr.ReadToEnd().Split('_').ToList());
                information[fileCounter].RemoveAt(0);
                foreach (string paragraph in information[fileCounter])
                {
                    tempString = paragraph.Split('|')[0];
                    switch (tempString)
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
                char[] seperators = new char[1];
                seperators[0] = '|';
                if (CIVDripCounter != 0)
                {
                    string[] CIVDripInfo;
                    for (; i < CIVDripCounter; i++)
                    {
                        CIVDripInfo = information[fileCounter][i].Split('|');
                        Record._contractionIVDrip temp = new Record._contractionIVDrip();
                        if(!String.IsNullOrEmpty(CIVDripInfo[1])) temp.Time = DateTime.ParseExact(CIVDripInfo[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        if(!String.IsNullOrEmpty(CIVDripInfo[2])) temp.NumberOfContractionsPerMinute = Convert.ToInt32(CIVDripInfo[2]);
                        if(!String.IsNullOrEmpty(CIVDripInfo[3])) temp.SDripMlPerHour = Convert.ToInt32(CIVDripInfo[3]);
                        recordToBeAdded.ContractionIVDripList.Add(temp);
                    }
                }
                if (vgExpCounter != 0)
                {
                    string[] vagExpInfo;
                    int loopCounter = vgExpCounter + i;
                    for (; i < loopCounter; i++)
                    {
                        vagExpInfo = information[fileCounter][i].Split('|');
                        Record._vaginalExploration temp = new Record._vaginalExploration();
                        if (!String.IsNullOrEmpty(vagExpInfo[1])) temp.Time = DateTime.ParseExact(vagExpInfo[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        if (!String.IsNullOrEmpty(vagExpInfo[2])) temp.Collum = Convert.ToInt32(vagExpInfo[2]);
                        if (!String.IsNullOrEmpty(vagExpInfo[3])) temp.Dialation = Convert.ToInt32(vagExpInfo[3]);
                        if (!String.IsNullOrEmpty(vagExpInfo[4])) temp.Position = vagExpInfo[4];
                        if (!String.IsNullOrEmpty(vagExpInfo[5])) temp.Rotation = Convert.ToInt32(vagExpInfo[5]);
                        if (!String.IsNullOrEmpty(vagExpInfo[6])) temp.Consistency = vagExpInfo[6];
                        if (!String.IsNullOrEmpty(vagExpInfo[7])) temp.Location = vagExpInfo[7];
                        if (!String.IsNullOrEmpty(vagExpInfo[8])) temp.AmnioticFluid = vagExpInfo[8];
                        recordToBeAdded.VaginalExplorationList.Add(temp);
                    }
                }
                if (micturitionCounter != 0)
                {
                    string[] MictuInfo;
                    int loopCounter = micturitionCounter + i;
                    for (; i < loopCounter; i++)
                    {
                        MictuInfo = information[fileCounter][i].Split('|');
                        Record._micturition temp = new Record._micturition();
                        if (!String.IsNullOrEmpty(MictuInfo[1])) temp.Time = DateTime.ParseExact(MictuInfo[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        if (!String.IsNullOrEmpty(MictuInfo[2])) temp.MicturitionNote = MictuInfo[2];
                        recordToBeAdded.MicturitionList.Add(temp);
                    }
                }
                if (fetusObsCounter != 0)
                {
                    string[] fetusObsInfo;
                    int loopCounter = fetusObsCounter + i;
                    for (; i < loopCounter; i++)
                    {
                        fetusObsInfo = information[fileCounter][i].Split('|');
                        Record._fetusObservation temp = new Record._fetusObservation();
                        if (!String.IsNullOrEmpty(fetusObsInfo[1])) temp.Time = DateTime.ParseExact(fetusObsInfo[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        if (!String.IsNullOrEmpty(fetusObsInfo[2])) temp.HearthFrequency = fetusObsInfo[2];
                        if (!String.IsNullOrEmpty(fetusObsInfo[3])) temp.CTG = fetusObsInfo[3];
                        if (!String.IsNullOrEmpty(fetusObsInfo[4])) temp.CTGClassification = fetusObsInfo[4];
                        if (!String.IsNullOrEmpty(fetusObsInfo[5])) temp.STAN = fetusObsInfo[5];
                        if (!String.IsNullOrEmpty(fetusObsInfo[6])) temp.ScalppH = Convert.ToDouble(fetusObsInfo[6]);
                        if (!String.IsNullOrEmpty(fetusObsInfo[7])) temp.ScalpLactate = Convert.ToDouble(fetusObsInfo[7]);
                        recordToBeAdded.FetusObservationList.Add(temp);
                    }
                }
                if (birthInfoCounter != 0)
                {
                    string[] birthInfo;
                    int loopCounter = birthInfoCounter + i;
                    for (; i < loopCounter; i++)
                    {
                        birthInfo = information[fileCounter][i].Split('|');
                        Record._birthInformation temp = new Record._birthInformation();
                        if (!String.IsNullOrEmpty(birthInfo[1])) temp.Time = DateTime.ParseExact(birthInfo[1], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        if (!String.IsNullOrEmpty(birthInfo[2])) temp.Result = birthInfo[2];
                        if (!String.IsNullOrEmpty(birthInfo[3])) temp.AmnioticFluid = birthInfo[3];
                        if (!String.IsNullOrEmpty(birthInfo[4])) temp.AmountOfFluid = birthInfo[4];
                        if (!String.IsNullOrEmpty(birthInfo[5])) temp.BloodAmount = Convert.ToDouble(birthInfo[5]);
                        if (!String.IsNullOrEmpty(birthInfo[6])) temp.BleedingCause = birthInfo[6];
                        if (!String.IsNullOrEmpty(birthInfo[7])) temp.BirthPosition = birthInfo[7];
                        recordToBeAdded.BirthInformationList.Add(temp);
                    }
                }
                if (variCounter != 0)
                {
                    string[] VariInfo;
                    int loopCounter = variCounter + i;
                    for (; i < loopCounter; i++)
                    {
                        VariInfo = information[fileCounter][i].Split('|');
                        if (!String.IsNullOrEmpty(VariInfo[1])) recordToBeAdded.ThisRecordID = Convert.ToInt32(VariInfo[1]);
                        if (!String.IsNullOrEmpty(VariInfo[2])) recordToBeAdded.TimeOfBirth = DateTime.ParseExact(VariInfo[2], "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        if (!String.IsNullOrEmpty(VariInfo[3])) recordToBeAdded.CircumferenceHead = Convert.ToDouble(VariInfo[3]);
                        if (!String.IsNullOrEmpty(VariInfo[4])) recordToBeAdded.CircumferenceStomach = Convert.ToDouble(VariInfo[4]);
                        if (!String.IsNullOrEmpty(VariInfo[5])) recordToBeAdded.BloodSugar = Convert.ToDouble(VariInfo[5]);
                        if (!String.IsNullOrEmpty(VariInfo[6])) recordToBeAdded.GA = VariInfo[6];
                        if (!String.IsNullOrEmpty(VariInfo[7])) recordToBeAdded.NavelpHVenous = Convert.ToDouble(VariInfo[7]);
                        if (!String.IsNullOrEmpty(VariInfo[8])) recordToBeAdded.NavelpHArterial = Convert.ToDouble(VariInfo[8]);
                        if (!String.IsNullOrEmpty(VariInfo[9])) recordToBeAdded.NavelBaseExcessArterial = Convert.ToDouble(VariInfo[9]);
                        if (!String.IsNullOrEmpty(VariInfo[10])) recordToBeAdded.NavelBaseExcessVenous = Convert.ToDouble(VariInfo[10]);
                        if (!String.IsNullOrEmpty(VariInfo[11])) recordToBeAdded.FetusPosition = Convert.ToInt32(VariInfo[11]);
                        if (!String.IsNullOrEmpty(VariInfo[12])) recordToBeAdded.PlacentaWeight = Convert.ToDouble(VariInfo[12]);
                        if (!String.IsNullOrEmpty(VariInfo[13])) recordToBeAdded.KVitamin = Convert.ToBoolean(VariInfo[13]);
                        if (!String.IsNullOrEmpty(VariInfo[14])) recordToBeAdded.ApgarOneMinute = Convert.ToInt32(VariInfo[14]);
                        if (!String.IsNullOrEmpty(VariInfo[15])) recordToBeAdded.ApgarFiveMinutes = Convert.ToInt32(VariInfo[15]);
                        if (!String.IsNullOrEmpty(VariInfo[16])) recordToBeAdded.ApgarTenMinutes = Convert.ToInt32(VariInfo[16]);
                        if (!String.IsNullOrEmpty(VariInfo[17])) recordToBeAdded.AO = Convert.ToInt32(VariInfo[17]);
                        if (!String.IsNullOrEmpty(VariInfo[18])) recordToBeAdded.HO = Convert.ToInt32(VariInfo[18]);
                        if (!String.IsNullOrEmpty(VariInfo[19])) recordToBeAdded.Weight = Convert.ToDouble(VariInfo[19]);
                        if (!String.IsNullOrEmpty(VariInfo[20])) recordToBeAdded.Length = Convert.ToDouble(VariInfo[20]);
                        if (!String.IsNullOrEmpty(VariInfo[21])) recordToBeAdded.NumberOfChildren = VariInfo[21];
                        if (!String.IsNullOrEmpty(VariInfo[22])) recordToBeAdded.FurtherNotice = VariInfo[22];
                        if (!String.IsNullOrEmpty(VariInfo[23])) recordToBeAdded.Sucking = Convert.ToBoolean(VariInfo[23]);
                        if (!String.IsNullOrEmpty(VariInfo[24])) recordToBeAdded.Nose = Convert.ToBoolean(VariInfo[24]);
                        if (!String.IsNullOrEmpty(VariInfo[25])) recordToBeAdded.Pharynx = Convert.ToBoolean(VariInfo[25]);
                        if (!String.IsNullOrEmpty(VariInfo[26])) recordToBeAdded.Ventricle = Convert.ToBoolean(VariInfo[26]);
                        if (!String.IsNullOrEmpty(VariInfo[27])) recordToBeAdded.Diagnosis = VariInfo[27];
                        if (!String.IsNullOrEmpty(VariInfo[28])) recordToBeAdded.Note = VariInfo[28];
                        for (int h = 29; h < VariInfo.Length; h++)
                        {
                            recordToBeAdded.Diseases.Add(VariInfo[h]);
                        }
                    }
                    sr.Close();
                }
                patient.RecordList.Add(recordToBeAdded);
                fileCounter++;
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
                Ward.Patients.Add(new Patient(FolderName.Substring(_PatientsPath.Length + 1, 10), informationline[0] + " " + informationline[1]));
                sr.Close();
            }
        }
        public static void ReadBills(Patient patient)
        {
            string[] filer = Directory.GetFiles(Path.Combine(_PatientsPath, patient.CPR.ToString())).Where(x => !x.Contains("info") && !x.Contains("Record")).ToArray();
            Bill tempbill;
            Record temprecord;
            if (filer != null)
            {
                foreach (string fil in filer)
                {
                    temprecord = patient.RecordList.Find(x => x.ThisRecordID == Convert.ToInt32(fil.Split('_').Last().Substring(4, fil.Split('_').Last().Length - 4)));
                    tempbill = new Bill(temprecord);
                    foreach (string line in File.ReadLines(fil))
                    {
                        if (!line.Contains("price"))
                        {
                            tempbill.BillItemList.Add(Ward.MedicalServicesList.Find(x => line.Contains(x.Name)));
                        }
                    }
                }
            }

        }
        public static void ReadWords()
        {
            using (StreamReader reader = new StreamReader(Environment.CurrentDirectory + "\\PersonInfo\\Words.dos", System.Text.Encoding.GetEncoding("iso-8859-1")))
            {
                string line;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    DanishWordList.Add(line.ToLower());
                }
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
        #endregion
        public static List<string> DanishWordList = new List<string>();

        



    }
}
