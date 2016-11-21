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
    public class Employee : DependencyObject
    {

        public static DependencyProperty NameProperty = DependencyProperty.Register(nameof(Name), typeof(string), typeof(Employee));
        public static DependencyProperty IDProperty = DependencyProperty.Register(nameof(ID), typeof(int), typeof(Employee));
        public static DependencyProperty PasswordProperty = DependencyProperty.Register(nameof(Password), typeof(string), typeof(Employee));
        public static DependencyProperty TelephoneNumberProperty = DependencyProperty.Register(nameof(TelephoneNumber), typeof(int), typeof(Employee));
        public static DependencyProperty EmailProperty = DependencyProperty.Register(nameof(Email), typeof(string), typeof(Employee));



        public int ID { get { return (int)this.GetValue(IDProperty); } set { this.SetValue(IDProperty, value); } }
        public string Name { get { return (string)this.GetValue(NameProperty); } set { this.SetValue(NameProperty, value); } }
        public string Password { get; set; }
        public int TelephoneNumber { get { return (int)this.GetValue(TelephoneNumberProperty); } set { this.SetValue(TelephoneNumberProperty, value); } }
        public string Email { get { return (string)this.GetValue(EmailProperty); } set { this.SetValue(EmailProperty, value); } }

        public Employee(int id, string name, string password, int telephonenumber, string email)
        {
            this.ID = id;
            this.Name = name;
            this.Password = password;
            this.TelephoneNumber = telephonenumber;
            this.Email = email;
        }

        public Employee()
        {
        }

        public string GenerateCpr(string gender = "male")
        {
            List<int> AlreadyUsedCPR = new List<int>();
            DateTime today = DateTime.Today;
            string TodayString = today.ToString();
            int[] CPR = new int[10];
            //pis go måde!!
            //int count = 0;
            //for (int i = 0; i < 10; i++)
            //{
            //    if(i == 0 || i == 1 || i==3 || i == 4 || i == 8 || i == 9)
            //    {
            //        Int32.TryParse(TodayString[i].ToString(),out CPR[count]);
            //        count++;
            //    }
            //}

            // Endnu bedre måde!
            string CPRString = DateTime.Today.ToString("ddMMyy");
            int i = 0;
            foreach (char item in CPRString)
            {
                CPR[i] = (int)char.GetNumericValue(item);
                i++;
            }
            
            int tempTotal = CPR[0] * 4;
            tempTotal += CPR[1] * 3;
            tempTotal += CPR[2] * 2;
            tempTotal += CPR[3] * 7;
            tempTotal += CPR[4] * 6;
            tempTotal += CPR[5] * 5;
            int total = 0;
            for (int j = 0; j < 10000; j++)
            {
                CPR[9]++;
                if (CPR[9] == 10)
                {
                    CPR[8]++;
                    CPR[9] = 0;
                    if (CPR[8] == 10)
                    {
                        CPR[7]++;
                        CPR[8] = 0;
                        if (CPR[7] == 10)
                        {
                            CPR[6]++;
                            CPR[7] = 0;
                        }
                    }
                }
                total = tempTotal;
                total += CPR[6] * 4;
                total += CPR[7] * 3;
                total += CPR[8] * 2;
                total += CPR[9] * 1;
                if (total % 11 == 0)
                {
                    break;
                }
            }



            string result ="";
            foreach (int item in CPR)
            {
                result = result + item.ToString();
            }
            return result;
        }

        public override string ToString()
        {
            return "Name: " + Name + " - ID: " + ID;
        }
    }
}
