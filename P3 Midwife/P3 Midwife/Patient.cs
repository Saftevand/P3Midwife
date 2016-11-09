using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    class Patient
    {
        public int CPR { get; }
        public string Name { get;}

        public Patient(int PatientCPR)
        {
            this.CPR = PatientCPR;
        }

        public Patient(int PatientCPR, string PatientName)
        {
            this.CPR = PatientCPR;
            this.Name = PatientName;
        }

        public List<Record> RecordList { get; set; }
    }
}
