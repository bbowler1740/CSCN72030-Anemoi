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
    public sealed partial class ActionDataItem : UserControl
    {
        private DeviceList DeviceList;
        public Devices SelectedDevice { get {
                if (cboDevice.SelectedIndex == -1)
                {
                    return null;
                }
                return DeviceList.search(Convert.ToInt32(cboDevice.SelectedValue));
            }
        }
        public bool IsTurnOn { get => Convert.ToBoolean(chkTurnOn.IsChecked); }
        public ActionDataItem(DeviceList deviceList)
        {
            this.InitializeComponent();
            DeviceList = deviceList;

            var deviceItems = new List<ListDeviceItem>();
            foreach (var item in deviceList.getDeviceList())
            {
                deviceItems.Add(new ListDeviceItem() { DeviceType = item.GetType().Name, DeviceId = item.GetDeviceID() });
            }
            cboDevice.ItemsSource = deviceItems;
        }
    }

    public class ListDeviceItem
    {
        public string DeviceType { get; set; }
        public uint DeviceId { get; set; }
    }
}
