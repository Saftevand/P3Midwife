using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3_Midwife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife.Tests
{
    [TestClass()]
    public class DeliveryRoomTests
    {
        DeliveryRoom DeliveryRoomTest = new DeliveryRoom(1);
        [TestMethod()]
        public void DeliveryRoomConstructorTest()
        {
            Assert.IsTrue(DeliveryRoomTest.RoomID == 1);
        }

    }
}