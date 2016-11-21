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
    public class PatientTests
    {
        DeliveryRoom TestDeliveryRoom = new DeliveryRoom(1);
        Patient TestPatient = new Patient(1234567890, "TestName");

        #region ConstructorTests
        [TestMethod()]
        public void PatientTestConstructorCPR()
        {
            Assert.IsNotNull(TestPatient.CPR);
        }

        [TestMethod()]
        public void PatientTestConstructorName()
        {
            Assert.IsNotNull(TestPatient.Name);
        }
        #endregion

        #region AddAndRemovePatientFromRoom
        [TestMethod()]
        public void AdmitPatientToRoomTest()
        {
            Patient TestPatient1 = new Patient(1234567890, "TestName");
            TestPatient1.AdmitPatientToRoom(TestDeliveryRoom);

            //Assert.IsTrue(TestDeliveryRoom.PatientsInRoom.Find(x => x.CPR == TestPatient.CPR) != null);

            Assert.AreEqual(TestDeliveryRoom.PatientsInRoom.First(), TestPatient1); 
        }

        [TestMethod()]
        public void DischargePatientFromRoomTest()
        {
            Patient TestPatient2 = new Patient(1234567890, "TestName");

            TestPatient2.AdmitPatientToRoom(TestDeliveryRoom);
            TestPatient2.DischargePatientFromRoom(TestDeliveryRoom);

            Assert.IsFalse(TestDeliveryRoom.PatientsInRoom.Contains(TestPatient2));
        }
        #endregion
    }
}