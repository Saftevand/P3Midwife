using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace P3_Midwife
{
    public class Midwife : Employee
    {

        public Midwife(int id, string name, string password, int telephonenumber, string email, int clearance) 
            : base(id, name, password, telephonenumber, email, clearance)
        { }

        //Removes a patient from ward
        private void TransferPatient(Patient _patient)
        {
            Filemanagement.RemovePatientFromRoomFile(_patient);
            Filemanagement.RemovePatientFromFile(_patient);

            if(_patient.Children.Count > 0)
            {
                foreach (Patient item in _patient.Children)
                {
                    if (Ward.Patients.Contains(item))
                    {
                        Filemanagement.RemovePatientFromFile(item);
                        Filemanagement.RemovePatientFromRoomFile(item);
                    }
                }
            }
        }

        //Puts a patient in a vacant room
        public void AssignPatientToDRoom(Patient _patient)
        {
            DeliveryRoom currentRoom = Ward.DeliveryRooms.Find(x => x.Occupied == false);
            if (currentRoom != null)
            {
                currentRoom.PatientsInRoom.Add(_patient);
                _patient.InRoom = currentRoom;
            }

            else
                throw new Exception("There are no empty rooms to assign patients to");
        }

        //TODO: Do we need method RequestTestSample? a bit like RegisterTreatmentToBill 
        //Adds a MedicalService to the a patient's record's bill.
        public void RegisterTreatmentToBill(Patient _patient, MedicalService _medicalService)
        {
            Record currentRecord = _patient.RecordList.Find(x => x.IsActive == true);
            if (currentRecord != null)
            {
                currentRecord.CurrentBill.BillItemList.Add(_medicalService);
            }
            else
            {
                throw new Exception("No active record for patient");
            }
        }

        //Removes a specific medicalservice to a patients bill
        public void RemoveTreatmentFromBill(Patient _patient, MedicalService _medicalService)
        {
            if (_patient.RecordList.Last().IsActive == true)
            {
                _patient.RecordList.Last().CurrentBill.BillItemList.Remove(_medicalService);
            }
            else
            {
                throw new Exception("No active record for patient");
            }
        }
        
        //Admits patient to the ward. Also places patient in a room using the AssignPatientToDRoom method
        public void AdmitPatient(string _cpr, string _name)
        {
            Patient patientToAdd = new Patient(_cpr, _name);
            Ward.Patients.Add(patientToAdd);
            Filemanagement.AddPatientOrEmployeeToFile(patientToAdd);
            AssignPatientToDRoom(patientToAdd);
        }

        //Method to create a patient when a baby is born.
        public void CreatePatient(Patient _mother, char _gender)
        {
            Patient child = new Patient(_gender);
            child.Mother = _mother;
            _mother.Children.Add(child);
        }

        public void CreatePatient(Patient _mother, char _gender, string _date)
        {
            Patient child = new Patient(_gender, _date);
            child.Mother = _mother;
            _mother.Children.Add(child);
        } 
    }
}
