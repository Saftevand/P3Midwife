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
    public class EmployeeTests
    {
        Employee TestEmployee = new Midwife(1, "TestName", "password", 12345678, "mail@mail.com");

        #region ConstructorTests
        [TestMethod()]
        public void EmployeeTestConstructorID()
        {
            Assert.IsNotNull(TestEmployee.ID);
        }

        [TestMethod()]
        public void EmployeeTestConstructorName()
        {
            Assert.IsNotNull(TestEmployee.Name);
        }

        [TestMethod()]
        public void EmployeeTestConstructorPassword()
        {
            Assert.IsNotNull(TestEmployee.Password);
        }

        [TestMethod()]
        public void EmployeeTestConstructorTelephoneNumber()
        {
            Assert.IsNotNull(TestEmployee.TelephoneNumber);
        }

        [TestMethod()]
        public void EmployeeTestConstructorEmail()
        {
            Assert.IsNotNull(TestEmployee.Email);
        }

        [TestMethod()]
        public void EmployeeTestFindPatient()
        {
            Ward.Patients.Add(new Patient("1111111111", "testname"));
            Patient foundpatient = TestEmployee.FindPatient("1111111111");

            Assert.AreEqual("testname", foundpatient.Name);
        }

        #endregion
    }
}