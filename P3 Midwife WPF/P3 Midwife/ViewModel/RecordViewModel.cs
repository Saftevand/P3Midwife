using System;
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
        private Employee _currentEmployee;
        private ObservableCollection<char> _genders = new ObservableCollection<char> { 'D', 'P' };
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

        private ObservableCollection<BirthInformation> _birthInformationList = new ObservableCollection<BirthInformation>();
        private ObservableCollection<ContractionIVDrip> _contractrionIVDripList= new ObservableCollection<ContractionIVDrip>();
        private ObservableCollection<FetusObservation> _fetusObservationList = new ObservableCollection<FetusObservation>();
        private ObservableCollection<Micturition> _micturitionList = new ObservableCollection<Micturition>();
        private ObservableCollection<VaginalExploration> _vaginalExplorationList = new ObservableCollection<VaginalExploration>();
        private ObservableCollection<MedicalService> _medicalServicesList = new ObservableCollection<MedicalService>();
        private ObservableCollection<MedicalService> _availableMedicalServices = new ObservableCollection<MedicalService>();

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
                MedicalServicesList.AddRange(RecordCurrent.CurrentBill.BillItemList);
                //BirthInformationListProperty = new ObservableCollection<BirthInformation>();
                //VaginalExplorationListProperty = new ObservableCollection<VaginalExploration>();
                //MicturitionListProperty = new ObservableCollection<Micturition>();
                //ContractionListProperty = new ObservableCollection<ContractionIVDrip>();
                //MicturitionListProperty = new ObservableCollection<Micturition>();
                //BirthInformationListProperty.AddRange(RecordCurrent.BirthInformationList);
                //VaginalExplorationListProperty.AddRange(RecordCurrent.VaginalExplorationList);
                //MicturitionListProperty.AddRange(RecordCurrent.MicturitionList);
                //ContractionListProperty.AddRange(RecordCurrent.ContractionIVDripList);
                //MicturitionListProperty.AddRange(RecordCurrent.MicturitionList);
                //InitiateCollections();
            }
        }
        public Patient PatientCurrent
        {
            get { return (Patient)this.GetValue(PatientProperty); }
            set { this.SetValue(PatientProperty, value); }
        }
        public Employee EmployeeCurrent
        {
            get { return _currentEmployee; }
            set { _currentEmployee = value; }
        }

        public RecordViewModel()
        {
            this.Cancel = new RelayCommand(parameter =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToRecord"));
            });
            Messenger.Default.Register<NotificationMessage>(this, "LogOut", parameter =>
            {
                RecordCurrent.Note = RecordCurrent.NewNote;
                RecordCurrent.NewNote = null;
            });
            Messenger.Default.Register<Record>(this, "NewRecordToRecordView", (ActiveRecord) => { RecordCurrent = ActiveRecord; });
            Messenger.Default.Register<Patient>(this, "PatientToRecordView", (ActivePatient) => { PatientCurrent = ActivePatient; });
            Messenger.Default.Register<Employee>(this, "EmployeetoRecordView", (ActiveEmployee) => { EmployeeCurrent = ActiveEmployee; });            

            _availableMedicalServices.AddRange(Ward.MedicalServicesList);

            this.AddMedicalService = new RelayCommand(parameter =>
            {
                MedicalServicesList.Add(SelectedAvailableMedicalServiceInfo);
            });

            this.CreateChildCommand = new RelayCommand(parameter =>
            {
                Midwife tempMidwife = _currentEmployee as Midwife;
                tempMidwife.CreatePatient(PatientCurrent, ChildGender, ChildBirthDate.Date+ChildBirthTime.TimeOfDay);
                Messenger.Default.Send(PatientCurrent, "PatientToNewChildView");
                Messenger.Default.Send(EmployeeCurrent, "EmployeetoNewChildView");
                Patient tempChild = PatientCurrent.Children.Find(x => x.BirthDateTime == ChildBirthDate + ChildBirthTime.TimeOfDay);
                Messenger.Default.Send(tempChild, "ChildToNewChildView");
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("ToNewChild"));
                //Messenger.Default.Send<NotificationMessage>(new NotificationMessage("SaveChildBasic"));

            });


            this.OpenMedicalServicesToAdd = new RelayCommand(parameter =>
            {
                Messenger.Default.Send<NotificationMessage>(new NotificationMessage("AddMedicalServices"));
            });

            this.RemoveMedicalService = new RelayCommand(parameter =>
            {
                MedicalServicesList.Remove(SelectedMedicalServiceInfo);
            });

            this.LogOutCommand = new RelayCommand(parameter =>
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.BackCommand = new RelayCommand(Parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ToPatient"));
                Messenger.Default.Send(PatientCurrent, "Patient");
                Messenger.Default.Send(EmployeeCurrent, "Employee");
            });
            this.NewChildDialogCommand = new RelayCommand(parameter =>
            {  
                ChildBirthDate = DateTime.Now;
                Messenger.Default.Send(new NotificationMessage("NewChildDialog"));
                new P3_Midwife.Views.NewChildWindow();
                   
            });
            this.SaveAndCompleteCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("RecordSave"));
                this.RecordCurrent.Note += RecordCurrent.NewNote;
                this.RecordCurrent.BirthInformationList.AddRange(BirthInformationListProperty);
                BirthInformationListProperty.Clear();
                this.RecordCurrent.ContractionIVDripList.AddRange(ContractionListProperty);
                ContractionListProperty.Clear();
                this.RecordCurrent.MicturitionList.AddRange(MicturitionListProperty);
                MicturitionListProperty.Clear();
                this.RecordCurrent.VaginalExplorationList.AddRange(VaginalExplorationListProperty);
                VaginalExplorationListProperty.Clear();
                this.RecordCurrent.FetusObservationList.AddRange(FetusObservationListProperty);
                FetusObservationListProperty.Clear();
                this.RecordCurrent.CurrentBill.BillItemList.AddRange(MedicalServicesList);
                MedicalServicesList.Clear();
                InitiateCollections();
                RecordCurrent.IsActive = false;
                Messenger.Default.Send(new NotificationMessage("ToPatient"));
                Messenger.Default.Send(PatientCurrent, "Patient");
                Messenger.Default.Send(EmployeeCurrent, "Employee");
            });
            this.AddBirthInfo= new RelayCommand(parameter =>
            {
                BirthInfo = new BirthInformation();
                BirthInformationListProperty.Add(BirthInfo);

            });
            this.AddContractionIVDripInfo = new RelayCommand(parameter =>
            {
                ContractionIVDripInfo = new ContractionIVDrip();
                ContractionListProperty.Add(ContractionIVDripInfo);

            });
            this.AddFetusObservationInfo = new RelayCommand(parameter =>
            {
                FetusObservationInfo = new FetusObservation();
                FetusObservationListProperty.Add(FetusObservationInfo);
            });
            this.AddMicturitionInfo = new RelayCommand(parameter =>
            {
                MicturitionInfo = new Micturition();
                MicturitionListProperty.Add(MicturitionInfo);
            });
            this.AddVaginalExplorationInfo = new RelayCommand(parameter =>
            {
                VaginalExplorationInfo = new VaginalExploration();
                VaginalExplorationListProperty.Add(VaginalExplorationInfo);
            });

            InitiateCollections();

        }

        private void InitiateCollections()
        {
            if (BirthInformationListProperty.Count == 0)
            {
                BirthInformationListProperty.Add(new BirthInformation());
            }
            if (ContractionListProperty.Count == 0)
            {
                ContractionListProperty.Add(new ContractionIVDrip());
            }
            if (MicturitionListProperty.Count == 0)
            {
                MicturitionListProperty.Add(new Micturition());
            }
            if (VaginalExplorationListProperty.Count == 0)
            {
                VaginalExplorationListProperty.Add(new VaginalExploration());
            }
            if (FetusObservationListProperty.Count == 0)
            {
                FetusObservationListProperty.Add(new FetusObservation());
            }
        }
    }
}
