using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PdfMerger.UI.Converters
{
    public class BoolToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val && parameter is string par)
            {
                switch (par)
                {
                    case "checked":
                        return val ? "✔" : "✖";
                    default:
                        throw new NotImplementedException();
                }
            }
            throw new NotImplementedException();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
