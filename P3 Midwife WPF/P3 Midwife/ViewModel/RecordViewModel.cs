﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;
using P3_Midwife.Models;

namespace P3_Midwife.ViewModel
{
    public class RecordViewModel : DependencyObject
    {
        #region Variables
        #region Relaycommands
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand BackCommand { get; }
        public RelayCommand NewChildDialogCommand { get; }
        public RelayCommand MedicinCommand { get; }
        public RelayCommand SaveAndCompleteCommand { get; }
        public RelayCommand AddBirthInfo { get; }
        public RelayCommand AddContractionIVDripInfo { get; }
        public RelayCommand AddFetusObservationInfo { get; }
        public RelayCommand AddMicturitionInfo { get; }
        public RelayCommand AddVaginalExplorationInfo { get; }
        public RelayCommand RemoveMedicalService { get; }
        public RelayCommand AddMedicalService { get; }
        public RelayCommand OpenMedicalServicesToAdd { get; }
        public RelayCommand CreateChildCommand { get; }
        public RelayCommand Cancel { get; }
        public RelayCommand AppendNewNoteToNote { get; }
        #endregion
        #region DependcyProperties
        public static DependencyProperty CTGClassificationProperty = DependencyProperty.Register(nameof(CTGClassification), typeof(string), typeof(RecordViewModel));
        public static DependencyProperty ChildBirthDateProperty = DependencyProperty.Register(nameof(ChildBirthDate), typeof(DateTime), typeof(RecordViewModel));
        public static DependencyProperty ChildGenderProperty = DependencyProperty.Register(nameof(ChildGender), typeof(char), typeof(RecordViewModel));
        public static DependencyProperty ChildBirthTimeProperty = DependencyProperty.Register(nameof(ChildBirthTimeProperty), typeof(DateTime), typeof(RecordViewModel));
        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(PatientCurrent), typeof(Patient), typeof(RecordViewModel));
        public static DependencyProperty RecordProperty = DependencyProperty.Register(nameof(RecordCurrent), typeof(Record), typeof(RecordViewModel));
        public static DependencyProperty BirthInfoProperty = DependencyProperty.Register(nameof(BirthInfo), typeof(BirthInformation), typeof(RecordViewModel));
        public static DependencyProperty ContractionIVDripProperty = DependencyProperty.Register(nameof(ContractionIVDripInfo), typeof(ContractionIVDrip), typeof(RecordViewModel));
        public static DependencyProperty FetusObservationProperty = DependencyProperty.Register(nameof(FetusObservationInfo), typeof(FetusObservation), typeof(RecordViewModel));
        public static DependencyProperty MicturitionProperty = DependencyProperty.Register(nameof(MicturitionInfo), typeof(Micturition), typeof(RecordViewModel));
        public static DependencyProperty VaginalExplorationProperty = DependencyProperty.Register(nameof(VaginalExplorationInfo), typeof(VaginalExploration), typeof(RecordViewModel));
        public static DependencyProperty SelectedMedicalServiceProperty = DependencyProperty.Register(nameof(SelectedMedicalServiceInfo), typeof(MedicalService), typeof(RecordViewModel));
        public static DependencyProperty SelectedAvailableMedicalServiceProperty = DependencyProperty.Register(nameof(SelectedAvailableMedicalServiceInfo), typeof(MedicalService), typeof(RecordViewModel));
        public static DependencyProperty EmployeeProperty = DependencyProperty.Register(nameof(EmployeeCurrent), typeof(Employee), typeof(RecordViewModel));
        public static DependencyProperty PriorBirthComplicationsProperty = DependencyProperty.Register(nameof(PriorBirthComplications), typeof(bool), typeof(RecordViewModel));
        public static DependencyProperty NoteProperty = DependencyProperty.Register(nameof(Note), typeof(string), typeof(RecordViewModel));
        #endregion
        #region ObservableCollections
        private ObservableCollection<BirthInformation> _birthInformationList = new ObservableCollection<BirthInformation>();
        private ObservableCollection<ContractionIVDrip> _contractrionIVDripList = new ObservableCollection<ContractionIVDrip>();
        private ObservableCollection<FetusObservation> _fetusObservationList = new ObservableCollection<FetusObservation>();
        private ObservableCollection<Micturition> _micturitionList = new ObservableCollection<Micturition>();
        private ObservableCollection<VaginalExploration> _vaginalExplorationList = new ObservableCollection<VaginalExploration>();
        private ObservableCollection<MedicalService> _medicalServicesList = new ObservableCollection<MedicalService>();
        private ObservableCollection<MedicalService> _availableMedicalServices = new ObservableCollection<MedicalService>();
        private ObservableCollection<Patient> _children = new ObservableCollection<Patient>();
        private ObservableCollection<char> _genders = new ObservableCollection<char> { 'D', 'P' };
        private ObservableCollection<string> _ctgClassification = new ObservableCollection<string> { "Normal", "Afvigende", "Patologisk" };
        #endregion
        #endregion

