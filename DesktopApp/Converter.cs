using System.Globalization;
using System.Windows.Data;

namespace DesktopApp
{
  internal class Converter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      string txt = value as string;
      return txt?.ToUpper();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      string txt = value as string;
      return txt?.ToLower();
    }
  }
}
