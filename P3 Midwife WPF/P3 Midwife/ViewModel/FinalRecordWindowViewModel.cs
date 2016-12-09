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
    public class FinalRecordWindowViewModel : DependencyObject
    {
        private Employee _currentEmployee;
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand BackCommand { get; }

        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(PatientCurrent), typeof(Patient), typeof(RecordViewModel));
        public static DependencyProperty RecordProperty = DependencyProperty.Register(nameof(RecordCurrent), typeof(Record), typeof(RecordViewModel));
        private ObservableCollection<Record._birthInformation> _birthInformationList = new ObservableCollection<Record._birthInformation>();
        private ObservableCollection<Record._contractionIVDrip> _contractrionIVDripList = new ObservableCollection<Record._contractionIVDrip>();
        private ObservableCollection<Record._fetusObservation> _fetusObservationList = new ObservableCollection<Record._fetusObservation>();
        private ObservableCollection<Record._micturition> _micturitionList = new ObservableCollection<Record._micturition>();
        private ObservableCollection<Record._vaginalExploration> _vaginalExplorationList = new ObservableCollection<Record._vaginalExploration>();
        private ObservableCollection<MedicalService> _medicalServicesList = new ObservableCollection<MedicalService>();
        private ObservableCollection<MedicalService> _availableMedicalServices = new ObservableCollection<MedicalService>();

        public Record RecordCurrent
        {
            get { return (Record)this.GetValue(RecordProperty); }
            set { this.SetValue(RecordProperty, value); }
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
        public ObservableCollection<MedicalService> MedicalServices
        {
            get { return _availableMedicalServices; }
            set { _availableMedicalServices = value; }
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

        public FinalRecordWindowViewModel()
        {
            Messenger.Default.Register<Record>(this, "NewRecordToRecordView", (ActiveRecord) => { RecordCurrent = ActiveRecord; });
            Messenger.Default.Register<Patient>(this, "PatientToRecordView", (ActivePatient) => { PatientCurrent = ActivePatient; });
            Messenger.Default.Register<Employee>(this, "EmployeetoRecordView", (ActiveEmployee) => { EmployeeCurrent = ActiveEmployee; });

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
                Messenger.Default.Send(new NotificationMessage("ToRecord"));
                Messenger.Default.Send(PatientCurrent, "Patient");
                Messenger.Default.Send(EmployeeCurrent, "Employee");
            });
        }
    }
}
