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

        public Midwife(int id, string name, string password, int telephonenumber, string email, int clearance) 
            : base(id, name, password, telephonenumber, email, clearance)
        {
        }

    }
}
