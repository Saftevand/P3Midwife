using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife.Models
{
    public class BirthInformation
    {
        private Employee _currentEmployee;
        public Employee CurrentEmployee { get { return _currentEmployee; } set { _currentEmployee = value; } }


        private DateTime _time = new DateTime();
        private string _result;
        private string _amnioticFluid;
        private string _amountOfFluid;
        private double _bloodAmount;
        private string _bleedingCause;
        private string _birthPosition;
        public DateTime Time { get { return _time; } set { _time = value; } }
        public string Result { get { return _result; } set { _result = value; } }
        public string AmnioticFluid { get { return _amnioticFluid; } set { _amnioticFluid = value; } }
        public string AmountOfFluid { get { return _amountOfFluid; } set { _amountOfFluid = value; } }
        public double BloodAmount { get { return _bloodAmount; } set { _bloodAmount = value; } }
        public string BleedingCause { get { return _bleedingCause; } set { _bleedingCause = value; } }
        public string BirthPosition { get { return _birthPosition; } set { _birthPosition = value; } }

        public BirthInformation()
        {
            _time = DateTime.Now;
        }

        public override string ToString()
        {
            return ("_birthInformation|" + Time.ToString() + "|" + Result + "|" + AmnioticFluid + "|" + AmountOfFluid + "|" + BloodAmount.ToString() + "|" + BleedingCause + "|" + BirthPosition);
        }
    }
}
