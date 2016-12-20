using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public static class Ward
    {
        //Static lists that works kinda as a database when the system is running
        public static List<string> UsedCPRs = new List<string>();
        public static List<Employee> Employees = new List<Employee>();
        public static List<Patient> Patients = new List<Patient>();
        public static List<DeliveryRoom> DeliveryRooms = new List<DeliveryRoom>();
        public static List<MedicalService> MedicalServicesList = new List<MedicalService>();

        static Ward()
        {
        }
    }
}
