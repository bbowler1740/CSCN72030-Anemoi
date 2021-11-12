using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSCN72030_Anemoi
{
    public class DeviceList : aList
    {
        public string path = @"DeviceList.txt";
        List<DeviceList> list = new List<DeviceList>(); // Must change Device List to Devices when intergrate

        // change data types fromDeviceList to Device when integrated

        /*        public int CompareTo()
                {
                    return this.id.CompareTo(list.id);
                }*/

        // This function allows for the creation of devices
        // The switch case is for the user to select what device they want through a numbered list
        public void createDevice(string type, uint id, string name, string description)
        {

            switch (type)
            {
                case "Irrigation":
                    list.Add(new Irrigation(id, name, description) { });

                    break;

                case "Canopy":
                    list.Add(new Lights(id, name, description) { });
                    break;

                case "PoolHeater":
                    list.Add(new PoolHeater(id, name, description) { });
                    break;

                case "Lights":
                    list.Add(new Canopy(id, name, description) { });
                    break;

                case "Custom":
                    list.Add(new CustomDevice(id, name, description) { });
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("\nInvalid input\n\n");
                    break;

            }
        }


        // This fucntion removes a device at a given index from the list
        // Takes the user input for what device they wish to delete by id
        public void removeDevice(uint id)
        {

            foreach (Devices devices in list) 
            {
                if (id == devices.id) // must be changed from DeviceList to Device and devices.id to however id is accessed in Devices class
                {
                    list.Remove(devices);
                    return;
                }
            }
        }


        // This fucntion saves the text data taken from the Devices module and writes it to a text file generated inside of the solution
        // The text is formated with a idnetifier ahead of it so that it can be determined to be a device
        public void save() // Change DeviceList to Device when integrate
        {

            TextWriter writer = new StreamWriter(path);

            if (!File.Exists(path))
            {
                File.Create(path);
            }

            foreach (Devices element in list)
            {
                writer.WriteLine(element.Save()); // this will call the created save function from Devices
            }
            writer.Flush();
            writer.Close();

        }

        // This takes the list and then reads from the hradcoded file path by line
        // The function splits the file into text that is needed for loading the data
        // The correct text is then sent to the constructor cooresponding to the type of device that is meant for
        public void load() // must change DeviceList to Devices when integrate
        {
            StreamReader file = new StreamReader(path);

            foreach (Devices element in list)
            {
                string text = file.ReadLine();
                string[] words = text.Split(':');

                switch (words[0])
                {
                    case "Irrigation":
                        //     Irrigation i = new Irrigation(words[1]);
                        break;

                    case "Canopy":
                        //     Canopy c = new Canopy(words[1]);
                        break;

                    case "Lights":
                        //     Lights l = new Lights(words[1]);
                        break;

                    case "PoolHeater":
                        //     PoolHeater p = new PoolHeater(words[1]);
                        break;

                    case "Custom":
                        //     CustomDevice cd = new CustomDevice(words[1]);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nError Loading Data\n\n"); // remove later
                        break;
                }

            }
        }

        // This function will call the Sort function built into the list object but instead of using the default compare to
        // It will be overridden allowing for devices to be sorted
        public void sortList() // Change from DeviceList to Device when implment
        {
            list.Sort();
        }
    }

}

