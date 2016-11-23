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
        Patient TestPatient = new Patient(1234567890, "TestName");
        DeliveryRoom TestDeliveryRoom = new DeliveryRoom(1);

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
            TestPatient.AdmitPatientToRoom(TestDeliveryRoom);

            Assert.IsTrue(TestDeliveryRoom.PatientsInRoom.Contains(TestPatient));
        }

        [TestMethod()]
        public void DischargePatientFromRoomTest()
        {
            TestPatient.AdmitPatientToRoom(TestDeliveryRoom);
            TestPatient.DischargePatientFromRoom(TestDeliveryRoom);

            Assert.IsFalse(TestDeliveryRoom.PatientsInRoom.Contains(TestPatient));
        }
        #endregion
    }
}