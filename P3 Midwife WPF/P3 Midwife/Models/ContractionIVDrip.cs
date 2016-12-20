using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife.Models
{
    public class ContractionIVDrip
    {
        #region Variables
        private Employee _currentEmployee;
        private DateTime _time;
        private int _numberOfContractionsPerMinute;
        private int _contractionPain;
        private int _SDripMLPerHour;
        #endregion

        #region Property
        public DateTime Time { get { return _time; } set { _time = value; } }
        public int NumberOfContractionsPerMinute { get { return _numberOfContractionsPerMinute; } set { _numberOfContractionsPerMinute = value; } }
        public int ContractionPain { get { return _contractionPain; } set { _contractionPain = value; } }
        public int SDripMlPerHour { get { return _SDripMLPerHour; } set { _SDripMLPerHour = value; } }
        public Employee CurrentEmployee { get { return _currentEmployee; } set { _currentEmployee = value; } }
        #endregion

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
