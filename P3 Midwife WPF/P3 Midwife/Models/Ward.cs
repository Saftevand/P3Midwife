using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    //TODO: skal måske laves om til ikke at være static??
    public static class Ward
    {
        public static List<string> UsedCPRs = new List<string>();
        public static List<Employee> Employees = new List<Employee>();
        public static List<Patient> Patients = new List<Patient>();
        public static List<DeliveryRoom> DeliveryRooms = new List<DeliveryRoom>();
        public static List<MedicalService> MedicalServicesList = new List<MedicalService>();

        static Ward()
        {
            Filemanagement.InitialiseFoldersAndFiles();
            Filemanagement.CreatePatientFolderAndFile(new Patient("1805961577", "Patrick Alminde"));
            Filemanagement.ReadPatients();
            Filemanagement.ReadBirthRecords(Patients[0]);
        }

        //TODO: HVOR FANDEN SKAL LOGIN/LOGUD/AUTOLOGUD LIGGE?!?!

        //public static void AdmitPatient(string _cpr, string _name)
        //{
        //    Patient patientToAdd = new Patient(_cpr, _name);
        //    Patients.Add(patientToAdd);
        //    Filemanagement.AddPatientOrEmployeeToFile(patientToAdd);
        //    RoomAdmit(patientToAdd);
        //}

        //public static void RoomAdmit(Patient _patientToBeAdd)
        //{
        //    DeliveryRoom foundRoom = DeliveryRooms.Find(x => x.Occupied == false);
        //    foundRoom.Occupied = true;
        //    foundRoom.PatientsInRoom.Add(_patientToBeAdd);
        //    _patientToBeAdd.InRoom = foundRoom;
        //}
    }
}
