﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using P3_Midwife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife.Tests
{
    [TestClass()]
    public class MidwifeTests
    {
        static int id = 101;
        static string name = "testname";
        static string password = "password";
        static int telephonenumber = 123345678;
        static string email = "mail@mail.com";

        Midwife TestMidwife = new Midwife(id, name, password, telephonenumber, email);

        [TestMethod()]
        public void MidwifeConstructorTestID()
        {
            Assert.AreEqual(id, TestMidwife.ID);
        }

        [TestMethod()]
        public void MidwifeConstructorTestName()
        {
            Assert.AreEqual(name, TestMidwife.Name);
        }

        [TestMethod()]
        public void MidwifeConstructorTestPassword()
        {
            Assert.AreEqual(password, TestMidwife.Password);
        }

        [TestMethod()]
        public void MidwifeConstructorTestTelephonenumber()
        {
            Assert.AreEqual(telephonenumber, TestMidwife.TelephoneNumber);
        }

        [TestMethod()]
        public void MidwifeConstructorTestEmail()
        {
            Assert.AreEqual(email, TestMidwife.Email);
        }
    }
}