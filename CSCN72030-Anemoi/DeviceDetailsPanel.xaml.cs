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
    public sealed partial class DeviceDetailsPanel : UserControl
    {
        DeviceList list;
        Devices device;

        public System.Action<UserControl> Close;

        public DeviceDetailsPanel(DeviceList tempList, int id)
        {

            this.InitializeComponent();

            list = tempList;

           device = list.search(id);



            //Change the values of the panel

            txtBlockName.Text = device.GetName();
            txtBlockType.Text = device.GetType().ToString();
            txtBlockDesc.Text = device.GetDescription();
            txtBlockType.Text = device.GetType().Name;
            toggleSwitch.IsOn = device.GetState();

        }

        private void btnDoneClick(object sender, RoutedEventArgs e)
        {

            Close?.Invoke(this);

        }

        private void btnDeleteClick(object sender, RoutedEventArgs e1)
        {

             list.removeDevice(device.GetDeviceID());
            
            Close?.Invoke(this);
        }

        private void toggleSwitchToggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch tSwitch = sender as ToggleSwitch;

            var element = device;

            if (element == null)
            {
                return;
            }

            if (tSwitch.IsOn)
            {
                element.TurnOn();

            }
            else
            {
                element.TurnOff();
            }
        }
    }
}
