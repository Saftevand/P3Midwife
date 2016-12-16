using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P3_Midwife
{
    public class Patient
    {
        private char _gender;
        private string _name;
        private Patient _mother;
        private string _CPR;
        private string _bloodType = "Ukendt";
        private DateTime _birthDateTime;
        private List<Patient> _children = new List<Patient>();
        private List<Record> _recordList = new List<Record>();

        public string CPR { get { return _CPR; } set { _CPR = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public char Gender { get { return _gender; } set { _gender = value; } }
        public DateTime BirthDateTime { get { return _birthDateTime; } set { _birthDateTime = value; } }
        public int Age { get { return CalcAge(); } }
        public Patient Mother { get { return _mother; } set { _mother = value; } }
        public List<Patient> Children { get { return _children; } set { _children = value; } }
        public List<Record> RecordList { get { return _recordList; } set { _recordList = value; } }
        public string BloodType { get { return _bloodType; } set { _bloodType = value; } }


        #region Constructors
        public Patient(string PatientCPR, string PatientName)
        {
            this._CPR = PatientCPR;
            this._name = PatientName;
            this._gender = FindGenderFromCPR(PatientCPR);
            // this.RecordList.Add(new Record(this));
        }

        public Patient(string PatientCPR)
        {
            this.CPR = PatientCPR;
            this._gender = FindGenderFromCPR(PatientCPR);
        }

        public Patient(char gender)
        {
            GenerateCpr(gender);
            this._gender = gender;
        }

        public Patient(char gender, string today, Patient mother)
        {
            GenerateCpr(gender, today);
            this._gender = gender;
            this._mother = mother;
            mother.Children.Add(this);
        }
        public Patient(char gender, DateTime birthDateTime, Patient mother)
        {
            this._birthDateTime = birthDateTime;
            GenerateCpr(gender, birthDateTime.ToString("ddMMyy"));
            this._gender = gender;
            this._mother = mother;
            mother.Children.Add(this);
        }

        public Patient(string Cpr, string Name, string BloodType)
        {
            this._CPR = Cpr;
            this._name = Name;
            this._bloodType = BloodType;
            this._gender = FindGenderFromCPR(Cpr);
        }

        public Patient()
        {

        }
        #endregion

        #region Methods
        //looks at last digit of CPR to get gender
        private char FindGenderFromCPR(string _cpr)
        {
            if ((int)char.GetNumericValue(_cpr[9]) % 2 == 0)
                return 'P';
            else
                return 'D';
        }

        //TODO: HUSK GENERATE CPR ER OVERLOADET den med "date" er til at teste. ikke sikkert den er nødvendig
        //Function to generate CPR.
        public void GenerateCpr(char gender)
        {
            //Retrieves date for cpr number
            GenerateCpr(gender, DateTime.Today.ToString("ddMMyy"));
        }

        public void GenerateCpr(char gender, string date)
        {
            int[] CPR = new int[10];

            string result = null;

            //Puts date into int array for later calculations
            int count = 0;
            foreach (char item in date)
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

        //Calculates age besed on CPR number
        private int CalcAge()
        {
            long cpr = long.Parse(this.CPR);
            int date = (int)(cpr / 10000);
            int year = ((100 - (date % 100)) + DateTime.Today.Year) % 100;
            date /= 100;
            if (date % 100 < DateTime.Today.Month || date / 100 < DateTime.Today.Day)
            {
                return year;
            }
            else
                return --year;
        }

        #endregion

        public override string ToString()
        {
            string result = this.CPR + " " + this.Name;

            foreach (Patient children in _children)
            {
                result += " " + children.CPR;
            }
            return result;
        }
    }
}
