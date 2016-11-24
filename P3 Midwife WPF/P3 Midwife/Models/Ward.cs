using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    //TODO: skal liiige laves om til ikke at være static - nemt fordi den kan læse fra filer som opdateres løbende
    public static class Ward
    {
        public static List<string> UsedCPRs = new List<string>();
        public static List<Employee> Employees = new List<Employee>();
        public static List<Patient> Patients = new List<Patient>();
        public static List<DeliveryRoom> DeliveryRooms = new List<DeliveryRoom>();
        public static List<MedicalService> MedicalServicesList = new List<MedicalService>();

        static Ward()
        {
            Filemanagement.ReadEmployees(Environment.CurrentDirectory + "\\PersonInfo", "Employee_info.txt");
            Filemanagement.ReadPatients(Environment.CurrentDirectory + "\\PersonInfo", "Patient_info.txt");
            Filemanagement.ReadRooms();
        }

        public static void AdmitPatient(string _cpr, string _name)
        {
            Patient patientToAdd = new Patient(_cpr, _name);
            Patients.Add(patientToAdd);
            Filemanagement.AddPatientOrEmployeeToFile(patientToAdd);
            RoomAdmit(patientToAdd);
        }

        public static void RoomAdmit(Patient _patientToBeAdd)
        {
            DeliveryRoom foundRoom = DeliveryRooms.Find(x => x.Occupied == false);
            foundRoom.Occupied = true;
            foundRoom.PatientsInRoom.Add(_patientToBeAdd);
            _patientToBeAdd.InRoom = foundRoom;
        }


    }
}
