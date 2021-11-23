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
    public sealed partial class CreateLocationPanel : UserControl
    {
        public System.Action<UserControl> Close;

        Frame frame;

        public CreateLocationPanel(Frame frameTemp)
        {
            this.InitializeComponent();

            frame = frameTemp;
        }

        private void btnCancelClick(object sender, RoutedEventArgs e)
        {
            Close?.Invoke(this);

        }

        private void btnCreateLocationClick(object sender, RoutedEventArgs e)
        {
            Location newLocation = new Location();

            newLocation.createLocation(locationName.Text);

            frame.Navigate(typeof(MainPage), newLocation);

        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {


        }

    }
}
