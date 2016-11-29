﻿using System;
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
        public int Clearance { get; set; }
        public List<Patient> _currentPatients = new List<Patient>();

        public Employee(int id, string name, string password, int telephonenumber, string email, int clearance)
        {
            this.ID = id;
            this.Name = name;
            this.Password = password;
            this.TelephoneNumber = telephonenumber;
            this.Email = email;
            this.Clearance = clearance;
        }

        public List<Patient> CurrentPatients { get { return _currentPatients; } set { _currentPatients = value; } }

        public override string ToString()
        {
            return this.Name + " " + this.Password + " " + this.TelephoneNumber + " " + this.Email + " " + this.Clearance;
        }

        public Patient FindPatient(string cpr)
        {
            return Ward.Patients.Find(x => x.CPR == cpr);
        }


    }
}
