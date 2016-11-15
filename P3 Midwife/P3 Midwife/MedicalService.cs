using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
   public class MedicalService
   {
        public string Name { get; }
        public decimal Price { get; }
        public string AbbrevationName { get; }

        public MedicalService(decimal price, string name, string abbrevationName)
        {
            this.Price = price;
            this.Name = name;
            this.AbbrevationName = abbrevationName;
        }

        public override string ToString()
        {
            return Name + " - Price : " + Price;
        }

    }
}
