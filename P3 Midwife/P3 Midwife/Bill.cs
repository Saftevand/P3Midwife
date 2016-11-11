using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    public class Bill
    {
        private decimal CalculateTotalPrice()
        {
            decimal result = 0;


            return result;
        }

        public List<BillItem> BillItemList { get; set; }
        public decimal Price { get { return CalculateTotalPrice(); }}

        public override string ToString()
        {
            string AllItems = "";
            foreach (BillItem BI in BillItemList)
            {
                AllItems += BI.ToString() + "\n";
            }
            return "Total amount : " + Price + ". Items : " + AllItems;
        }

    }
}
