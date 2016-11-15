using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class Dictionary
    {
        public Dictionary(List<BillItem> BillItemList)
        {
            CreateDictionary(BillItemList);
        }


        public void CreateDictionary(List<BillItem> BillItemList)
        {
            foreach (BillItem BI in BillItemList)
            {
                ListOfWords.Add(BI.Name);
            }
        }



        public List<string> ListOfWords { get; set; }
    }
}
