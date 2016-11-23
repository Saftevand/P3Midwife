using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife.People
{
    class Child : Patient
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
        private bool suck;
        private bool nose;
        private bool throat;
        private bool ventricle;
        private bool bloodsugar;
        private decimal arteriaPH;
        private decimal arteriaSBE;
        private decimal venePH;
        private decimal veneSBE;

        //TODO: læg listen et andet sted. i noget database isch.

        public Child (char _gender)
        {
            gender = _gender;
            CPR = this.GenerateCpr(_gender);
        }

        //Function to generate CPR.
        //TODO: måske skal input ændres. pt tjekker den kun om det er en dreng eller ej.
        public string GenerateCpr(char gender)
        {
            int[] CPR = new int[10];
            DateTime today = DateTime.Today;

            string TodayString = today.ToString();
            string result = null;

            //Retrieves date for cpr number
            string CPRString = DateTime.Today.ToString("ddMMyy");

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
            AlreadyUsedCPR.Add(result);

            return result;
        }

        // Function to check if last digit matches the gender of the child
        bool GenderCPRMatch(char _gender, int[] _cpr)
        {
            if (_gender =='D' && _cpr[9] % 2 == 1)
                return true;
            else if (_gender == 'P' && _cpr[9] % 2 == 0)
                return true;
            else
                return false;
        }

        //Calculates the last 4 numbers in CPR number
        string CalcLastFour(int[] _cpr, char _gender)
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
                    if (AlreadyUsedCPR.Contains(tempResult))
                        continue;
                    else
                        break;
                }
            }
            return tempResult;
        }

        //Calculates last digit of cpr number according to 
        int CalcLastDigit(int _total)
        {
            int rest = 0;
            rest = _total % 11;
            return 11 - rest;
        }
    }
}
