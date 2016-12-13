using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife.Models
{
    public class FetusObservation
    {
        private DateTime _time;
        private string _hearthfrequency;
        private string _CTG;
        private string _CTGClassification;
        private string _STAN;
        private double _scalppH;
        private double _scalpLactate;
        public DateTime Time { get { return _time; } set { _time = value; } }
        public string HearthFrequency { get { return _hearthfrequency; } set { _hearthfrequency = value; } }
        public string CTG { get { return _CTG; } set { _CTG = value; } }
        public string CTGClassification { get { return _CTGClassification; } set { _CTGClassification = value; } }
        public string STAN { get { return _STAN; } set { _STAN = value; } }
        public double ScalppH { get { return _scalppH; } set { _scalppH = value; } }
        public double ScalpLactate { get { return _scalpLactate; } set { _scalpLactate = value; } }

        public FetusObservation()
        {
            _time = DateTime.Now;
        }

        public override string ToString()
        {
            return ("_fetusObservation|" + Time.ToString() + "|" + HearthFrequency + "|" + CTG + "|" + CTGClassification + "|" + STAN + "|" + ScalppH.ToString() + "|" + ScalpLactate.ToString());
        }
    }
}
