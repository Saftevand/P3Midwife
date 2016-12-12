using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife.Models
{
    public class VaginalExploration
    {
        private DateTime _time;
        private int _collum;
        private int _dilation;
        private string _position;
        private int _rotation;
        private string _consistency;
        private string _location;
        private string _amnioticFluid;

        public VaginalExploration()
        {
            _time = DateTime.Now;
        }

        public DateTime Time { get { return _time; } set { _time = value; } }
        public int Collum { get { return _collum; } set { _collum = value; } }
        public int Dialation { get { return _dilation; } set { _dilation = value; } }
        public string Position { get { return _position; } set { _position = value; } }
        public int Rotation { get { return _rotation; } set { _rotation = value; } }
        public string Consistency { get { return _consistency; } set { _consistency = value; } }
        public string Location { get { return _location; } set { _location = value; } }
        public string AmnioticFluid { get { return _amnioticFluid; } set { _amnioticFluid = value; } }

        public override string ToString()
        {
            return ("_vaginalExp|" + Time.ToString() + "|" + Collum.ToString() + "|" + Dialation.ToString() + "|" + Position + "|" + Rotation.ToString() + "|" + Consistency + "|" + Location + "|" + AmnioticFluid);
        }
    }
}
