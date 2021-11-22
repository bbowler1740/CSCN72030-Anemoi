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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CSCN72030_Anemoi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //var panel = new ConditionalActionPanel();
            //main.Children.Add(panel);


            // ----------- Device List Intergration 
            //List<Devices> deviceLists = new List<Devices>();
            //DeviceList newList = new DeviceList();
            //newList.createDevice("Canopy","Bob","This is a canopy, his name is bob."); 
            //newList.save(ApplicationData.Current.LocalFolder.Path);
            //newList.load(ApplicationData.Current.LocalFolder.Path);
            //newList.removeDevice(1);

            // ------------ Device Intergration 
            //List<Devices> deviceLists = new List<Devices>();
            //DeviceList newList = new DeviceList();
            //newList.createDevice("Canopy","Bo|b","This is :a canopy, his name is bob.");
            //Devices canopy = newList.search(1);

            // ------------ Sensor Integration

            //List<Sensor> sensorList = new List<Sensor>();
            //SensorList coolBeans = new SensorList();


            //Integration test to determine if the List module can create a sensor object.

            //string type = "Wind Speed";
            //string name = "Windy Boi";
            //string location = "Backyard";
            //coolBeans.createSensor(type, name, location);

            //Integration test to determine the List modules ability to access properties of the Sensor object.

            //float test = coolBeans.search(1).SensorData;
            //int test2 = coolBeans.search(1).SensorID;
            //string test3 = coolBeans.search(1).SensorNickName;
            //string test4 = coolBeans.search(1).SensorLocation;

            //Integration test to determine Sensor module's ability to receive the correct directory from which to load its external weather data form.



            //Integration test to determine if the Sensor module can send it's save data string to the list module.

            //string teststring = coolBeans.search(1).SaveInfoToString();


            //Integration test to determine if the Sensor module can correctly interpret a string containing the Sensor load data.

            //coolBeans.save(ApplicationData.Current.LocalFolder.Path);
            //coolBeans.load(ApplicationData.Current.LocalFolder.Path);


            // ------------ Condition Analyzer Integration
            /*            var ca = new ConditionAnalyzer();

                        SensorList coolBeans = new SensorList();
                        var weather = WeatherConditions.GetCurrentWeather(coolBeans.getSensorList());

                        string type = "Wind Speed";
                        string name = "Windy Boi";
                        string location = "Backyard";
                        coolBeans.createSensor(type, name, location);
                        var condition = new Condition(coolBeans.search(1), true, -1, 15);

                        DeviceList newList = new DeviceList();
                        newList.createDevice("Canopy","Bob","This is a canopy, his name is bob.");
                        var action = new Action(newList.search(1), true);

                        var conditions = new List<Condition>();
                        conditions.Add(condition);
                        var actions = new List<Action>();
                        actions.Add(action);
                        ca.AddConditionalAction("custom", conditions, actions);

                        var b = ca.IsActionEnabled("custom");*/

            //ca.UpdateConditionalAction("custom", "yes", conditions, actions, false);

            //b = ca.IsActionEnabled("yes");
            /*
                        ca.DeviceList = newList;
                        ca.SensorList = coolBeans;

                        ca.ProcessAllConditionalActions();

                        ca.Save(ApplicationData.Current.LocalFolder.Path);

                        var temp = new ConditionAnalyzer();
                        temp.Load(ApplicationData.Current.LocalFolder.Path);

                        var readings = WeatherConditions.GetWeatherReadings(Weather.Rain);*/

            // ------------ Location Integration
/*
            Location location = new Location();
            location.createLocation("Joe");
            location.save();
            location.selectLocation("Joe");
            string type = "Wind Speed";
            string name = "Windy Boi";
            string area = "Backyard";
            location.getCA().SensorList.createSensor(type, name, area);
            location.getCA().DeviceList.createDevice("Canopy", "Bob", "This is a canopy, his name is bob.");
            Condition condition = new Condition(location.getCA().SensorList.search(1), true, 0, 100);
            Action action = new Action(location.getCA().DeviceList.search(1), true);
            var conlist = new List<Condition>();
            conlist.Add(condition);
            var actlist = new List<Action>();
            actlist.Add(action);
            location.getCA().AddConditionalAction(conlist, actlist);
            location.save();
            location.createLocation("Bob");
            location.createLocation("Alice");
            Location.displayLocation();
            location.deleteLocation("Joe");*/



        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string locatoinName = e.Parameter.ToString();

            Stuff.Text = locatoinName;

            base.OnNavigatedTo(e);      //Calls the parents implementation of the fuction

        }
    }
}
