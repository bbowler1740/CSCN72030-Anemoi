using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
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
    
    public sealed partial class AddSensorPanel : UserControl
    {
        private ConditionAnalyzer CA;
        public System.Action<UserControl> Close;
        public AddSensorPanel(ConditionAnalyzer ca)
        {
            this.InitializeComponent();

            //Local Condition Analyzer
            CA = ca;

            //Add items individually
            cboSensorTypes.Items.Add("Air Pressure");
            cboSensorTypes.Items.Add("Custom");
            cboSensorTypes.Items.Add("Ground Moisture");
            cboSensorTypes.Items.Add("Humidity");
            cboSensorTypes.Items.Add("Precipitation");
            cboSensorTypes.Items.Add("Sensor");
            cboSensorTypes.Items.Add("Sunlight");
            cboSensorTypes.Items.Add("Temperature");
            cboSensorTypes.Items.Add("Wind Direction");
            cboSensorTypes.Items.Add("Wind Speed");


        }

        public void btnSenCancelClick(object sender, RoutedEventArgs e)
        {

            Close?.Invoke(this);
           
        }

        public void btnAddSenClick(object sender, RoutedEventArgs e)
        {
            //Only enable button if textboxes contain something
            

            //Get the textbox data
            string sensorNickName = txtNickname.Text;
            int sensorID = Int32.Parse(txtID.Text);
            string sensorLocation = txtLocation.Text;

            //Get the dropbox selection for sensor type
            string sensorType = cboSensorTypes.SelectedItem.ToString();

            //Send the data to the sensor creation method
            CA.SensorList.createSensor(sensorType, sensorNickName, sensorLocation, sensorID);

            Close?.Invoke(this);
           

        }

        public void txtNickname_TextChanged(object sender, TextChangedEventArgs e)
        {

            setBtnAddSenVisibility();

        }

        public void txtID_TextChanged(object sender, TextChangedEventArgs e)
        {

            setBtnAddSenVisibility();

        }

        public void txtLocation_TextChanged(object sender, TextChangedEventArgs e)
        {

            setBtnAddSenVisibility();

        }

        public void cboSensorTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            setBtnAddSenVisibility();

        }

        private void setBtnAddSenVisibility()
        {

            if ((txtNickname.Text != string.Empty) && (txtID.Text != string.Empty) && (txtLocation.Text != string.Empty) && (cboSensorTypes.SelectedItem != null))
            {

                btnAddSen.IsEnabled = true;

            }

            else
            {

                btnAddSen.IsEnabled = false;

            }

        }








    }
}
