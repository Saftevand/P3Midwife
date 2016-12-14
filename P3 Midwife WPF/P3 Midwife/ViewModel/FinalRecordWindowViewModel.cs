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
    public class FinalRecordWindowViewModel : DependencyObject
    {
        private Employee _currentEmployee;
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand BackCommand { get; }

        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(PatientCurrentf), typeof(Patient), typeof(FinalRecordWindowViewModel));
        public static DependencyProperty RecordProperty = DependencyProperty.Register(nameof(RecordCurrentf), typeof(Record), typeof(FinalRecordWindowViewModel));
        public static DependencyProperty NoteProperty = DependencyProperty.Register(nameof(NoteCurrent), typeof(string), typeof(FinalRecordWindowViewModel));

        private ObservableCollection<BirthInformation> _birthInformationList = new ObservableCollection<BirthInformation>();
        private ObservableCollection<ContractionIVDrip> _contractrionIVDripList = new ObservableCollection<ContractionIVDrip>();
        private ObservableCollection<FetusObservation> _fetusObservationList = new ObservableCollection<FetusObservation>();
        private ObservableCollection<Micturition> _micturitionList = new ObservableCollection<Micturition>();
        private ObservableCollection<VaginalExploration> _vaginalExplorationList = new ObservableCollection<VaginalExploration>();
        private ObservableCollection<MedicalService> _medicalServicesList = new ObservableCollection<MedicalService>();
        private ObservableCollection<MedicalService> _availableMedicalServices = new ObservableCollection<MedicalService>();

        public string NoteCurrent
        {
            get { return (string)this.GetValue(NoteProperty); }
            set { this.SetValue(NoteProperty, value); }
        }

        public Record RecordCurrentf
        {
            get { return (Record)this.GetValue(RecordProperty); }
            set
            {
                this.SetValue(RecordProperty, value);

                /*BirthInformationListProperty.Clear();
                ContractionListProperty.Clear();
                FetusObservationListProperty.Clear();
                MicturitionListProperty.Clear();
                VaginalExplorationListProperty.Clear();
                MedicalServicesList.Clear();
                */
                BirthInformationListProperty.AddRange(RecordCurrentf.BirthInformationList);
                ContractionListProperty.AddRange(RecordCurrentf.ContractionIVDripList);
                FetusObservationListProperty.AddRange(RecordCurrentf.FetusObservationList);
                MicturitionListProperty.AddRange(RecordCurrentf.MicturitionList);
                VaginalExplorationListProperty.AddRange(RecordCurrentf.VaginalExplorationList);
                MedicalServicesList.AddRange(RecordCurrentf.CurrentBill.BillItemList);
            }
        }
        public Patient PatientCurrentf
        {
            get { return (Patient)this.GetValue(PatientProperty); }
            set { this.SetValue(PatientProperty, value); }
        }

        public Employee EmployeeCurrentf
        {
            get { return _currentEmployee; }
            set { _currentEmployee = value; }
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

        public FinalRecordWindowViewModel()
        {
            Messenger.Default.Register<Record>(this, "NewRecordToRecordView", (ActiveRecord) => { RecordCurrentf = ActiveRecord; NoteCurrent = ActiveRecord.Note; });
            Messenger.Default.Register<Patient>(this, "PatientToRecordView", (ActivePatient) => { PatientCurrentf = ActivePatient; });
            Messenger.Default.Register<Employee>(this, "EmployeetoRecordView", (ActiveEmployee) => { EmployeeCurrentf = ActiveEmployee; });

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
                Messenger.Default.Send(PatientCurrentf, "Patient");
                Messenger.Default.Send(EmployeeCurrentf, "Employee");
            });
        }
    }
}