        #region Properties
        public string Note
        {
            get { return this.GetValue(NoteProperty).ToString(); }
            set { this.SetValue(NoteProperty, value); }
        }

        public ObservableCollection<Patient> Children
        {
            get { return _children; }
            set { _children = value; }
        }

        public bool PriorBirthComplications
        {
            get { return (bool)this.GetValue(PriorBirthComplicationsProperty);}
            set { this.SetValue(PriorBirthComplicationsProperty,value); }
        }

        public ObservableCollection<char> Genders
        {
            get { return _genders; }
            set { _genders = value; }
        }

        public char ChildGender
        {
            get { return (char)this.GetValue(ChildGenderProperty); }
            set { this.SetValue(ChildGenderProperty, value); }
        }

        public string CTGClassification
        {
            get { return (string)this.GetValue(CTGClassificationProperty); }
            set { this.SetValue(CTGClassificationProperty, value); }
        }

        public DateTime ChildBirthDate
        {
            get { return (DateTime)this.GetValue(ChildBirthDateProperty); }
            set { this.SetValue(ChildBirthDateProperty, value); }
        }

        public DateTime ChildBirthTime
        {
            get { return (DateTime)this.GetValue(ChildBirthTimeProperty); }
            set { this.SetValue(ChildBirthTimeProperty, value); }
        }

        public ObservableCollection<MedicalService> MedicalServices
        {
            get { return _availableMedicalServices; }
            set { _availableMedicalServices = value; }
        }

        public MedicalService SelectedAvailableMedicalServiceInfo
        {
            get { return (MedicalService)this.GetValue(SelectedAvailableMedicalServiceProperty); }
            set { this.SetValue(SelectedAvailableMedicalServiceProperty, value); }
        }

        public MedicalService SelectedMedicalServiceInfo
        {
            get { return (MedicalService)this.GetValue(SelectedMedicalServiceProperty); }
            set { this.SetValue(SelectedMedicalServiceProperty, value); }
        }

        public ObservableCollection<MedicalService> MedicalServicesList
        {
            get { return _medicalServicesList; }
            set { _medicalServicesList = value; }
        }

        public ObservableCollection<BirthInformation> BirthInformationListProperty
        {
            get { return _birthInformationList; }
            set { _birthInformationList = value; }
        }

        public ObservableCollection<string> CTGClassificationValuesList
        {
            get { return _ctgClassification; }
            set { _ctgClassification = value; }
        }

        public ObservableCollection<ContractionIVDrip> ContractionListProperty
        {
            get { return _contractrionIVDripList; }
            set { _contractrionIVDripList = value; }
        }

        public ObservableCollection<FetusObservation> FetusObservationListProperty
        {
            get { return _fetusObservationList; }
            set { _fetusObservationList = value; }
        }

        public ObservableCollection<Micturition> MicturitionListProperty
        {
            get { return _micturitionList; }
            set { _micturitionList = value; }
        }

        public ObservableCollection<VaginalExploration> VaginalExplorationListProperty
        {
            get { return _vaginalExplorationList; }
            set { _vaginalExplorationList = value; }
        }
        public BirthInformation BirthInfo
        {
            get { return (BirthInformation)this.GetValue(BirthInfoProperty); }
            set { this.SetValue(BirthInfoProperty, value); }
        }
        public ContractionIVDrip ContractionIVDripInfo
        {
            get { return (ContractionIVDrip)this.GetValue(ContractionIVDripProperty); }
            set { this.SetValue(ContractionIVDripProperty, value); }
        }
        public FetusObservation FetusObservationInfo
        {
            get { return (FetusObservation)this.GetValue(FetusObservationProperty); }
            set { this.SetValue(FetusObservationProperty, value); }
        }
        public Micturition MicturitionInfo
        {
            get { return (Micturition)this.GetValue(MicturitionProperty); }
            set { this.SetValue(MicturitionProperty, value); }
        }
        public VaginalExploration VaginalExplorationInfo
        {
            get { return (VaginalExploration)this.GetValue(VaginalExplorationProperty); }
            set { this.SetValue(VaginalExplorationProperty, value); }
        }
        public Record RecordCurrent
        {
            get { return (Record)this.GetValue(RecordProperty); }
            set
            {
                
                this.SetValue(RecordProperty, value);
                BirthInformationListProperty.Clear();
                ContractionListProperty.Clear();
                FetusObservationListProperty.Clear();
                MicturitionListProperty.Clear();
                VaginalExplorationListProperty.Clear();
                MedicalServicesList.Clear();

                BirthInformationListProperty.AddRange(RecordCurrent.BirthInformationList);
                ContractionListProperty.AddRange(RecordCurrent.ContractionIVDripList);
                FetusObservationListProperty.AddRange(RecordCurrent.FetusObservationList);
                MicturitionListProperty.AddRange(RecordCurrent.MicturitionList);
                VaginalExplorationListProperty.AddRange(RecordCurrent.VaginalExplorationList);
                MedicalServicesList.AddRange(RecordCurrent.CurrentBill.BillItemList);
            }
        }

