using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace P3_Midwife.Views
{
    public class GenderStringToCharConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
      
            if (value is char && value.ToString() != "\0")
            {
                value = value.ToString().ToLower();
                char input = char.Parse((string)value);
              //  char input = (char)value;
                if (input == 'd')
                {
                    return "Dreng";
                }
                else if (input == 'p')
                {
                    return "Pige";
                }
                else if (input == 'u')
                {
                    return "Ukendt";
                }
                
            }
            return null;
            throw new ArgumentNullException();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string input = (string)value;
                if (input.ToLower() == "dreng")
                {
                    return 'd';
                }
                else if (input.ToLower() == "pige")
                {
                    return 'p';
                }
                else if (input.ToLower() == "ukendt")
                {
                    return 'u';
                }

            }
            return null;
            throw new ArgumentNullException();
        }
    }

}
