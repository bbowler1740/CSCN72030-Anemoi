using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace CSCN72030_Anemoi
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (string)value;
            //return val == "Enabled" ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red); Left in to maybe change it to match the reds?
            return val == "Enabled" ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Color.FromArgb(0xff, 0x9B, 0x29, 0x15));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
