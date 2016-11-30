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
    public class BillTests
    {
        Midwife testwife = new Midwife(1, "midwifeName", "midwifePass", 12345678, "email");
        Patient testpatient = new P3_Midwife.Patient("1234567890", "testPerson");
        

        [TestMethod()]
        public void BillTestContructorRecord()
        {
            Bill BillTest = new Bill(new Record(testpatient));
            testwife.RegisterTreatmentToBill(testpatient, new MedicalService(10, "testService", "TS"));
            Assert.IsNotNull(BillTest.BillItemList);
        }

        [TestMethod()]
        public void BillPriceTest()
        {
            Bill BillTest = new Bill(new Record(testpatient));
            Assert.IsNotNull(BillTest.TotalPrice);
        }

        [TestMethod()]
        public void AddToBillItemListTest()
        {
            testpatient.RecordList.Add(new Record(testpatient));
            testpatient.RecordList[0].CurrentBill = new Bill(testpatient.RecordList[0]);
            MedicalService testservice = new MedicalService(30, "TestName", "TM");
            testwife.RegisterTreatmentToBill(testpatient, testservice);

            Assert.IsNotNull(testpatient.RecordList[0].CurrentBill.BillItemList);
        }

        [TestMethod()]
        public void CalculateTotalPriceTest()
        {
            testpatient.RecordList.Add(new Record(testpatient));
            testpatient.RecordList[0].CurrentBill = new Bill(testpatient.RecordList[0]);
            MedicalService testservice = new MedicalService(30, "TestName", "TM");
            testwife.RegisterTreatmentToBill(testpatient, testservice);

            Assert.IsTrue(testpatient.RecordList[0].CurrentBill.TotalPrice == 30);
        }
    }
}