        public Patient PatientCurrent
        {
            get { return (Patient)this.GetValue(PatientProperty); }
            set { this.SetValue(PatientProperty, value);
                Children.Clear();
                Children.AddRange(PatientCurrent.Children.Where(x => !Children.Contains(x) && RecordCurrent.ChildCPR == x.CPR));
                PriorBirthComplications = EmployeeCurrent.PriorBirthComplications(PatientCurrent);
            }
        }
        public Employee EmployeeCurrent
        {
            get { return (Employee)this.GetValue(EmployeeProperty); }
            set { this.SetValue(EmployeeProperty, value); }
        }
        private void ExitAndSave()
        {
            if (PatientCurrent != null || RecordCurrent != null)
            {
                Filemanagement.SaveToDatabase(PatientCurrent);
                this.RecordCurrent.BirthInformationList.AddRange(BirthInformationListProperty.Where(x => !RecordCurrent.BirthInformationList.Contains(x)));
                this.RecordCurrent.ContractionIVDripList.AddRange(ContractionListProperty.Where(x => !RecordCurrent.ContractionIVDripList.Contains(x)));
                this.RecordCurrent.MicturitionList.AddRange(MicturitionListProperty.Where(x => !RecordCurrent.MicturitionList.Contains(x)));
                this.RecordCurrent.VaginalExplorationList.AddRange(VaginalExplorationListProperty.Where(x => !RecordCurrent.VaginalExplorationList.Contains(x)));
                this.RecordCurrent.FetusObservationList.AddRange(FetusObservationListProperty.Where(x => !RecordCurrent.FetusObservationList.Contains(x)));
                this.RecordCurrent.CurrentBill.BillItemList.AddRange(MedicalServicesList.Where(x => !RecordCurrent.CurrentBill.BillItemList.Contains(x)));
                Filemanagement.SaveRecord(RecordCurrent);
            }
        }
        #endregion

