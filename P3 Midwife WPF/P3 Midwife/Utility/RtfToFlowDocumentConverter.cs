using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Controls;
using System.IO;
using System.Net.Sockets;

namespace P3_Midwife.Utility
{
    public class RtfToFlowDocumentConverter : IValueConverter
    {
             public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
             {
                 FlowDocument document = new FlowDocument();
                 TextRange range = new TextRange(document.ContentStart, document.ContentEnd);
        
        
             }
        
             public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
             {
                 throw new NotImplementedException();
             }
         
    }
}
