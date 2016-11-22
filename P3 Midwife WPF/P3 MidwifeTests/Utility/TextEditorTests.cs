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
    public class TextEditorTests
    {
        [TestMethod()]
        public void CheckWordTest()
        {
            Patient TestPatient = new Patient(1234567890, "TestName");
            string TestString = "TEST";
            List<MedicalService> TestList = new List<MedicalService>();

            TestList.Add(new MedicalService(20, TestString, "TS"));
            TestPatient.RecordList.Add(new Record(TestPatient));
            TestPatient.RecordList.First().CurrentBill.AddToBillItemList(new MedicalService(20, "testname", "tn"));
            TestPatient.RecordList.First().IsActive = true;

            TextEditor.CheckWord(TestString, TestList, TestPatient);

            Assert.IsNotNull(TestPatient.RecordList);
        }

        [TestMethod()]
        public void AutoFillTest()
        {
            //Metoden er ikke færdiglavet på tidspunktet af denne kommentar og kan derfor ikke unit testes endnu
        }
    }
}