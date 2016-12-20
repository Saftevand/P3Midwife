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
        private List<MedicalService> _billItemList = new List<MedicalService>();
        private decimal _totalPrice;
        #endregion


        #region Properties
        public List<MedicalService> BillItemList { get { return _billItemList; } set {; } }
        public decimal TotalPrice { get { return CalculateTotalPrice(); } }
        public bool Active { get; set; }
        public Record BillsRecord { get; set; }
        public int RecordID { get; set; }
        #endregion

        public Bill(Record record)
        {
            this.RecordID = record._thisRecordID;
            this.Active = true;
            this.BillsRecord = record;
        }

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

        //Caluculates the total sum of the cost of all the medicalservices a patient has recieved
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
        #endregion
    }
}
