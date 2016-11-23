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
    public class MedicalServiceTests
    {
        static decimal PriceTest = 20;
        static string NameTest = "TestName";
        static string AbbreviationNameTest = "TM";
        MedicalService MedicalServiceTest = new MedicalService(PriceTest, NameTest, AbbreviationNameTest);
        [TestMethod()]
        public void MedicalServiceConstructorPriceTest()
        {
            Assert.IsTrue(MedicalServiceTest.Price == PriceTest);
        }

        [TestMethod()]
        public void MedicalServiceConstructorNameTest()
        {
            Assert.IsTrue(MedicalServiceTest.Name == NameTest);
        }

        [TestMethod()]
        public void MedicalServiceConstructorAbbreviationNameTest()
        {
            Assert.IsTrue(MedicalServiceTest.AbbrevationName == AbbreviationNameTest);
        }
    }
}