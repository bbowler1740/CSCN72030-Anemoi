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
        int idCount = 0;

        // Gets the list for use in other modules
        public List<Devices> getDeviceList()
        {
            return list;
        }

        public Devices search(int id)
        {
            foreach (Devices devices in list)
            {
                if (id == devices.GetDeviceID()) // must be changed from DeviceList to Device and devices.id to however id is accessed in Devices class
                {
                    return devices;
                }
            }
        }

        // This function allows for the creation of devices
        // The switch case is for the user to select what device they want through a numbered list
        public void createDevice(string type, string name, string description)
        {
            switch (type)
            {
                case "Irrigation":
                    list.Add(new Irrigation(idCount, name, description));
                    idCount++;
                    break;

                case "Canopy":
                    list.Add(new Lights(idCount, name, description));
                    idCount++;
                    break;

                case "PoolHeater":
                    list.Add(new PoolHeater(idCount, name, description));
                    idCount++;
                    break;

                case "Lights":
                    list.Add(new Canopy(idCount, name, description));
                    idCount++;
                    break;

                case "Custom":
                    list.Add(new CustomDevice(idCount, name, description));
                    idCount++;
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
                if (id == devices.GetDeviceID()) // must be changed from DeviceList to Device and devices.id to however id is accessed in Devices class
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
            foreach (Devices idcheck in list)
            {
                if (idcheck.getDeviceID() > idCount)
                {
                    idCount = idcheck.getDeviceID();
                }
            }


            public void sortList()
            {
                list = list.OrderBy(d => d.GetDeviceID()).ToList();
            }
        }

    }

}