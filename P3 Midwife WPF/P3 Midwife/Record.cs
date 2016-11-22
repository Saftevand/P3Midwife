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
        private List<String> _diseases;
        private double _navelpHVenous;
        private double _navelpHArterial;
        private double _navelBaseExcess;
        private int _fetusPosition;
        private double _placentaWeight;
        private bool _KVitamin;
        private int _apgarOneMinute;
        private int _apgarFiveMinutes;
        private int _apgarTenMinutes;
        private DateTime _timeOfBirth;
        private string _diagnosis;
        private List<_vaginalExploration> _vaginalExplorationList;
        private List<_contractionIVDrip> _contractionIVDRIPList;
        private List<_micturition> _micturitionList;
        private List<_fetusObservation> _fetusObservationList;
        private List<_birthInformation> _birthInformationList;
        private Bill _bill = new Bill();
     

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
        }

        public struct _contractionIVDrip
        {
            private DateTime _time;
            private int _numberOfContractionsPerMinutes;
            private int _contractionPain;
            private int _SDripMLPerHour;
        }

        public struct _micturition
        {
            private DateTime _time;
            private string _micturitionNote;
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
        }

        public Record(Patient Patient)
        {
            this._patient = Patient;
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
        #endregion







    }
}
