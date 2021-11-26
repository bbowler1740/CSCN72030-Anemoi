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
using System.ComponentModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CSCN72030_Anemoi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page , INotifyPropertyChanged
    {
        Location location;
        StackPanel backgroundFade;
        List<TextBlock> listOfTextBlock;
        List<StackPanel> listOfPanels;

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged (string info)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));

        }

        public float windDirection 
        { 
            get 
            {
               
              var test = location.getCA().SensorList.getSensorList().Where(x => x.GetType().Name == "WindDirection").FirstOrDefault(); 
                
                if (test != null)
                {
                    return test.SensorData;
                }

                return -1;

            }

            set 
            {

                NotifyPropertyChanged(nameof(windDirection));

            }

        }

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
                if (element is TextBlock)
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

            foreach (TextBlock textBlock in listOfTextBlock)
            {
                textBlock.IsTapEnabled = false;

                if (textBlock.Tag != null)
                {
                    textBlock.Text = "No Sensor";
                    textBlock.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x9B, 0x29, 0x15));
                }
            }

            foreach (var sensor in listOfSensors)
            {

                sensor.ReadScenarioDataFromFile();
                foreach (TextBlock textBlock in listOfTextBlock)
                {

                    if (textBlock.Tag == null)
                    {

                        if (sensor.GetType().Name == textBlock.Text.Replace(" ", ""))
                        {
                            textBlock.IsTapEnabled = true;
                        }

                    }

                    else if (textBlock.Tag.ToString().StartsWith(sensor.GetType().Name))
                    {
                        textBlock.Text = string.Format("{0}{1}", sensor.SensorData, textBlock.Tag.ToString().Replace(sensor.GetType().Name, ""));
                        textBlock.Foreground = new SolidColorBrush(Colors.Black);
                    }
                }
            }

            var curWeather = WeatherConditions.GetCurrentWeather(location.getCA().SensorList.getSensorList());
            LiveData.Text = string.Format("Current Weather: {0}\t\t{1}", curWeather.ToString(), DateTime.Now.ToString("HH:mm"));

            //Conditional Actions Section
            var listOfCondtions = new List<ConditionalActionData>();

            foreach (var caID in location.getCA().GetListOfName())
            {
                listOfCondtions.Add(new ConditionalActionData()
                {
                    Id = caID,
                    Conditions = location.getCA().GetConditions(caID),
                    Actions = location.getCA().GetActions(caID),
                    Status = location.getCA().IsActionEnabled(caID)
                });
            }
            listViewConditionalActions.ItemsSource = listOfCondtions;

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

            foreach (StackPanel panel in listOfPanels)  // Turn to default states
            {

                foreach (var things in panel.Children)
                {

                    if (things is ToggleSwitch)
                    {
                        var tSwitch = things as ToggleSwitch;

                        //tSwitch.IsOn = false;   //Is Toggled gets called cannt be used
                        //tSwitch.IsEnabled = false;

                    }
                    else if (things is TextBlock)
                    {
                        var textBlock = things as TextBlock;

                        if ((things as TextBlock).Tag.ToString() == "Status")  //Error Message Logic
                        {

                            textBlock.Text = "No Device";

                        }
                        else if ((things as TextBlock).Tag.ToString() != "Status") //Tappable Logic
                        {

                            textBlock.IsTapEnabled = false;

                        }

                    }

                }

            
            }
    

            foreach (var device in listOfDevices)   //Changing the state of the panel children to on
            {
                foreach (StackPanel element in listOfPanels)
                {
                    bool isHere = false;

                    foreach (var things in element.Children)
                    {
                        
                        if(things is ToggleSwitch)
                        {
                            var tSwitch = things as ToggleSwitch;

                            if (tSwitch.Tag.ToString().StartsWith(device.GetType().Name))      //Switch Logic
                            {
                                
                                tSwitch.IsOn = device.GetState();
                                tSwitch.IsEnabled = true;

                            }
   
                        }
                        else if (things is TextBlock)
                        {
                            var textBlock = things as TextBlock;

                            if((things as TextBlock).Tag.ToString() == "Status" && isHere)  //Error Message Logic
                            {

                                    textBlock.Text = "";
                              
                            }
                            else if((things as TextBlock).Tag.ToString() != "Status") //Tappable Logic
                            {

                                if (textBlock.Tag.ToString().StartsWith(device.GetType().Name)){
                                    textBlock.IsTapEnabled = true;
                                    isHere = true;
                                }

                            }

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

        private void listViewConditionalActions_ItemClicked(object sender, ItemClickEventArgs e)
        {
            var panel = new ConditionalActionPanel(location.getCA(), e.ClickedItem as ConditionalActionData);
            panel.Close = ClosePanel;

            panel.SetValue(Grid.RowSpanProperty, 2);
            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.HorizontalAlignment = HorizontalAlignment.Center;

            main.Children.Add(backgroundFade);

            main.Children.Add(panel);
        }

        //Opens the detailed device 
        private void txtBlockDeviceDetails_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TextBlock txtBlock = sender as TextBlock;
            int id = -1;

            foreach(Devices device in location.getCA().DeviceList.getDeviceList())
            {

                if (txtBlock.Tag.ToString() == device.GetType().Name)
                {
                    id = (int)device.GetDeviceID();
                }
            }

            var panel = new DeviceDetailsPanel(location.getCA().DeviceList, id);
            panel.Close = ClosePanel;

            panel.SetValue(Grid.RowSpanProperty, 2);
            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.HorizontalAlignment = HorizontalAlignment.Center;

            main.Children.Add(backgroundFade);

            main.Children.Add(panel);


        }

        private void txtBlockSensorDetails_Tapped(object sender, TappedRoutedEventArgs e)
        {

            TextBlock textBlock = sender as TextBlock;
            int id = 0;

            foreach (Sensor sensor in location.getCA().SensorList.getSensorList())
            {

                string parse = sensor.GetType().Name;

                if (parse == textBlock.Text.ToString().Replace(" ", ""))
                {

                    id = sensor.SensorID;

                }

            }

            var panel = new ViewSensorDetailsPanel(location.getCA(), id);
            panel.Close = ClosePanel;

            panel.SetValue(Grid.RowSpanProperty, 2);
            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.HorizontalAlignment = HorizontalAlignment.Center;

            main.Children.Add(backgroundFade);

            main.Children.Add(panel);

        }

        private void listViewCustomDevices_ItemClicked(object sender, ItemClickEventArgs e)
        {

            DeviceData item = (DeviceData)e.ClickedItem;

            var panel = new DeviceDetailsPanel(location.getCA().DeviceList, (int)item.DeviceID) ;
            panel.Close = ClosePanel;

            panel.SetValue(Grid.RowSpanProperty, 2);
            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.HorizontalAlignment = HorizontalAlignment.Center;

            main.Children.Add(backgroundFade);

            main.Children.Add(panel);

        }
    }
}
