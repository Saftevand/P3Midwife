using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife.Models
{
    public class Micturition
    {
        #region
        private Employee _currentEmployee;
        private DateTime _time;
        private string _micturitionNote;
        #endregion

        #region
        public DateTime Time { get { return _time; } set { _time = value; } }
        public string MicturitionNote { get { return _micturitionNote; } set { _micturitionNote = value; } }
        public Employee CurrentEmployee { get { return _currentEmployee; } set { _currentEmployee = value; } }
        #endregion

        public Micturition()
        {
            _time = DateTime.Now;
        }

        public override string ToString()
        {
            return ("_micturition|" + Time.ToString() + "|" + MicturitionNote);
        }
    }
}
