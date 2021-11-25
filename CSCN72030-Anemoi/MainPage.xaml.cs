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
        List<TextBlock> listOfTextBlock;
        List<StackPanel> listOfPanels;

        public MainPage()
        {
            this.InitializeComponent();

            backgroundFade = new StackPanel();
            backgroundFade.Background = new SolidColorBrush(Windows.UI.Colors.White);
            backgroundFade.Opacity = 0.5;
            backgroundFade.VerticalAlignment = VerticalAlignment.Stretch;
            backgroundFade.HorizontalAlignment = HorizontalAlignment.Stretch;
            backgroundFade.SetValue(Grid.RowSpanProperty, 2);


            listOfTextBlock = new List<TextBlock>();
            foreach (var element in grdSensorSection.Children)
            {
                if (element is TextBlock && (element as TextBlock).Tag != null)
                {
                    listOfTextBlock.Add(element as TextBlock);
                    
                }
            }

            listOfPanels = new List<StackPanel>();
            foreach (var element in gridDevicesInner.Children)
            {
                var item = element as StackPanel;
                listOfPanels.Add(item);

            }



        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            location = e.Parameter as Location;

            Sensor.UpdateWeatherScenario(ApplicationData.Current.LocalFolder.Path + @"\" + location.locationName);

            Title.Text = location.locationName;
            UpdateLiveData();

            base.OnNavigatedTo(e);      //Calls the parents implementation of the fuction

            

        }

        private void btnGoLocation(object sender, RoutedEventArgs e)
        {
            Save();
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateLiveData();
        }

        private void UpdateLiveData()
        {
            //Current Weather : Sunny     21:45
           

            //Sensors Sections
            

            listViewCustomSensors.ItemsSource = location.getCA().SensorList.getCustomSensorList();

            var listOfSensors = location.getCA().SensorList.getSensorList();

            //is this where we read from file?




            foreach (var sensor in listOfSensors)
            {
                sensor.ReadScenarioDataFromFile();

                foreach (TextBlock textBlock in listOfTextBlock)
                {
                    
                    if (textBlock.Tag.ToString().StartsWith(sensor.GetType().Name))
                    {
                        textBlock.Text = string.Format("{0}{1}", sensor.SensorData, textBlock.Tag.ToString().Replace(sensor.GetType().Name, ""));

                        textBlock.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }

            }

            var curWeather = WeatherConditions.GetCurrentWeather(location.getCA().SensorList.getSensorList());
            LiveData.Text = string.Format("Current Weather: {0}\t\t{1}", curWeather.ToString(), DateTime.Now.ToString("HH:mm"));

            //Conditional Actions Section
            listViewConditionalActions.ItemsSource = new List<ConditionalActionData>() {
                new ConditionalActionData() { Conditions = "Temperature > 10, Wind Speed < 60", Actions = "Canopy = On", Status = "Enabled" },
                new ConditionalActionData() { Conditions = "Sunlight < 10", Actions = "Lights = On, Pool Heater = Off", Status = "Disabled" }
            };

            location.getCA().ProcessAllConditionalActions();

            //Devices Sections
            //Do this last because sensors/conditionalAction can change their states
            var customDevices = location.getCA().DeviceList.getCustomDeviceList();
            var customDeviceListdata = new List<DeviceData>();
            foreach (var customdev in customDevices)
            {
                customDeviceListdata.Add(new DeviceData() { Name = customdev.GetName(), Device = customdev });
            }
            listViewCustomDevices.ItemsSource = customDeviceListdata;


            var listOfDevices = location.getCA().DeviceList.getDeviceList();

            foreach (var device in listOfDevices)
            {
                foreach (StackPanel element in listOfPanels)
                {
                    bool isHere = false;

                    foreach (var things in element.Children)
                    {
                        
                        if(things is ToggleSwitch)
                        {
                            var tSwitch = things as ToggleSwitch;

                            if (tSwitch.Tag.ToString().StartsWith(device.GetType().Name))
                            {
                                tSwitch.IsEnabled = true;
                                tSwitch.IsOn = device.GetState();
                                isHere = true;
                            }
                            
                        }
                     
                        else if(things is TextBlock && (things as TextBlock).Tag != null && isHere)
                        {

                            var textBlock = things as TextBlock;
                            
                            textBlock.Text = "";
                        } 
                    }
                }
                    
            }

        }

        private void ClosePanel(UserControl sender)
        {
            main.Children.Remove(backgroundFade);

            main.Children.Remove(sender);

            UpdateLiveData();
            Save();
        }

        public void Save()
        {
            location.save();
        }

        private void toggleSwitchToggled(object sender, RoutedEventArgs e)
        {
            //set the state of the device

            ToggleSwitch tSwitch = sender as ToggleSwitch;

            var element = location.getCA().DeviceList.getDeviceList().Where (x => x.GetType().Name == tSwitch.Tag.ToString()).FirstOrDefault();
            
            if(element == null)
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

        private void btnNextScenario_Click(object sender, RoutedEventArgs e)
        {
            Sensor.UpdateWeatherScenario(ApplicationData.Current.LocalFolder.Path + @"\" + location.locationName);
            UpdateLiveData();

        }
    }
}
