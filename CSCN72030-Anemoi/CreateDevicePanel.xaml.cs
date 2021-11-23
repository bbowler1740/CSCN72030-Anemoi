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
    public sealed partial class CreateDevicePanel : UserControl
    {

        public System.Action<UserControl> Close;

        DeviceList list;

        
        public CreateDevicePanel(DeviceList tempList)
        {
            this.InitializeComponent();

            list = tempList;
        }

        private void bntCancelClick(object sender, RoutedEventArgs e)
        {

            Close?.Invoke(this);

        }

        private void btnCreateDeviceClick(object e, RoutedEventArgs e1)
        {

            string type = cmbOuter.SelectedItem.ToString(); //No clue if this will work

            list.createDevice(type, txtBoxName.Text, txtBoxDesc.Text);

            Close?.Invoke(this);

        }
    }
}
