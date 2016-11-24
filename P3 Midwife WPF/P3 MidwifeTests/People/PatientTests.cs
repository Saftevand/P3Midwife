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
        DeliveryRoom TestDeliveryRoom = new DeliveryRoom(1, true);
        Patient TestPatient = new Patient("1234567890", "TestName");

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
            Patient TestPatientAdmit = new Patient("1234567890", "TestName");
            TestPatientAdmit.AdmitPatientToRoom(TestDeliveryRoom);

            Assert.AreEqual(TestDeliveryRoom.PatientsInRoom.First(), TestPatientAdmit); 
        }

        [TestMethod()]
        public void DischargePatientFromRoomTest()
        {
            Patient TestPatientAdmit = new Patient("1234567890", "TestName");

            TestPatientAdmit.AdmitPatientToRoom(TestDeliveryRoom);
            TestPatientAdmit.DischargePatientFromRoom(TestDeliveryRoom);

            Assert.IsFalse(TestDeliveryRoom.PatientsInRoom.Contains(TestPatientAdmit));
        }
        #endregion
    }
}