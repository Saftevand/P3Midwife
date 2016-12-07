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

        //constructor to initialize the static lists.
        static Ward()
        {
            Filemanagement.InitialiseFoldersAndFiles();
            Filemanagement.CreatePatientFolderAndFile(new Patient("1234567890", "Patrick Almind"));
            Filemanagement.CreatePatientFolderAndFile(new Patient("2345678901", "Patrick Almin"));
            Filemanagement.CreatePatientFolderAndFile(new Patient("3456789012", "Patrick Almi"));
            Filemanagement.CreatePatientFolderAndFile(new Patient("4567890123", "Patrick Alm"));
            Filemanagement.CreatePatientFolderAndFile(new Patient("5678901234", "Patrick Al"));
            Filemanagement.ReadPatients();
            //Filemanagement.ReadBirthRecords(Patients[0]);
            //Filemanagement.SaveRecord(Patients[0].RecordList[1]);

        }
        //TODO: HVOR FANDEN SKAL LOGIN/LOGUD/AUTOLOGUD LIGGE?!?!
    }
}
