using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    class DeliveryRoom
    {
        public int RoomID { get; }
        public bool Occupied { get; set; }
        public List<Patient> PatientsInRoom { get; set; }

        public DeliveryRoom(int id)
        {
            this.RoomID = id;
            this.Occupied = false;
        }



    }
}
