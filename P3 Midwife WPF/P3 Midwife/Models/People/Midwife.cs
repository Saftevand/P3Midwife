﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using GalaSoft.MvvmLight.Messaging;

namespace P3_Midwife
{
    public class Midwife : Employee
    {

        public Midwife(int id, string name, string password, int telephonenumber, string email) 
            : base(id, name, password, telephonenumber, email)
        { }

        //Removes a patient from ward
        public void TransferPatient(Patient patient)
        {
<<<<<<< HEAD
            Filemanagement.RemovePatientFromRoomFile(_patient);
           // Filemanagement.RemovePatientFromFile(_patient);

            if(_patient.Children.Count > 0)
=======
            DeliveryRoom tempRoom =  Ward.DeliveryRooms.Find(x => x.PatientsInRoom.Contains(patient));
            if (tempRoom != null)
>>>>>>> c2a31a0d1796d72d7df69b0d6f296d482e484e34
            {
                if (patient.Children.Count > 0)
                {
                    foreach (Patient child in patient.Children)
                    {
<<<<<<< HEAD
                      //  Filemanagement.RemovePatientFromFile(item);
                        Filemanagement.RemovePatientFromRoomFile(item);
=======
                        if (tempRoom.PatientsInRoom.Contains(child))
                        {
                            tempRoom.PatientsInRoom.Remove(child);
                            CurrentPatients.Remove(child);
                        }
>>>>>>> c2a31a0d1796d72d7df69b0d6f296d482e484e34
                    }
                }
                tempRoom.PatientsInRoom.Remove(patient);
                CurrentPatients.Remove(patient);
                tempRoom.Occupied = false;
            }
            else throw new ArgumentException("Input patient not found in room");
            
        }

        //Puts a patient in a vacant room
        private void AssignPatientToDRoom(Patient _patient)
        {
            DeliveryRoom currentRoom = Ward.DeliveryRooms.Find(x => x.Occupied == false);
            if (currentRoom != null)
            {
                currentRoom.PatientsInRoom.Add(_patient);
<<<<<<< HEAD
=======
                currentRoom.Occupied = true;
>>>>>>> c2a31a0d1796d72d7df69b0d6f296d482e484e34
            }
            else throw new Exception("There are no empty rooms to assign patients to");
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
        public void AdmitPatient(Patient patient)
        {
            AssignPatientToDRoom(patient);
<<<<<<< HEAD
            this._currentPatients.Add(patient);
=======
            this.CurrentPatients.Add(patient);
>>>>>>> c2a31a0d1796d72d7df69b0d6f296d482e484e34
        }

        //Method to create a patient when a baby is born.
        public void CreatePatient(Patient _mother, char _gender)
        {
            Patient child = new Patient(_gender);
            child.Mother = _mother;
            _mother.Children.Add(child);
        }

        public void CreatePatient(Patient mother, char gender, string date)
        {
            Patient child = new Patient(gender, date, mother);
            Ward.Patients.Add(child);
            Ward.DeliveryRooms.Find(x => x.PatientsInRoom.Contains(mother)).PatientsInRoom.Add(child);
            child.RecordList.Add(new Record(child));
            Filemanagement.CreatePatientFolderAndFile(child);

        }
<<<<<<< HEAD

        public void CreatePatient(Patient mother, char gender, DateTime date)
        {
            Patient child = new Patient(gender, date, mother);
            Ward.Patients.Add(child);
            Ward.DeliveryRooms.Find(x => x.PatientsInRoom.Contains(mother)).PatientsInRoom.Add(child);
            child.RecordList.Add(new Record(child));
            Filemanagement.CreatePatientFolderAndFile(child);

        }


=======
>>>>>>> c2a31a0d1796d72d7df69b0d6f296d482e484e34
    }
}
