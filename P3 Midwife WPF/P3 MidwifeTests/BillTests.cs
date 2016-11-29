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
        Bill BillTest = new Bill(1);

        [TestMethod()]
        public void BillTestContructorRecord()
        {
            Assert.IsNotNull(BillTest.BillItemList);
        }

        [TestMethod()]
        public void CalculateTotalPriceTest()
        {
            Assert.IsNotNull(BillTest.TotalPrice);
        }

        //[TestMethod()]
        //public void CalculateTotalPriceTest2()
        //{
        //    BillTest.AddToBillItemList(new MedicalService(20, "TestName", "TM"));
        //    Assert.IsTrue(BillTest.TotalPrice == 20);
        //}

        //[TestMethod()]
        //public void AddToBillItemListTest()
        //{
        //    BillTest.AddToBillItemList(new MedicalService(20, "TestName", "TM"));
        //    Assert.IsNotNull(BillTest.BillItemList);
        //}
    }
}