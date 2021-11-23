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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CSCN72030_Anemoi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LocationSelect : Page
    {

        StackPanel backgroundFade;

        public LocationSelect()
        {
            this.InitializeComponent();

            //Get the locations

            //string[] listOfLocations = Location.displayLocation();
            string[] listOfLocations = { "Hello", "Hia", "Hey" };

            //print to the listview
            LocationList.ItemsSource = listOfLocations;

            backgroundFade = new StackPanel();
            backgroundFade.Background = new SolidColorBrush(Windows.UI.Colors.White);
            backgroundFade.Opacity = 0.5;
            backgroundFade.VerticalAlignment = VerticalAlignment.Stretch;
            backgroundFade.HorizontalAlignment = HorizontalAlignment.Stretch;

        }

       private void ListLocationSelect(object sender, SelectionChangedEventArgs e)
        {
           string name = (string)LocationList.SelectedItem;  // Obtains the name of the selected item

            Location tempLoc = new Location();

            tempLoc.selectLocation(name);

            Frame.Navigate(typeof(MainPage), tempLoc); //Opens to the main page passing through the name
        }

        private void btnCancelClick(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void btnCreateNewLocation(object sender, RoutedEventArgs e)
        {
            var panel = new CreateLocationPanel(Frame);
            panel.Close = ClosePanel;

            panel.VerticalAlignment = VerticalAlignment.Center;
            panel.HorizontalAlignment = HorizontalAlignment.Center;

            gridLocationSelect.Children.Add(backgroundFade);

            gridLocationSelect.Children.Add(panel);
        }

        private void ClosePanel(UserControl sender)
        {
            gridLocationSelect.Children.Remove(backgroundFade);

            gridLocationSelect.Children.Remove(sender);
        }

        private void ClosePanel(UserControl sender)
        {
            gridLocationSelect.Children.Remove(backgroundFade);

            gridLocationSelect.Children.Remove(sender);
        }
    }
}
