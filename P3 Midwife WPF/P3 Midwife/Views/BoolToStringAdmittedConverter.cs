using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
namespace P3_Midwife.Views
{
    class BoolToStringAdmittedConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool input = (bool)value;

                if (input == true)
                {
                    return "Ja";
                }
                else
                {
                    return "Nej";
                }
            }
            else
            {
                return "";
            }
        }



        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            throw new ArgumentNullException();
        }
    }
}

