using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;

namespace P3_Midwife
{
    public class Bill
    {

        #region Instans Variabler
        //private Record _record;
        private List<MedicalService> _billItemList = new List<MedicalService>();
        private decimal _totalPrice;
        #endregion
        
        public Bill(/*Record record*/)
        {
            //this._record = record;
        }

        #region Properties
        public List<MedicalService> BillItemList { get { return _billItemList; } set {; } }
        public decimal TotalPrice { get { return CalculateTotalPrice(); } }
        #endregion

        #region Methods
        public override string ToString()
        {
            string AllItems = "";
            foreach (MedicalService BI in BillItemList)
            {
                AllItems += BI.ToString() + "\n";
            }
            return "Total amount : " + _totalPrice + ". Items : " + AllItems;
        }

        private decimal CalculateTotalPrice()
        {
            decimal result = 0;
            foreach(MedicalService service in _billItemList)
            {
                result += service.Price;
            }
            _totalPrice = result;
            return result;
        }

        public void AddToBillItemList(MedicalService BI)
        {
            _billItemList.Add(BI);
        }

        public void RemoveFromBillItemList(MedicalService BI)
        {
            _billItemList.Remove(BI);
        }
        #endregion
    }
}
