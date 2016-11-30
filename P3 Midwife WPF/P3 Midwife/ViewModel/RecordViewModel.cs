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
        public RelayCommand LogOutCommand { get; }
        public RelayCommand ExitCommand { get; }
        public RelayCommand BackCommand { get; }
        public static DependencyProperty RecordProperty = DependencyProperty.Register(nameof(RecordCurrent), typeof(Record), typeof(RecordViewModel));
        private ObservableCollection<Record._birthInformation> _birthInformationList = new ObservableCollection<Record._birthInformation>();
        
        public ObservableCollection<Record._birthInformation> BirthInformationListProperty
        {
            get { return _birthInformationList; }
            set { _birthInformationList = value; }
        } 

        public Record RecordCurrent
        {
            get { return (Record)this.GetValue(RecordProperty); }
            set { this.SetValue(RecordProperty, value); }
        }

        public RecordViewModel()
        {
            Messenger.Default.Register<Record>(this, "Record", (ActiveRecord) => { RecordCurrent = ActiveRecord; });
            this.LogOutCommand = new RelayCommand(parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ShowMainViewRecord"));
            });
            this.ExitCommand = new RelayCommand(parameter =>
            {
                Application.Current.Shutdown();
            });
            this.BackCommand = new RelayCommand(Parameter =>
            {
                Messenger.Default.Send(new NotificationMessage("ShowPatientView"));
            });
        }
    }
}
