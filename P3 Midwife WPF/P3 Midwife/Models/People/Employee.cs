using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;

namespace P3_Midwife
{
    public abstract class Employee
    {      
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int TelephoneNumber { get; set; }
        public string Email { get; set; }
        
        public List<Patient> _currentPatients = new List<Patient>();

        public Employee(int id, string name, string password, int telephonenumber, string email)
        {
            this.ID = id;
            this.Name = name;
            this.Password = password;
            this.TelephoneNumber = telephonenumber;
            this.Email = email;
        }

        public List<Patient> CurrentPatients { get { return _currentPatients; } set { _currentPatients = value; } }

        public override string ToString()
        {
            return this.Name + " " + this.Password + " " + this.TelephoneNumber + " " + this.Email;
        }

        //Finds a patient in the wards patient list based on cpr
        public Patient FindPatient(string cpr)
        {
            return Ward.Patients.Find(x => x.CPR == cpr);
        }


    }
}
