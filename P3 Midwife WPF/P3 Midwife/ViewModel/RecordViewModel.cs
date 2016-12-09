using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace P3_Midwife.ViewModel
{
    public class RecordViewModel : DependencyObject
    {        
        private Employee _currentEmployee;
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand BackCommand { get; }
        public RelayCommand NewChildCommand { get; }
        public RelayCommand SaveAndCompleteCommand { get; }
        public RelayCommand AddBirthInfo { get; }
        public RelayCommand AddContractionIVDripInfo { get; }
        public RelayCommand AddFetusObservationInfo { get; }
        public RelayCommand AddMicturitionInfo { get; }
        public RelayCommand AddVaginalExplorationInfo { get; }
        public RelayCommand RemoveMedicalService { get; }
        public RelayCommand AddMedicalService { get; }
        public RelayCommand OpenMedicalServicesToAdd { get; }

        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(PatientCurrent), typeof(Patient), typeof(RecordViewModel));
        public static DependencyProperty RecordProperty = DependencyProperty.Register(nameof(RecordCurrent), typeof(Record), typeof(RecordViewModel));
        public static DependencyProperty BirthInfoProperty = DependencyProperty.Register(nameof(BirthInfo), typeof(Record._birthInformation), typeof(RecordViewModel));
        public static DependencyProperty ContractionIVDripProperty = DependencyProperty.Register(nameof(ContractionIVDripInfo), typeof(Record._contractionIVDrip), typeof(RecordViewModel));
        public static DependencyProperty FetusObservationProperty = DependencyProperty.Register(nameof(FetusObservationInfo), typeof(Record._fetusObservation), typeof(RecordViewModel));
        public static DependencyProperty MicturitionProperty = DependencyProperty.Register(nameof(MicturitionInfo), typeof(Record._micturition), typeof(RecordViewModel));
        public static DependencyProperty VaginalExplorationProperty = DependencyProperty.Register(nameof(VaginalExplorationInfo), typeof(Record._vaginalExploration), typeof(RecordViewModel));
        public static DependencyProperty SelectedMedicalServiceProperty = DependencyProperty.Register(nameof(SelectedMedicalServiceInfo), typeof(MedicalService), typeof(RecordViewModel));
        public static DependencyProperty SelectedAvailableMedicalServiceProperty = DependencyProperty.Register(nameof(SelectedAvailableMedicalServiceInfo), typeof(MedicalService), typeof(RecordViewModel));
        private ObservableCollection<Record._birthInformation> _birthInformationList = new ObservableCollection<Record._birthInformation>();
        private ObservableCollection<Record._contractionIVDrip> _contractrionIVDripList= new ObservableCollection<Record._contractionIVDrip>();
        private ObservableCollection<Record._fetusObservation> _fetusObservationList = new ObservableCollection<Record._fetusObservation>();
        private ObservableCollection<Record._micturition> _micturitionList = new ObservableCollection<Record._micturition>();
        private ObservableCollection<Record._vaginalExploration> _vaginalExplorationList = new ObservableCollection<Record._vaginalExploration>();
        private ObservableCollection<MedicalService> _medicalServicesList = new ObservableCollection<MedicalService>();
        private ObservableCollection<MedicalService> _availableMedicalServices = new ObservableCollection<MedicalService>();

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

        public ObservableCollection<Record._birthInformation> BirthInformationListProperty
        {
            get { return _birthInformationList; }
            set { _birthInformationList = value; }
        }

        public ObservableCollection<Record._contractionIVDrip> ContractionListProperty
        {
            get { return _contractrionIVDripList; }
            set { _contractrionIVDripList = value; }
        }

        public ObservableCollection<Record._fetusObservation> FetusObservationListProperty
        {
            get { return _fetusObservationList; }
            set { _fetusObservationList = value; }
        }

        public ObservableCollection<Record._micturition> MicturitionListProperty
        {
            get { return _micturitionList; }
            set { _micturitionList = value; }
        }

        public ObservableCollection<Record._vaginalExploration> VaginalExplorationListProperty
        {
            get { return _vaginalExplorationList; }
            set { _vaginalExplorationList = value; }
        }
        public Record._birthInformation BirthInfo
        {
            get { return (Record._birthInformation)this.GetValue(BirthInfoProperty); }
            set { this.SetValue(BirthInfoProperty, value); }
        }
        public Record._contractionIVDrip ContractionIVDripInfo
        {
            get { return (Record._contractionIVDrip)this.GetValue(ContractionIVDripProperty); }
            set { this.SetValue(ContractionIVDripProperty, value); }
        }
        public Record._fetusObservation FetusObservationInfo
        {
            get { return (Record._fetusObservation)this.GetValue(FetusObservationProperty); }
            set { this.SetValue(FetusObservationProperty, value); }
        }
        public Record._micturition MicturitionInfo
        {
            get { return (Record._micturition)this.GetValue(MicturitionProperty); }
            set { this.SetValue(MicturitionProperty, value); }
        }
        public Record._vaginalExploration VaginalExplorationInfo
        {
            get { return (Record._vaginalExploration)this.GetValue(VaginalExplorationProperty); }
            set { this.SetValue(VaginalExplorationProperty, value); }
        }
        public Record RecordCurrent
        {
            get { return (Record)this.GetValue(RecordProperty); }
            set
            {
                this.SetValue(RecordProperty, value);
                _medicalServicesList.AddRange(RecordCurrent.CurrentBill.BillItemList);
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
                _medicalServicesList.Add(SelectedAvailableMedicalServiceInfo);
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
            this.NewChildCommand = new RelayCommand(parameter =>
            {
                if (EmployeeCurrent is Midwife)
                {   
                    new P3_Midwife.Views.NewChildWindow();
                    Messenger.Default.Send(PatientCurrent, "PatientToNewChildView");
                    Messenger.Default.Send(EmployeeCurrent, "EmployeetoNewChildView");
                    Messenger.Default.Send(new NotificationMessage("NewChildDialog"));
                }

            });
            this.SaveAndCompleteCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("RecordSave"));
                RecordCurrent.BirthInformationList.AddRange(BirthInformationListProperty);
                RecordCurrent.ContractionIVDripList.AddRange(ContractionListProperty);
                RecordCurrent.MicturitionList.AddRange(MicturitionListProperty);
                RecordCurrent.VaginalExplorationList.AddRange(VaginalExplorationListProperty);
                RecordCurrent.FetusObservationList.AddRange(FetusObservationListProperty);
                RecordCurrent.IsActive = false;
                Messenger.Default.Send(new NotificationMessage("ToPatient"));
                Messenger.Default.Send(PatientCurrent, "Patient");
                Messenger.Default.Send(EmployeeCurrent, "Employee");
            });
            this.AddBirthInfo= new RelayCommand(parameter =>
            {
                Record._birthInformation tempBirthInfo = new Record._birthInformation();
                BirthInformationListProperty.Add(BirthInfo);
                BirthInfo = tempBirthInfo;

            });
            this.AddContractionIVDripInfo = new RelayCommand(parameter =>
            {
                Record._contractionIVDrip tempContractionIVDripInfo = new Record._contractionIVDrip();
                ContractionListProperty.Add(tempContractionIVDripInfo);
                ContractionIVDripInfo = tempContractionIVDripInfo;

            });
            this.AddFetusObservationInfo = new RelayCommand(parameter =>
            {
                Record._fetusObservation tempFetusObservationInfo = new Record._fetusObservation();
                FetusObservationListProperty.Add(tempFetusObservationInfo);
                FetusObservationInfo = tempFetusObservationInfo;

            });
            this.AddMicturitionInfo = new RelayCommand(parameter =>
            {
                Record._micturition tempMicturitionInfo = new Record._micturition();
                MicturitionListProperty.Add(tempMicturitionInfo);
                MicturitionInfo = tempMicturitionInfo;
            });
            this.AddVaginalExplorationInfo = new RelayCommand(parameter =>
            {
                Record._vaginalExploration tempVaginalExplorationInfo = new Record._vaginalExploration();
                VaginalExplorationListProperty.Add(tempVaginalExplorationInfo);
                VaginalExplorationInfo = tempVaginalExplorationInfo;
            });


            if (BirthInformationListProperty.Count == 0)
            {
                BirthInformationListProperty.Add(new Record._birthInformation());
            }
            if (ContractionListProperty.Count == 0)
            {
                ContractionListProperty.Add(new Record._contractionIVDrip());
            }
            if (MicturitionListProperty.Count == 0)
            {
                MicturitionListProperty.Add(new Record._micturition());
            }
            if (VaginalExplorationListProperty.Count == 0)
            {
                VaginalExplorationListProperty.Add(new Record._vaginalExploration());
            }
            if (FetusObservationListProperty.Count == 0)
            {
                FetusObservationListProperty.Add(new Record._fetusObservation());
            }
        }
    }
}
