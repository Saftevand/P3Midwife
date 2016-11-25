﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P3_Midwife
{
    public class Patient
    {
        private char gender;
        private int GA;
        private int weekDay;
        private int weightGram;
        private int lenghtCM;
        private int fetus;
        private int HO;
        private int AO;
        private int placentaWeightGram;
        private char kVitamin;
        private string diag;
        private int abgar1;
        private int abgar5;
        private int abgar10;
        private string note;
        //TODO: visse bools skal måske have andre typer.
        private bool suck;
        private bool nose;
        private bool throat;
        private bool ventricle;
        private bool bloodsugar;
        private decimal arteriaPH;
        private decimal arteriaSBE;
        private decimal venePH;
        private decimal veneSBE;
        private DateTime timeOfBirth;
        private string _name;
        public Patient Mother;
        public DeliveryRoom InRoom;

        public string CPR { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private List<Record> _recordList = new List<Record>();
        public List<Record> RecordList { get { return _recordList; } set { _recordList = value; } }
        public List<Patient> Children = new List<Patient>();

        public Patient(string PatientCPR, string PatientName)
        {
            this.CPR = PatientCPR;
            this._name = PatientName;
            this.gender = FindGenderFromCPR(PatientCPR);
            Filemanagement.AddPatientOrEmployeeToFile(this);
            this.RecordList.Add(new Record(this));
        }

        public Patient(char _gender)
        {
            GenerateCpr(_gender);
            this.gender = _gender;
            Filemanagement.AddPatientOrEmployeeToFile(this);
            this.InRoom = this.Mother.InRoom;
        }

        public Patient(char _gender, string _today)
        {
            GenerateCpr(_gender, _today);
            this.gender = _gender;
            Filemanagement.AddPatientOrEmployeeToFile(this);
            this.InRoom = this.Mother.InRoom;
        }

        private char FindGenderFromCPR(string _cpr)
        {
            if ((int)char.GetNumericValue(_cpr[9]) % 2 == 0)
                return 'P';
            else
                return 'D';  
        }

        //TODO: HUSK GENERATE CPR ER OVERLOADET den med CPR string er til at teste. ikke sikkert den er nødvendig
        //Function to generate CPR.
        private void GenerateCpr(char gender)
        {
            //Retrieves date for cpr number
            GenerateCpr(gender, DateTime.Today.ToString("ddMMyy"));
        }

        private void GenerateCpr(char gender, string CPRString)
        {
            int[] CPR = new int[10];
            
            string result = null;

            //Puts date into int array for later calculations
            int count = 0;
            foreach (char item in CPRString)
            {
                CPR[count] = (int)char.GetNumericValue(item);
                count++;
            }

            CPR[6] = 4;

            result = CalcLastFour(CPR, gender);

            // Stores used CPR numbers
            Ward.UsedCPRs.Add(result);

            this.CPR = result;
        }

        // Function to check if last digit matches the gender of the child
        private bool GenderCPRMatch(char _gender, int[] _cpr)
        {
            if (_gender == 'D' && _cpr[9] % 2 == 1)
                return true;
            else if (_gender == 'P' && _cpr[9] % 2 == 0)
                return true;
            else
                return false;
        }

        //Calculates the last 4 numbers in CPR number
        private string CalcLastFour(int[] _cpr, char _gender)
        {
            string tempResult = null;
            int total = 0, firstSixTotal = 0;
            int[] multiplyValues = { 4, 3, 2, 7, 6, 5 };

            //Calculates the sum of the first 6 cpr digits multiplied with their weights.
            for (int i = 0; i < 6; i++)
            {
                firstSixTotal += _cpr[i] * multiplyValues[i];
            }

            //Iterates through possible values of digit 7, 8 and 9.
            for (int i = 1; i < 600; i++)
            {
                _cpr[8]++;
                if (_cpr[8] == 10)
                {
                    _cpr[7]++;
                    _cpr[8] = 0;
                    if (_cpr[7] == 10)
                    {
                        _cpr[6]++;
                        _cpr[7] = 0;
                    }
                }

                // calculates total sum of first 9 digits in cpr multiplied with their weights.
                total = firstSixTotal;
                total += _cpr[6] * 4;
                total += _cpr[7] * 3;
                total += _cpr[8] * 2;

                _cpr[9] = CalcLastDigit(total);

                // Checks if generated cpr fullfills the requirements and checks if cpr is used before.
                if (_cpr[9] > 9 || (total + _cpr[9]) % 11 != 0 || !GenderCPRMatch(_gender, _cpr))
                    continue;

                else
                {
                    foreach (int item in _cpr)
                    {
                        tempResult = tempResult + item.ToString();
                    }
                    if (Ward.UsedCPRs.Contains(tempResult))
                    {
                        tempResult = null;
                        continue;
                    }
                    else
                        break;
                }
            }
            return tempResult;
        }

        //Calculates last digit of cpr number according to 
        private int CalcLastDigit(int _total)
        {
            int rest = 0;
            rest = _total % 11;
            return 11 - rest;
        }


        public void CreateChild(char _gender)
        {
            Patient child = new Patient(_gender);
            child.Mother = this;
            Children.Add(child);
        }

        public void CreateChild(char _gender, string _date)
        {
            Patient child = new Patient(_gender, _date);
            child.Mother = this;
            Children.Add(child);
        }

        // erstattet af funktion i Midwife - assignPatientToDRoom
        //public void AdmitPatientToRoom(DeliveryRoom room)
        //{            
        //    if (!room.PatientsInRoom.Contains(this))
        //    {
        //        room.PatientsInRoom.Add(this);
        //    }
        //    else throw new ArgumentException(_name + " with CPR:" + CPR.ToString() + " is already in room:" + room.RoomID.ToString());
        //}

        public void DischargePatientFromRoom(DeliveryRoom room)
        {
            if (room.PatientsInRoom.Contains(this))
            {
                room.PatientsInRoom.Remove(this);
            }
            else throw new ArgumentException(_name + " with CPR:" + CPR.ToString() + " is NOT in room:" + room.RoomID.ToString());
        }

        public override string ToString()
        {
            return this.CPR + " " + this.Name;
        }
    }
}
