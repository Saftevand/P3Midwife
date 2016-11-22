using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class DeliveryRoom
    {
        public int RoomID { get; }
        public bool Occupied { get; set; }
        private List<Patient> _patientsInRoomList = new List<Patient>();

        public DeliveryRoom(int id)
        {
            this.RoomID = id;
            this.Occupied = false;
        }
        
        public List<Patient> PatientsInRoom { get { return _patientsInRoomList; } set { _patientsInRoomList = value; }}

    }
}
