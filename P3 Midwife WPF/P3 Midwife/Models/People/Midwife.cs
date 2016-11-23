using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace P3_Midwife
{
    public class Midwife : Employee
    {
        private List<Patient> _currentPatients = new List<Patient>();

        public Midwife(int id, string name, string password, int telephonenumber, string email, int clearance) 
            : base(id, name, password, telephonenumber, email, clearance)
        {
        }

        public List<Patient> CurrentPatients { get { return _currentPatients; } set { _currentPatients = value; } }

    }
}
