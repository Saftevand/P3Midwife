using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
    class BillItem
    {
        public string Name { get; }
        public decimal Price { get; }

        public BillItem(decimal price, string name)
        {
            this.Price = price;
            this.Name = name;
        }


        public override string ToString()
        {
            return Name + " - Price : " + Price;
        }

    }
}
