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

    public class WindDirectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (float)value;

            if (val < 0)
            {

                return "No Sensor";

            }

            if (val < 45.00 || val > 315.00)
            {
                return "North";
            }
            if (val < 135.00 || val > 45.00)
            {
                return "East";
            }
            if (val < 225.00 || val > 135.00)
            {
                return "South";
            }
            if (val < 315.00 || val > 225.00)
            {
                return "West";
            }

            return "No Sensor";

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0f;
        }
    }

    public class GenericConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0f;
        }
    }

    public class WindSpeedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (float)value;

            if (val <= 40)
            {
                return new SolidColorBrush(Colors.Black);
            }
            else if (val <= 80)
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else if (val > 80)
            {
                return new SolidColorBrush(Colors.Red);
            }

            return new SolidColorBrush(Color.FromArgb(0xff, 0x9B, 0x29, 0x15));

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0f;
        }
    }

    public class TemperatureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (float)value;

            if (val <= 0)
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else if (val <= 25)
            {
                return new SolidColorBrush(Colors.Black);
            }
            else if (val <= 40)
            {
                return new SolidColorBrush(Colors.Orange);
            }
            else if (val > 40)
            {
                return new SolidColorBrush(Colors.Red);
            }

            return new SolidColorBrush(Color.FromArgb(0xff, 0x9B, 0x29, 0x15));

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0f;
        }
    }

    public class SunlightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (float)value;

            if (val <= 1000)
            {
                return new SolidColorBrush(Colors.Blue);
            }
            else if (val <= 100000)
            {
                return new SolidColorBrush(Colors.Black); //cloudy
            }
            else if (val > 100000)
            {
                return new SolidColorBrush(Colors.Orange); //too sunny
            }

            return new SolidColorBrush(Color.FromArgb(0xff, 0x9B, 0x29, 0x15));

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0f;
        }
    }

    public class PrecipitationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (float)value;

            if (val <= 20)
            {
                return new SolidColorBrush(Colors.Black); //light
            }
            else if (val <= 50)
            {
                return new SolidColorBrush(Colors.Orange); //heavy
            }
            else if (val > 50)
            {
                return new SolidColorBrush(Colors.Red);
            }

            return new SolidColorBrush(Color.FromArgb(0xff, 0x9B, 0x29, 0x15));

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 0f;
        }
    }
}
