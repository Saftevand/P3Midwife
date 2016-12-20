using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class DeliveryRoom
    {
        private List<Patient> _patientsInRoomList = new List<Patient>();

        public DeliveryRoom(int _id, bool _occupied)
        {
            this.RoomID = _id;
            this.Occupied = _occupied;
        }
        
        public List<Patient> PatientsInRoom { get { return _patientsInRoomList; } set { _patientsInRoomList = value; }}
        public int RoomID { get; }
        public bool Occupied { get; set; }

        public override string ToString()
        {
            return (RoomID.ToString() + " " + Occupied.ToString());
        }

    }
}
