using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace CSCN72030_Anemoi
{
    public sealed partial class ConditionDataItem : UserControl
    {
        private SensorList SensorList;
        public Sensor SelectedSensor { get
            {
                if (cboSensor.SelectedIndex == -1)
                {
                    return null;
                }
                return SensorList.search((int)cboSensor.SelectedValue);
            }
        }
        public string LowText { get => txtLow.Text; }
        public string HighText { get => txtHigh.Text; }
        public bool IsBetween { get => Convert.ToBoolean(chkIsBetween.IsChecked); }
        public ConditionDataItem(SensorList sensorList)
        {
            this.InitializeComponent();
            SensorList = sensorList;

            var sensorItems = new List<ListSensorItem>();
            foreach (var item in sensorList.getSensorList())
            {
                sensorItems.Add(new ListSensorItem() { SensorType = item.GetType().Name, SensorId = item.SensorID });
            }
            cboSensor.ItemsSource = sensorItems;
        }

        private void TextBox_OnBeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }
    }

    public class ListSensorItem
    {
        public string SensorType { get; set; }
        public int SensorId { get; set; }
    }
}
