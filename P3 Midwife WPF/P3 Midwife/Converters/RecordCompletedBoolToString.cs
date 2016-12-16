using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;

namespace P3_Midwife.Views
{
    public class RecordCompletedBoolToString:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                List<Record> temp = value as List<Record>;
                Record tempRecord = temp.First();
                bool input = tempRecord.IsActive;
  
                    if (input == true)
                    {
                        return "Indlagt";
                    }
                    else
                    {
                        return "";
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
