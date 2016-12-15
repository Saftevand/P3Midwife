using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace P3_Midwife.Views
{
    class StringCTGClassificationConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value.ToString();
            

            //input = input.ToLower();
            //  char input = (char)value;
            if (input == "Normal")
            {
                return "Normal";
            }
            else if (input == "Afvigende")
            {
                return "Afvigende";
            }
            else if (input == "Patologisk")
            {
                return "Patologisk";
            }
            else
                return "";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new ArgumentNullException();
        }

    }
}
