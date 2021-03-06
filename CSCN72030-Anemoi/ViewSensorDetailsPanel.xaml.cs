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
    public sealed partial class ViewSensorDetailsPanel : UserControl
    {

        private ConditionAnalyzer CA;
        public System.Action<UserControl> Close;
        int sensorId;

        public ViewSensorDetailsPanel(ConditionAnalyzer ca, int id)
        {
            this.InitializeComponent();

            CA = ca;
            sensorId = id;

            //Initialize object data on panel start-up.
            txtDetailedNickname.Text = "Nickname: " + ca.SensorList.search(id).SensorNickName;
            txtDetailedID.Text = "ID: " + id.ToString();
            txtDetailedLocation.Text = "Location: " + ca.SensorList.search(id).SensorLocation;
            txtDetailedData.Text = "Data: " + ca.SensorList.search(id).SensorData;

        }

        public void btnSenDetailsCancelClick(object sender, RoutedEventArgs e)
        {
            Close?.Invoke(this);
        }

        public void btnSenDetailsRemoveClick(object sender, RoutedEventArgs e)
        {

            CA.SensorList.removeSensor(sensorId);

            Close?.Invoke(this);

        }

        private void PointerEntered(object sender, PointerRoutedEventArgs e)
        {

            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);

        }

        private void PointerExited(object sender, PointerRoutedEventArgs e)
        {

            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);

        }

    }
}
