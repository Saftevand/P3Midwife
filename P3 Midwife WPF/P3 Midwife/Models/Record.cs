using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class Record
    {
        #region Instansvariabler
        private Patient _patient;
        private double _circumferenceHead;
        private double _circumferenceStomach;
        private double _bloodSugar;
        private string _GA;
        private List<String> _diseases = new List<string>();
        private double _navelpHVenous;
        private double _navelpHArterial;
        private double _navelBaseExcess;
        private int _fetusPosition;
        private double _placentaWeight;
        private bool _KVitamin;
        private int _apgarOneMinute;
        private int _apgarFiveMinutes;
        private int _apgarTenMinutes;
        private DateTime _timeOfBirth;//
        private string _diagnosis;
        private List<_vaginalExploration> _vaginalExplorationList = new List<_vaginalExploration>();
        private List<_contractionIVDrip> _contractionIVDRIPList = new List<_contractionIVDrip>();
        private List<_micturition> _micturitionList = new List<_micturition>();
        private List<_fetusObservation> _fetusObservationList = new List<_fetusObservation>();
        private List<_birthInformation> _birthInformationList = new List<_birthInformation>();
        private Bill _bill;
        public static int RecordID;//
     

        public struct _vaginalExploration
        {
            private DateTime _time;
            private int _collum;
            private int _dilation;
            private string _position;
            private int _rotation;
            private string _consistency;
            private string _location;
            private string _amnioticFluid;
            private string _note;

            public string Note { get { return _note; } set { _note = value; } }
            public DateTime Time { get { return _time; } set { _time = value; } }
            public int Collum { get { return _collum; } set { _collum = value; } }
            public int Dialation { get { return _dilation; } set { _dilation = value; } }
            public string Position { get { return _position; } set { _position = value; } }
            public int Rotation { get { return _rotation; } set { _rotation = value; } }
            public string Consistency { get { return _consistency; } set { _consistency = value; } }
            public string Location { get { return _location; } set { _location = value; } }
            public string AmnioticFluid { get { return _amnioticFluid; } set { _amnioticFluid = value; } }

        }

        public struct _contractionIVDrip
        {
            private DateTime _time;
            private int _numberOfContractionsPerMinute;
            private int _contractionPain;
            private int _SDripMLPerHour;
            private string _note;
            public string Note { get { return _note; } set { _note = value; } }

            public DateTime Time { get { return _time; } set { _time = value; } }
            public int NumberOfContractionsPerMinute { get { return _numberOfContractionsPerMinute; } set { _numberOfContractionsPerMinute = value; } }
            public int ContractionPain { get { return _contractionPain; } set { _contractionPain = value; } }
            public int SDripMlPerHour { get { return _SDripMLPerHour; } set { _SDripMLPerHour = value; } }
        }

        public struct _micturition
        {
            private DateTime _time;
            private string _micturitionNote;
            private string _note;
            public string Note { get { return _note; } set { _note = value; } }
            public DateTime Time { get { return _time; } set { _time = value; } }
            public string MicturitionNote { get { return _micturitionNote; } set { _micturitionNote = value; } }
        }

        public struct _fetusObservation
        {
            private DateTime _time;
            private string _hearthfrequency;
            private string _CTG;
            private string _CTGClassification;
            private string _STAN;
            private double _scalppH;
            private double _scalpLactate;
            private string _note;
            public string Note { get { return _note; } set { _note = value; } }
            public DateTime Time { get { return _time; } set { _time = value; } }
            public string HearthFrequency { get { return _hearthfrequency; } set { _hearthfrequency = value; } }
            public string CTG { get { return _CTG; } set { _CTG = value; } }
            public string CTGClassification { get { return _CTGClassification; } set { _CTGClassification = value; } }
            public string STAN { get { return _STAN; } set { _STAN = value; } }
            public double ScalppH { get { return _scalppH; } set { _scalppH = value; } }
            public double ScalpLactate { get { return _scalpLactate; } set { _scalpLactate = value; } }
        }

        public struct _birthInformation
        {
            private DateTime _time;
            private string _result;
            private string _amnioticFluid;
            private string _amountOfFluid;
            private double _bloodAmount;
            private string _bleedingCause;
            private string _birthPosition;
            private string _note;
            public string Note { get { return _note; } set { _note = value; } }
            public DateTime Time { get { return _time; } set { _time = value; } }
            public string Result { get { return _result; } set { _result = value; } }
            public string AmnioticFluid { get { return _amnioticFluid; } set { _amnioticFluid = value; } }
            public string AmountOfFluid { get { return _amountOfFluid; } set { _amountOfFluid = value; } }
            public double BloodAmount { get { return _bloodAmount; } set { _bloodAmount = value; } }
            public string BleedingCause { get { return _bleedingCause; } set { _bleedingCause = value; } }
            public string BirthPosition { get { return _birthPosition; } set { _birthPosition = value; } }
        }
        #endregion

        #region Properties
        public List<_vaginalExploration> VaginalExplorationList { get { return this._vaginalExplorationList; } set { _vaginalExplorationList = value; } }
        public List<_micturition> MicturitionList { get { return this._micturitionList; } }
        public List<_fetusObservation> FetusObservationList { get { return this._fetusObservationList; } set { _fetusObservationList = value; } }
        public List<_contractionIVDrip> ContractionIVDripList { get { return this._contractionIVDRIPList; } set { _contractionIVDRIPList = value; } }
        public List<_birthInformation> BirthInformationList { get { return this._birthInformationList; } set { _birthInformationList = value; } }
        public double CircumferenceHead { get { return this._circumferenceHead; } set { this._circumferenceHead = value; } }
        public double CircumferenceStomach { get; set; }
        public double BloodSugar { get { return this._bloodSugar; } set { this._bloodSugar = value; } }
        public string GA { get { return this._GA; } set { this._GA = value; } }
        public List<string> Diseases { get { return this._diseases; } set { this._diseases = value; } }
        public double NavelpHVenous { get { return this._navelpHVenous; } set { this._navelpHVenous = value; } }
        public double NavelpHArterial { get { return this._navelpHArterial; } set { this._navelpHArterial = value; } }
        public double NavelBaseExcess { get { return this._navelBaseExcess; } set { this._navelBaseExcess = value; } }
        public int FetusPosition { get { return this._fetusPosition; } set { this._fetusPosition = value; } }
        public double PlacentaWeight { get { return this._placentaWeight; } set { this._placentaWeight = value; } }
        public bool KVitamin { get { return this._KVitamin; } set { this._KVitamin = value; } }
        public int ApgarOneMinute { get { return this._apgarOneMinute; } set { this._apgarOneMinute = value; } }
        public int ApgarFiveMinutes { get { return this._apgarFiveMinutes; } set { this._apgarFiveMinutes = value; } }
        public int ApgarTenMinutes { get { return this._apgarTenMinutes; } set { this._apgarTenMinutes = value; } }
        public DateTime TimeOfBirth { get { return this._timeOfBirth; } set { this._timeOfBirth = value; } }
        public string Diagnosis { get { return this._diagnosis; } set { this._diagnosis = value; } }
        public Bill CurrentBill { get { return _bill; } set { _bill = value; } }
        public bool IsActive { get; set; }
        public Patient RecordsPatient { get { return _patient; } }
        public int ThisRecordID { get; set; }
        #endregion

        public Record(Patient Patient)
        {
            this._patient = Patient;
            this.IsActive = true;
            this.CurrentBill = new Bill(RecordID);
            ThisRecordID = RecordID;
            RecordID++;
        }

        public int CalculateSGA(/*TODO - patrick gøred*/)
        {
            throw new NotImplementedException("Patrick laver den");
        }
    }
}
