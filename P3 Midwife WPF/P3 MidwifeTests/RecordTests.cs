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
    public class RecordTests
    {
        [TestMethod()]
        public void RecordConstruktorPatientTest()
        {
            Patient TestPatient = new Patient("1234567890", "TestName");
            Record TestRecord = new Record(TestPatient);

            Assert.AreEqual(TestPatient, TestRecord.RecordsPatient);
        }
    }
}