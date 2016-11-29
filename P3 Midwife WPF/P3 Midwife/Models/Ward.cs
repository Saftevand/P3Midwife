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
            Filemanagement.ReadEmployees(Environment.CurrentDirectory + "\\PersonInfo", "Employee_info.txt");
            Filemanagement.ReadPatients(Environment.CurrentDirectory + "\\PersonInfo", "Patient_info.txt");
            Filemanagement.ReadRooms();
            Filemanagement.ReadMedicalServiceFromFile();
        }
        //TODO: HVOR FANDEN SKAL LOGIN/LOGUD/AUTOLOGUD LIGGE?!?!
    }
}
