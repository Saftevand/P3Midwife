using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;

namespace P3_Midwife
{
    public abstract class Employee
    {        
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int TelephoneNumber { get; set; }
        public string Email { get; set; }

        //TODO: læg listen et andet sted. i noget database isch.
        public List<string> AlreadyUsedCPR = new List<string>();

        public Employee(int id, string name, string password, int telephonenumber, string email)
        {
            this.ID = id;
            this.Name = name;
            this.Password = password;
            this.TelephoneNumber = telephonenumber;
            this.Email = email;
        }

        //Function to generate CPR.
        //TODO: måske skal input ændres. pt tjekker den kun om det er en dreng eller ej.
        public string GenerateCpr(bool male)
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

            result = CalcLastFour(CPR, male);

            // Stores used CPR numbers
            AlreadyUsedCPR.Add(result);
            
            return result;
        }

        // Function to check if last digit matches the gender of the child
        bool GenderCPRMatch(bool _male, int[] _cpr)
        {
            if (_male && _cpr[9] % 2 == 1)
                return true;
            else if (!_male && _cpr[9] % 2 == 0)
                return true;
            else
                return false;
        }

        //Calculates the last 4 numbers in CPR number
        string CalcLastFour(int[] _cpr, bool _male)
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
                if (_cpr[9] > 9 || (total + _cpr[9]) % 11 != 0 || !GenderCPRMatch(_male, _cpr))
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


        public override string ToString()
        {
            return "Name: " + Name + " - ID: " + ID;
        }
    }
}
