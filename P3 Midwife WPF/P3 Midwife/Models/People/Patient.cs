using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P3_Midwife
{
    public class Patient
    {
        public string CPR { get; set; }
        public string Name { get; set; }
        private List<Record> _recordList = new List<Record>();

        public Patient(string PatientCPR)
        {
            this.CPR = PatientCPR;
        }

        public Patient(string PatientCPR, string PatientName)
        {
            this.CPR = PatientCPR;
            this.Name = PatientName;
        }
        public Patient()
        {

        }

        public List<Record> RecordList { get { return _recordList; } set { _recordList = value; } }



        public void AdmitPatientToRoom(DeliveryRoom room)
        {            
            if (!room.PatientsInRoom.Contains(this))
            {
                room.PatientsInRoom.Add(this);
            }
            else throw new ArgumentException(Name + " with CPR:" + CPR.ToString() + " is already in room:" + room.RoomID.ToString());
        }

        public void DischargePatientFromRoom(DeliveryRoom room)
        {
            if (room.PatientsInRoom.Contains(this))
            {
                room.PatientsInRoom.Remove(this);
            }
            else throw new ArgumentException(Name + " with CPR:" + CPR.ToString() + " is NOT in room:" + room.RoomID.ToString());
        }
    }
}
