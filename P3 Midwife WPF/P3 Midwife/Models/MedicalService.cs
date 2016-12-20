using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
   public class MedicalService
   {
        #region Properties
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string AbbrevationName { get; set; }
        #endregion

        public MedicalService(decimal price, string name, string abbrevationName)
        {
            this.Price = price;
            this.Name = name;
            this.AbbrevationName = abbrevationName;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
