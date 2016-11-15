using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public static class TextEditor
    {
       
        public static  void CheckWord(string input, List<MedicalService> BIList, Patient patient)
        {
            string inputLowerCase = input.ToLower();
            string BINameLowerCase;
            foreach (MedicalService BI in BIList)
            {
                BINameLowerCase = BI.Name.ToLower();
                if (inputLowerCase == BINameLowerCase)
                {
                    patient.RecordList.Find(x => x.IsActive == true).CurrentBill.BillItemList.Add(BI);
                }
            }
        }


        public static List<string> AutoFill(string input, Dictionary dictionary)
        {
            List<string> result = new List<string>();           

            if (input.Length > 2)
            {
                //string inputLowerCase = input.ToLower();
                result.AddRange(dictionary.ListOfWords.FindAll(x => x.Contains(input)));
            }
            
            return result;
        }

    }
}
