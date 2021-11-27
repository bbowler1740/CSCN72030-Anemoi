using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class ConditionalActionPanel : UserControl
    {
        private ConditionAnalyzer CA;
        public System.Action<UserControl> Close;
        private int updateId;

        public ConditionalActionPanel(ConditionAnalyzer ca)
        {
            this.InitializeComponent();
            CA = ca;

            listViewSensors.Items.Add(new ConditionDataItem(CA.SensorList));

            listViewDevices.Items.Add(new ActionDataItem(CA.DeviceList));
        }

        public ConditionalActionPanel(ConditionAnalyzer ca, ConditionalActionData caData)
        {
            this.InitializeComponent();
            CA = ca;
            btnSave.Content = "Update";
            updateId = caData.Id;
            tglIsEnabled.Visibility = Visibility.Visible;
            tglIsEnabled.IsOn = caData.Status;

            foreach (var condition in caData.Conditions)
            {
                listViewSensors.Items.Add(new ConditionDataItem(CA.SensorList)
                {
                    SelectedSensor = condition.Sensor,
                    LowText = condition.LowThreshold.ToString(),
                    HighText = condition.HighThreshold.ToString(),
                    IsBetween = condition.IsBetweenTrigger
                });
            }

            foreach (var action in caData.Actions)
            {
                listViewDevices.Items.Add(new ActionDataItem(CA.DeviceList)
                {
                    SelectedDevice = action.Device,
                    IsTurnOn = action.OutputState
                });
            }
        }

        private void btnAddSensor_Click(object sender, RoutedEventArgs e)
        {
            var item = new ConditionDataItem(CA.SensorList);
            listViewSensors.Items.Add(item);
            listViewSensors.ScrollIntoView(item);
        }

        private void btnAddDevice_Click(object sender, RoutedEventArgs e)
        {
            var item = new ActionDataItem(CA.DeviceList);
            listViewDevices.Items.Add(item);
            listViewDevices.ScrollIntoView(item);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var conditions = new List<Condition>();
            foreach (var conditionItem in listViewSensors.Items)
            {
                var conditionDataItem = conditionItem as ConditionDataItem;
                if (conditionDataItem.SelectedSensor == null || string.IsNullOrEmpty(conditionDataItem.LowText) || string.IsNullOrEmpty(conditionDataItem.HighText))
                {
                    continue;
                }
                Condition condition = new Condition(conditionDataItem.SelectedSensor, conditionDataItem.IsBetween, Convert.ToSingle(conditionDataItem.LowText), Convert.ToSingle(conditionDataItem.HighText));
                conditions.Add(condition);
            }

            var actions = new List<Action>();
            foreach (var actionItem in listViewDevices.Items)
            {
                var actionDataItem = actionItem as ActionDataItem;
                var action = new Action(actionDataItem.SelectedDevice, actionDataItem.IsTurnOn);
                actions.Add(action);
            }

            if (btnSave.Content.ToString() == "Save")
            {
                CA.AddConditionalAction(conditions, actions);
            }
            else
            {
                CA.UpdateConditionalAction(updateId, conditions, actions, tglIsEnabled.IsOn);
            }
            Close?.Invoke(this);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
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