        public RecordViewModel()
        {
            SetValue(ChildBirthDateProperty, DateTime.Today);
            SetValue(ChildBirthTimeProperty, DateTime.Now);

            Messenger.Default.Register<NotificationMessage>(this, "LogOut", parameter =>
            {
                RecordCurrent.Note = RecordCurrent.NewNote;
                RecordCurrent.NewNote = null;
            });
            Messenger.Default.Register<Record>(this, "NewRecordToRecordView", (ActiveRecord) => { RecordCurrent = ActiveRecord; Note = ActiveRecord.Note; });
            Messenger.Default.Register<Patient>(this, "PatientToRecordView", (ActivePatient) => { PatientCurrent = ActivePatient; });
            Messenger.Default.Register<Employee>(this, "EmployeetoRecordView", (ActiveEmployee) => { EmployeeCurrent = ActiveEmployee; });
            Messenger.Default.Register<String>(this, "SaveToDatabase", (thestring) => ExitAndSave());

            _availableMedicalServices.AddRange(Ward.MedicalServicesList);
            #region Command Definitions
            //Command to add a medical service to the list of medical services
            this.AddMedicalService = new RelayCommand(parameter =>
            {
                MedicalServicesList.Add(SelectedAvailableMedicalServiceInfo);
            });
            //Command to create a new child in the system
            this.CreateChildCommand = new RelayCommand(parameter =>
            {
                Midwife tempMidwife = EmployeeCurrent as Midwife;
                ChildBirthDate = ChildBirthDate.Add(ChildBirthTime.TimeOfDay);
                tempMidwife.CreatePatient(PatientCurrent, ChildGender, ChildBirthDate);
                Patient tempChild = PatientCurrent.Children.Last();
                RecordCurrent.ChildCPR = tempChild.CPR;
                new P3_Midwife.Views.NewChildWindow(RecordCurrent);
                Messenger.Default.Send(RecordCurrent, "ChildRecordToNewChildView");
                Messenger.Default.Send(PatientCurrent, "PatientToNewChildView");
                Messenger.Default.Send(EmployeeCurrent, "EmployeetoNewChildView");
                Messenger.Default.Send(tempChild, "ChildToNewChildView");
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToNewChild"));
            });
            //Command to remove a medical service from the list of medical services
            this.RemoveMedicalService = new RelayCommand(parameter =>
            {
                MedicalServicesList.Remove(SelectedMedicalServiceInfo);
            });
            //Command to return to the record view without creating a child
            this.Cancel = new RelayCommand(parameter =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToRecord"));
            });
            //Command to log the user out of the system
            this.LogOutCommand = new RelayCommand(parameter =>
            {
                Filemanagement.SaveToDatabase(PatientCurrent);
                this.RecordCurrent.BirthInformationList.AddRange(BirthInformationListProperty.Where(x => !RecordCurrent.BirthInformationList.Contains(x)));
                this.RecordCurrent.ContractionIVDripList.AddRange(ContractionListProperty.Where(x => !RecordCurrent.ContractionIVDripList.Contains(x)));
                this.RecordCurrent.MicturitionList.AddRange(MicturitionListProperty.Where(x => !RecordCurrent.MicturitionList.Contains(x)));
                this.RecordCurrent.VaginalExplorationList.AddRange(VaginalExplorationListProperty.Where(x => !RecordCurrent.VaginalExplorationList.Contains(x)));
                this.RecordCurrent.FetusObservationList.AddRange(FetusObservationListProperty.Where(x => !RecordCurrent.FetusObservationList.Contains(x)));
                this.RecordCurrent.CurrentBill.BillItemList.AddRange(MedicalServicesList.Where(x => !RecordCurrent.CurrentBill.BillItemList.Contains(x)));
                Filemanagement.SaveRecord(RecordCurrent);
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            });
            //Command to close to program
            this.ExitCommand = new RelayCommand(parameter =>
            {
                ExitAndSave();
            });
            //Command to return to the previous view
            this.BackCommand = new RelayCommand(Parameter =>
            {
                Filemanagement.SaveToDatabase(PatientCurrent);
                this.RecordCurrent.BirthInformationList.AddRange(BirthInformationListProperty.Where(x => !RecordCurrent.BirthInformationList.Contains(x)));
                BirthInformationListProperty.Clear();
                this.RecordCurrent.ContractionIVDripList.AddRange(ContractionListProperty.Where(x => !RecordCurrent.ContractionIVDripList.Contains(x)));
                ContractionListProperty.Clear();
                this.RecordCurrent.MicturitionList.AddRange(MicturitionListProperty.Where(x => !RecordCurrent.MicturitionList.Contains(x)));
                MicturitionListProperty.Clear();
                this.RecordCurrent.VaginalExplorationList.AddRange(VaginalExplorationListProperty.Where(x => !RecordCurrent.VaginalExplorationList.Contains(x)));
                VaginalExplorationListProperty.Clear();
                this.RecordCurrent.FetusObservationList.AddRange(FetusObservationListProperty.Where(x => !RecordCurrent.FetusObservationList.Contains(x)));
                FetusObservationListProperty.Clear();
                this.RecordCurrent.CurrentBill.BillItemList.AddRange(MedicalServicesList.Where(x => !RecordCurrent.CurrentBill.BillItemList.Contains(x)));
                MedicalServicesList.Clear();
                Filemanagement.SaveRecord(RecordCurrent);
                Messenger.Default.Send(EmployeeCurrent, "Employee");
                Messenger.Default.Send(PatientCurrent, "Patient");
                Messenger.Default.Send(new NotificationMessage("ToPatient"));
            });
            //Command to open a view where a new child can be created in the system
            this.NewChildDialogCommand = new RelayCommand(parameter =>
            {
                this.RecordCurrent.BirthInformationList.AddRange(BirthInformationListProperty.Where(x => !RecordCurrent.BirthInformationList.Contains(x)));
                this.RecordCurrent.ContractionIVDripList.AddRange(ContractionListProperty.Where(x => !RecordCurrent.ContractionIVDripList.Contains(x)));
                this.RecordCurrent.MicturitionList.AddRange(MicturitionListProperty.Where(x => !RecordCurrent.MicturitionList.Contains(x)));
                this.RecordCurrent.VaginalExplorationList.AddRange(VaginalExplorationListProperty.Where(x => !RecordCurrent.VaginalExplorationList.Contains(x)));
                this.RecordCurrent.FetusObservationList.AddRange(FetusObservationListProperty.Where(x => !RecordCurrent.FetusObservationList.Contains(x)));
                this.RecordCurrent.CurrentBill.BillItemList.AddRange(MedicalServicesList.Where(x => !RecordCurrent.CurrentBill.BillItemList.Contains(x)));
                Messenger.Default.Send(new NotificationMessage("NewChildDialog"));
            });
            //Command to save and archive a record, so that it cannot be edited anymore. The patient is also transfered away from the room
            this.SaveAndCompleteCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("RecordSave"));
                this.RecordCurrent.Note += "\n" + RecordCurrent.NewNote;
                this.RecordCurrent.BirthInformationList.AddRange(BirthInformationListProperty.Where(x => !RecordCurrent.BirthInformationList.Contains(x)));
                this.RecordCurrent.BirthInformationList.AddRange(BirthInformationListProperty);
                BirthInformationListProperty.Clear();
                this.RecordCurrent.ContractionIVDripList.AddRange(ContractionListProperty.Where(x => !RecordCurrent.ContractionIVDripList.Contains(x)));
                ContractionListProperty.Clear();
                this.RecordCurrent.MicturitionList.AddRange(MicturitionListProperty.Where(x => !RecordCurrent.MicturitionList.Contains(x)));
                MicturitionListProperty.Clear();
                this.RecordCurrent.VaginalExplorationList.AddRange(VaginalExplorationListProperty.Where(x => !RecordCurrent.VaginalExplorationList.Contains(x)));
                VaginalExplorationListProperty.Clear();
                this.RecordCurrent.FetusObservationList.AddRange(FetusObservationListProperty.Where(x => !RecordCurrent.FetusObservationList.Contains(x)));
                FetusObservationListProperty.Clear();
                this.RecordCurrent.CurrentBill.BillItemList.AddRange(MedicalServicesList.Where(x => !RecordCurrent.CurrentBill.BillItemList.Contains(x)));
                MedicalServicesList.Clear();
                RecordCurrent.IsActive = false;
                (EmployeeCurrent as Midwife).TransferPatient(PatientCurrent);
                Filemanagement.SaveToDatabase(PatientCurrent);
                Filemanagement.SaveRecord(RecordCurrent);
                Messenger.Default.Send(new NotificationMessage("ToPatient"));
                Messenger.Default.Send(EmployeeCurrent, "Employee");
                Messenger.Default.Send(PatientCurrent, "Patient");
            });
            //Command to add a new line of the type Birth Information in the record view
            this.AddBirthInfo = new RelayCommand(parameter =>
             {
                 BirthInfo = new BirthInformation();
                 BirthInfo.CurrentEmployee = EmployeeCurrent;
                 BirthInformationListProperty.Add(BirthInfo);
             });
            //Command to add a new line of the type ContractionIVDrip Informaion  in the record view
            this.AddContractionIVDripInfo = new RelayCommand(parameter =>
            {
                ContractionIVDripInfo = new ContractionIVDrip();
                ContractionIVDripInfo.CurrentEmployee = EmployeeCurrent;
                ContractionListProperty.Add(ContractionIVDripInfo);
            });
            //Command to add a new line of the type Fetus Observation Information in the record view
            this.AddFetusObservationInfo = new RelayCommand(parameter =>
            {

                FetusObservationInfo = new FetusObservation();
                FetusObservationInfo.CurrentEmployee = EmployeeCurrent;
                FetusObservationListProperty.Add(FetusObservationInfo);
            });
            //Command to add a new line of the type Micturition Information in the record view
            this.AddMicturitionInfo = new RelayCommand(parameter =>
            {
                MicturitionInfo = new Micturition();
                MicturitionInfo.CurrentEmployee = EmployeeCurrent;
                MicturitionListProperty.Add(MicturitionInfo);
            });
            //Command to add a new line of the type Vaginal Exploration Information in the record view
            this.AddVaginalExplorationInfo = new RelayCommand(parameter =>
            {
                VaginalExplorationInfo = new VaginalExploration();
                VaginalExplorationInfo.CurrentEmployee = EmployeeCurrent;
                VaginalExplorationListProperty.Add(VaginalExplorationInfo);
            });
            #endregion
        }
    }
}
