using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_Midwife
{
<<<<<<< HEAD:P3 Midwife/P3 Midwife/MedicalService.cs
    class MedicalService
=======
    public class BillItem
>>>>>>> ddd3dcc3e52b75b8db55877a57d119905608dbdb:P3 Midwife/P3 Midwife/BillItem.cs
    {
        public string Name { get; }
        public decimal Price { get; }
        public string AbbrevationName { get; }

<<<<<<< HEAD:P3 Midwife/P3 Midwife/MedicalService.cs
        public MedicalService(decimal price, string name)
=======
        public BillItem(decimal price, string name, string abbrevationName)
>>>>>>> ddd3dcc3e52b75b8db55877a57d119905608dbdb:P3 Midwife/P3 Midwife/BillItem.cs
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
