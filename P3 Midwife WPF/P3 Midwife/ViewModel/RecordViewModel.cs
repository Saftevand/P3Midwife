﻿using System;
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
        public RelayCommand MedicinCommand { get; }
        public RelayCommand SaveAndComplete { get; }
        public static DependencyProperty PatientProperty = DependencyProperty.Register(nameof(PatientCurrent), typeof(Patient), typeof(RecordViewModel));
        public static DependencyProperty RecordProperty = DependencyProperty.Register(nameof(RecordCurrent), typeof(Record), typeof(RecordViewModel));
        public static DependencyProperty BirthInfoProperty = DependencyProperty.Register(nameof(BirthInfo), typeof(Record._birthInformation), typeof(RecordViewModel));
        public static DependencyProperty ContractionIVDripProperty = DependencyProperty.Register(nameof(ContractionIVDripInfo), typeof(Record._contractionIVDrip), typeof(RecordViewModel));
        public static DependencyProperty FetusObservationProperty = DependencyProperty.Register(nameof(FetusObservationInfo), typeof(Record._fetusObservation), typeof(RecordViewModel));
        public static DependencyProperty MicturitionProperty = DependencyProperty.Register(nameof(Microsoft), typeof(Record._micturition), typeof(RecordViewModel));
        public static DependencyProperty VaginalExplorationProperty = DependencyProperty.Register(nameof(VaginalExplorationInfo), typeof(Record._vaginalExploration), typeof(RecordViewModel));
        private ObservableCollection<Record._birthInformation> _birthInformationList = new ObservableCollection<Record._birthInformation>();
        private ObservableCollection<Record._contractionIVDrip> _contractrionIVDripList= new ObservableCollection<Record._contractionIVDrip>();
        private ObservableCollection<Record._fetusObservation> _fetusObservationList = new ObservableCollection<Record._fetusObservation>();
        private ObservableCollection<Record._micturition> _micturitionList = new ObservableCollection<Record._micturition>();
        private ObservableCollection<Record._vaginalExploration> _vaginalExplorationList = new ObservableCollection<Record._vaginalExploration>();

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


        public RecordViewModel()
        {
            Messenger.Default.Register<Record>(this, "NewRecordToRecordView", (ActiveRecord) => { RecordCurrent = ActiveRecord; });
            Messenger.Default.Register<Patient>(this, "PatientToRecordView", (ActivePatient) => { PatientCurrent = ActivePatient; });
            Messenger.Default.Register<Employee>(this, "EmployeetoRecordView", (ActiveEmployee) => { EmployeeCurrent = ActiveEmployee; });
            this.LogOutCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("FromRecordToLogIn"));
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.BackCommand = new RelayCommand(Parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("FromRecordToPatient"));
                Messenger.Default.Send(PatientCurrent, "Patient");
                Messenger.Default.Send(EmployeeCurrent, "Employee");
            });
            this.NewChildCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("FromRecordToCNewChild"));
                Messenger.Default.Send(PatientCurrent, "PatientToNewChildView");
                Messenger.Default.Send(EmployeeCurrent, "EmployeetoNewChildView");
            });
            this.SaveAndComplete = new RelayCommand(parameter =>
            {
                RecordCurrent.BirthInformationList.Add(BirthInfo);
                RecordCurrent.ContractionIVDripList.Add(ContractionIVDripInfo);
                RecordCurrent.MicturitionList.Add(MicturitionInfo);
                RecordCurrent.FetusObservationList.Add(FetusObservationInfo);
                RecordCurrent.VaginalExplorationList.Add(VaginalExplorationInfo);
                PatientCurrent.RecordList.Add(RecordCurrent);
                Messenger.Default.Send(new NotificationMessage("FromRecordToPatient"));
                Messenger.Default.Send(PatientCurrent, "Patient");
                Messenger.Default.Send(EmployeeCurrent, "Employee");
            });
            this.MedicinCommand = new RelayCommand(parameter =>
            {
                //nyt vindue skal laves her
            });
        }
    }
}
