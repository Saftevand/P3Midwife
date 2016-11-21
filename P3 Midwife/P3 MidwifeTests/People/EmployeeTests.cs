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
        Employee TestEmployee = new Employee(1, "TestName", "password", 12345678, "mail@mail.com");

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
        public void ToStringTestConstructorPassword()
        {
            Assert.IsNotNull(TestEmployee.Password);
        }

        [TestMethod()]
        public void ToStringTestConstructorTelephoneNumber()
        {
            Assert.IsNotNull(TestEmployee.TelephoneNumber);
        }

        [TestMethod()]
        public void ToStringTestConstructorEmail()
        {
            Assert.IsNotNull(TestEmployee.Email);
        }
        #endregion
    }
}