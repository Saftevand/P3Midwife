using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife.Models
{
    public class ContractionIVDrip
    {
        private Employee _currentEmployee;
        public Employee CurrentEmployee { get { return _currentEmployee; } set { _currentEmployee = value; } }


        private DateTime _time;
        private int _numberOfContractionsPerMinute;
        private int _contractionPain;
        private int _SDripMLPerHour;
        public DateTime Time { get { return _time; } set { _time = value; } }
        public int NumberOfContractionsPerMinute { get { return _numberOfContractionsPerMinute; } set { _numberOfContractionsPerMinute = value; } }
        public int ContractionPain { get { return _contractionPain; } set { _contractionPain = value; } }
        public int SDripMlPerHour { get { return _SDripMLPerHour; } set { _SDripMLPerHour = value; } }

        public ContractionIVDrip()
        {
            _time = DateTime.Now;
        }

        public override string ToString()
        {
            return ("_contractionIVDrip|" + Time.ToString() + "|" + NumberOfContractionsPerMinute.ToString() + "|" + ContractionPain.ToString() + "|" + SDripMlPerHour.ToString());
        }
    }
}
