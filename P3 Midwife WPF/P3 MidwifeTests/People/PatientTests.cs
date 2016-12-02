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

        [TestMethod()]
        public void PatientTestConstructorGender()
        {
            Assert.AreEqual(TestPatient.Gender, "P");
        }
        #endregion

        #region MethodTests

        [TestMethod()]
        public void PatientTestCalcAge()
        {
            Patient AgeTester = new Patient("0109940000", "name");
            Assert.AreEqual(AgeTester.Age, 22);
        }
        #endregion
    }
}