using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Gadgeothek
{

    public class MyEnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return (ch.hsr.wpf.gadgeothek.domain.Condition)Enum.Parse(typeof(ch.hsr.wpf.gadgeothek.domain.Condition), value.ToString(), true);
        }
    }

    public class MyEnumToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)((ch.hsr.wpf.gadgeothek.domain.Condition)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return (ch.hsr.wpf.gadgeothek.domain.Condition)((int)value);
        }
    }
}
