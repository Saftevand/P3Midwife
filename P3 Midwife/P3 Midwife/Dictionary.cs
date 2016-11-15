using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class Dictionary
    {
        public Dictionary(List<MedicalService> BillItemList)
        {
            CreateDictionary(BillItemList);
        }


        public void CreateDictionary(List<MedicalService> BillItemList)
        {
            foreach (MedicalService BI in BillItemList)
            {
                ListOfWords.Add(BI.Name);
            }
        }



        public List<string> ListOfWords { get; set; }
    }
}
