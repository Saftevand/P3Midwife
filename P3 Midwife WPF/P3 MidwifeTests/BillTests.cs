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
        Midwife testwife = new Midwife(1, "midwifeName", "midwifePass", 12345678, "email", 1);
        Patient testpatient = new P3_Midwife.Patient("1234567890", "testPerson");
        

        [TestMethod()]
        public void BillTestContructorRecord()
        {
            Bill BillTest = new Bill(new Record(testpatient));
            testwife.RegisterTreatmentToBill(testpatient, new MedicalService(10, "testService", "TS"));
            Assert.IsNotNull(BillTest.BillItemList);
        }

        [TestMethod()]
        public void CalculateTotalPriceTest()
        {
            Bill BillTest = new Bill(new Record(testpatient));
            Assert.IsNotNull(BillTest.TotalPrice);
        }

        [TestMethod()]
        public void CalculateTotalPriceTest2()
        {
            
            Record RecordTest = new Record(testpatient);
            Bill BillTest = new Bill(RecordTest);
            MedicalService testservice = new MedicalService(30, "TestName", "TM");
            testwife.RegisterTreatmentToBill(testpatient, testservice);

            System.Diagnostics.Debug.WriteLine("fightlikeaman!!   " + BillTest.TotalPrice);
            Assert.IsTrue(BillTest.TotalPrice == 30);
        }

        [TestMethod()]
        public void AddToBillItemListTest()
        {
            Bill BillTest = new Bill(new Record(testpatient));
            testwife.RegisterTreatmentToBill(testpatient, new MedicalService(20, "TestName", "TM"));
            Assert.IsNotNull(BillTest.BillItemList);
        }
    }
}