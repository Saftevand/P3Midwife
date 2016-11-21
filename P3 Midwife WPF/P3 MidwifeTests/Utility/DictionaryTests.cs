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
    public class DictionaryTests
    {
        //[TestMethod()]
        //public void DictionaryTest()
        //{

        //}
                
        [TestMethod()]
        public void CreateDictionaryTest()
        {
            List<MedicalService> MedicalListTest = new List<MedicalService>();
            MedicalListTest.Add(new MedicalService(20, "TestName", "TN"));
            Dictionary DictionayTest = new Dictionary(MedicalListTest);

            Assert.IsNotNull(DictionayTest.ListOfWords);
        }
    }
}