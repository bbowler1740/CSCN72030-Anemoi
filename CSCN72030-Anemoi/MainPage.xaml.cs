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
using Windows.Storage;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CSCN72030_Anemoi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        StackPanel backgroundFade;

        public MainPage()
        {
            this.InitializeComponent();

            backgroundFade = new StackPanel();
            backgroundFade.Background = new SolidColorBrush(Windows.UI.Colors.White);
            backgroundFade.Opacity = 0.5;
            backgroundFade.VerticalAlignment = VerticalAlignment.Stretch;
            backgroundFade.HorizontalAlignment = HorizontalAlignment.Stretch;

            listViewConditionalActions.ItemsSource = new List<ConditionalActionData>() { 
                new ConditionalActionData() { Conditions = "Temperature > 10, Wind Speed < 60", Actions = "Canopy = On", Status = "Enabled" },
                new ConditionalActionData() { Conditions = "Sunlight < 10", Actions = "Lights = On, Pool Heater = Off", Status = "Disabled" }
            };
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Location loc = e.Parameter as Location;

            //Stuff.Text = loc.locationName;

            base.OnNavigatedTo(e);      //Calls the parents implementation of the fuction

        }

        private void btnGoLocation(object sender, RoutedEventArgs e)
        {

        }

        private void btnGoDevice(object sender, RoutedEventArgs e)
        {

        }

        private void btnGoSensor(object sender, RoutedEventArgs e)
        {

        }

        private void btnGoConditional(object sender, RoutedEventArgs e)
        {

        }

        private void ClosePanel(UserControl sender)
        {
            main.Children.Remove(backgroundFade);

            main.Children.Remove(sender);
        }
    }

    public class ConditionalActionData
    {
        public string Conditions { get; set; }
        public string Actions { get; set; }
        public string Status { get; set; }
    }

    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = (string)value;
            return val == "Enabled" ? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.Red);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
