using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class Employee
    {
        public int ID { get;}
        public string Name { get; }
        public string Password { get; set; }
        public int TelephoneNumber { get; set; }
        public string Email { get; set; }

        public Employee(int id, string name, string password, int telephonenumber, string email)
        {
            this.ID = id;
            this.Name = name;
            this.Password = password;
            this.TelephoneNumber = telephonenumber;
            this.Email = email;
        }

        public override string ToString()
        {
            return "Name: " + Name + " - ID: " + ID;
        }
    }
}
