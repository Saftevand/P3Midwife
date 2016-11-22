using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class Dictionary
    {
        private List<string> _listOfWords = new List<string>();

        public Dictionary(List<MedicalService> BillItemList)
        {
            CreateDictionary(BillItemList);
        }


        public void CreateDictionary(List<MedicalService> BillItemList)
        {
            foreach (MedicalService BI in BillItemList)
            {
                _listOfWords.Add(BI.Name);
            }
        }

        public List<string> ListOfWords { get { return _listOfWords; } set { _listOfWords = value; } }
    }
}
