using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class Patient
    {
        public int CPR { get; }
        public string Name { get;}

        public Patient(int PatientCPR)
        {
            this.CPR = PatientCPR;
        }

        public Patient(int PatientCPR, string PatientName)
        {
            this.CPR = PatientCPR;
            this.Name = PatientName;
        }

        public List<Record> RecordList { get; set; }


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
