using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class SOSU : Employee
    {
        public SOSU(int id, string name, string password, int telephonenumber, string email) 
            : base(id, name, password, telephonenumber, email)
        {

        }

    }
}
