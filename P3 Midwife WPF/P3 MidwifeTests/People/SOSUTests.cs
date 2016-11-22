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
    public class SOSUTests
    {
        static int id = 101;
        static string name = "testname";
        static string password = "password";
        static int telephonenumber = 123345678;
        static string email = "mail@mail.com";

        SOSU TestSOSU = new SOSU(id, name, password, telephonenumber, email);

        [TestMethod()]
        public void SOSUConstructorTestID()
        {
            Assert.AreEqual(id, TestSOSU.ID);
        }

        [TestMethod()]
        public void SOSUConstructorTestName()
        {
            Assert.AreEqual(name, TestSOSU.Name);
        }

        [TestMethod()]
        public void SOSUConstructorTestPassword()
        {
            Assert.AreEqual(password, TestSOSU.Password);
        }

        [TestMethod()]
        public void SOSUConstructorTestTelephonenumber()
        {
            Assert.AreEqual(telephonenumber, TestSOSU.TelephoneNumber);
        }

        [TestMethod()]
        public void SOSUConstructorTestEmail()
        {
            Assert.AreEqual(email, TestSOSU.Email);
        }
    }
}