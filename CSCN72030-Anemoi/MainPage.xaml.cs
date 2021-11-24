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
        Location location;
        StackPanel backgroundFade;

        public MainPage()
        {
            this.InitializeComponent();

            backgroundFade = new StackPanel();
            backgroundFade.Background = new SolidColorBrush(Windows.UI.Colors.White);
            backgroundFade.Opacity = 0.5;
            backgroundFade.VerticalAlignment = VerticalAlignment.Stretch;
            backgroundFade.HorizontalAlignment = HorizontalAlignment.Stretch;
            backgroundFade.SetValue(Grid.RowSpanProperty, 2);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            location = e.Parameter as Location;

            Title.Text = location.locationName;
            UpdateLiveData();

            base.OnNavigatedTo(e);      //Calls the parents implementation of the fuction

        }

        private void btnGoLocation(object sender, RoutedEventArgs e)
        {
            location.save();
            Frame.Navigate(typeof(LocationSelect));
        }

        private void btnGoDevice(object sender, RoutedEventArgs e)
        {
            var panel = new CreateDevicePanel(location.getCA().DeviceList);
            panel.Close = ClosePanel;

            panel.SetValue(Grid.RowSpanProperty, 2);
            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.HorizontalAlignment = HorizontalAlignment.Center;

            main.Children.Add(backgroundFade);

            main.Children.Add(panel);
        }

        private void btnGoSensor(object sender, RoutedEventArgs e)
        {
            var panel = new AddSensorPanel(location.getCA());
            panel.Close = ClosePanel;

            panel.SetValue(Grid.RowSpanProperty, 2);
            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.HorizontalAlignment = HorizontalAlignment.Center;

            main.Children.Add(backgroundFade);

            main.Children.Add(panel);
        }

        private void btnGoConditional(object sender, RoutedEventArgs e)
        {
            var panel = new ConditionalActionPanel(location.getCA());
            panel.Close = ClosePanel;

            panel.SetValue(Grid.RowSpanProperty, 2);
            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.HorizontalAlignment = HorizontalAlignment.Center;

            main.Children.Add(backgroundFade);

            main.Children.Add(panel);
        }

        private void UpdateLiveData()
        {
            //Current Weather : Sunny     21:45
            var curWeather = WeatherConditions.GetCurrentWeather(location.getCA().SensorList.getSensorList());
            LiveData.Text = string.Format("Current Weather: {0}\t\t{1}", curWeather.ToString(), DateTime.Now.ToString("HH:mm"));

            //Sensors Sections
            //is this where we read from file?

            //Conditional Actions Section
            listViewConditionalActions.ItemsSource = new List<ConditionalActionData>() {
                new ConditionalActionData() { Conditions = "Temperature > 10, Wind Speed < 60", Actions = "Canopy = On", Status = "Enabled" },
                new ConditionalActionData() { Conditions = "Sunlight < 10", Actions = "Lights = On, Pool Heater = Off", Status = "Disabled" }
            };

            location.getCA().ProcessAllConditionalActions();

            //Devices Sections
            //Do this last because sensors/conditionalAction can change their states
        }

        private void ClosePanel(UserControl sender)
        {
            main.Children.Remove(backgroundFade);

            main.Children.Remove(sender);
        }
    }
}
