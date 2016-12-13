﻿using P3_Midwife.Models;
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
        private DateTime _date = new DateTime();
        private double _circumferenceHead;
        private bool _isCompleted = false;
        private double _circumferenceStomach;
        private double _weight;
        private double _length;
        private double _bloodSugar;
        private List<String> _diseases = new List<string>();
        private string _GA;
        private int _ho;
        private int _ao;
        private int _fetuspre;
        private string _numberofchildren;
        private string _furtherNotice;
        private bool _sucking;
        private bool _nose;
        private bool _pharynx;
        private bool _ventricle;
        private double _navelpHVenous;
        private double _navelpHArterial;
        private double _navelBaseExcessVenous;
        private double _navelBaseExcessArterial;
        private int _fetusPosition;
        private double _placentaWeight;
        private bool _KVitamin;
        private int _apgarOneMinute;
        private int _apgarFiveMinutes;
        private int _apgarTenMinutes;
        private string _apgarOneMinuteNote;
        private string _apgarFiveMinuteNote;
        private string _apgarTenMinuteNote;
        private string _breastFeedingNote;
        private DateTime _timeOfBirth;
        private Patient _mother;
        private string _diagnosis;
        private List<VaginalExploration> _vaginalExplorationList = new List<VaginalExploration>();
        private List<ContractionIVDrip> _contractionIVDRIPList = new List<ContractionIVDrip>();
        private List<Micturition> _micturitionList = new List<Micturition>();
        private List<FetusObservation> _fetusObservationList = new List<FetusObservation>();
        private List<BirthInformation> _birthInformationList = new List<BirthInformation>();
        private Bill _bill;
        public static int RecordID;
        public int thisRecordID;
        private string _note;
        private string _newNote;

        #endregion

        #region Properties
        public List<VaginalExploration> VaginalExplorationList { get { return this._vaginalExplorationList; } set { _vaginalExplorationList = value; } }
        public List<Micturition> MicturitionList { get { return this._micturitionList; } }
        public List<FetusObservation> FetusObservationList { get { return this._fetusObservationList; } set { _fetusObservationList = value; } }
        public List<ContractionIVDrip> ContractionIVDripList { get { return this._contractionIVDRIPList; } set { _contractionIVDRIPList = value; } }
        public List<BirthInformation> BirthInformationList { get { return this._birthInformationList; } set { _birthInformationList = value; } }
        public double CircumferenceHead { get { return this._circumferenceHead; } set { this._circumferenceHead = value; } }
        public double CircumferenceStomach { get { return this._circumferenceStomach; } set { this._circumferenceStomach = value; } }
        public double BloodSugar { get { return this._bloodSugar; } set { this._bloodSugar = value; } }
        public string GA { get { return this._GA; } set { this._GA = value; } }
        public bool IsCompleted { get { return _isCompleted; } set { _isCompleted = value; } }
        public double Weight { get { return this._weight; } set { this._weight = value; } }
        public double Length { get { return this._length; } set { this._length = value; } }
        public int HO { get { return this._ho; } set { this._ho = value; } }
        public int AO { get { return this._ao; } set { this._ao = value; } }
        public bool Sucking { get { return this._sucking; } set { this._sucking = value; } }
        public int FetusPre { get { return this._fetuspre; } set { this._fetuspre = value; } }
        public string FurtherNotice { get { return this._furtherNotice; } set { this._furtherNotice = value; } }
        public string NumberOfChildren { get { return this._numberofchildren; } set { this._numberofchildren = value; } }
        public List<string> Diseases { get { return this._diseases; } set { this._diseases = value; } }
        public double NavelpHVenous { get { return this._navelpHVenous; } set { this._navelpHVenous = value; } }
        public double NavelpHArterial { get { return this._navelpHArterial; } set { this._navelpHArterial = value; } }
        public double NavelBaseExcessArterial { get { return this._navelBaseExcessArterial; } set { this._navelBaseExcessArterial = value; } }
        public double NavelBaseExcessVenous { get { return this._navelBaseExcessVenous; } set { this._navelBaseExcessVenous = value; } }
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
        public bool Nose { get { return this._nose; } set { this._nose = value; } }
        public bool Pharynx { get { return this._pharynx; } set { this._pharynx = value; } }
        public bool Ventricle { get { return this._ventricle; } set { this._ventricle = value; } }
        public DateTime Date { get { return this._date; } set { this._date = value; } }
        public Patient Mother { get { return _mother; } set { _mother = value; } }
        public string Note { get { return _note; } set { _note += value; } }
        public string NewNote { get { return _newNote; } set { _newNote = value; } }
        public string BreastFeedingNote { get { return _breastFeedingNote; } set { _breastFeedingNote = value; } }
        public string ApgarOneMinuteNote { get { return _apgarOneMinuteNote; } set { _apgarOneMinuteNote = value; } }
        public string ApgarFiveMinuteNote { get { return _apgarFiveMinuteNote; } set { _apgarFiveMinuteNote = value; } }
        public string ApgarTenMinuteNote { get { return _apgarTenMinuteNote; } set { _apgarTenMinuteNote = value; } }
        #endregion

        public Record(Patient Patient)
        {
            this._patient = Patient;
            this.IsActive = true;
            ThisRecordID = RecordID;
            this._date = DateTime.Now;
            RecordID++;
            thisRecordID = RecordID++;
            this.CurrentBill = new Bill(this);
        }

        //Creates Bill file and deactivates the bill.
        public void ArchiveBill()
        {
            this.CurrentBill.Active = false;
        }

        public int CalculateSGA(/*TODO - patrick gøred*/)
        {
            throw new NotImplementedException("Patrick laver den");
        }

        public string ToFile()
        {
            string ReturnString = "";
            foreach (ContractionIVDrip item in _contractionIVDRIPList)
            {
                ReturnString = ReturnString + item.ToString();
            }
            foreach (VaginalExploration item in _vaginalExplorationList)
            {
                ReturnString = ReturnString + item.ToString();
            }
            foreach (Micturition item in _micturitionList)
            {
                ReturnString = ReturnString + item.ToString();
            }
            foreach (FetusObservation item in FetusObservationList)
            {
                ReturnString = ReturnString + item.ToString();
            }
            foreach (BirthInformation item in BirthInformationList)
            {
                ReturnString = ReturnString + item.ToString();
            }
            ReturnString = (ReturnString + "_variables|" + ThisRecordID.ToString() + "|" + TimeOfBirth.ToString() + "|" + CircumferenceHead.ToString() + "|" + CircumferenceStomach.ToString() + "|" + BloodSugar.ToString() + "|" + GA + "|" + NavelpHVenous.ToString() + "|" + NavelpHArterial.ToString() + "|" + NavelBaseExcessArterial.ToString() + "|" + NavelBaseExcessVenous.ToString() + "|" + FetusPosition.ToString() + "|" + PlacentaWeight.ToString() + "|" + KVitamin.ToString() + "|" + ApgarOneMinute.ToString() + "|" + ApgarFiveMinutes.ToString() + "|" + ApgarTenMinutes.ToString() + "|" + AO.ToString() + "|" + HO.ToString() + "|" + Weight.ToString() + "|" + Length.ToString() + "|" + NumberOfChildren + "|" + FurtherNotice + "|" + Sucking.ToString() + "|" + Nose.ToString() + "|" + Pharynx.ToString() + "|" + Ventricle.ToString() + "|" + Diagnosis + "|" + Note);
            foreach (string item in Diseases)
            {
                ReturnString = ReturnString + "|" + item;
            }
            return (ReturnString);
        }
    }
}
