/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSCN72030_Anemoi
{
    class SensorList : aList
    {
        public string path = @"SensorList.txt";
        List<Sensors> list = new List<Sensors>(); // Must change Sensor List to Sensors when intergrate
        int idCount = 0;

        public List<Sensors> getDeviceList()
        {
            return list;
        }

        // This function allows for the creation of sensors
        public void createDevice(string type, string name, string location, float data)
        {
            Sensors s = Sensors.createSensors(type, idCount, name, location, data);
            idCount++;
            list.Add(s);
        }

        public Sensors search(int id)
        {
            foreach (Sensors sensors in list)
            {
                if (id == sensors.getSensorID()) // must be changed from SensorList to sensor and sensor.id to however id is accessed in sensors class
                {
                    return sensors;
                }
            }
        }

        // This fucntion removes a sensor at a given index from the list
        // Takes the user input for what sensor they wish to delete by id
        public void removeSensor(uint id)
        {

            foreach (Sensors sensors in list)
            {
                if (id == sensors.id) // must be changed from SensorList to sensor and sensor.id to however id is accessed in sensors class
                {
                    list.Remove(sensors);
                    return;
                }
            }
        }


        // This fucntion saves the text data taken from the sensors module and writes it to a text file generated inside of the solution
        // The text is formated with a idnetifier ahead of it so that it can be determined to be a sensor
        public void save()
        {

            TextWriter writer = new StreamWriter(path);

            if (!File.Exists(path))
            {
                File.Create(path);
            }

            foreach (Sensors element in list)
            {
                writer.WriteLine(element.Save()); // this will call the created save function from sensor
            }
            writer.Flush();
            writer.Close();

        }

        // This takes the list and then reads from the hradcoded file path by line
        // The function splits the file into text that is needed for loading the data
        // The correct text is then sent to the constructor cooresponding to the type of device that is meant for
        public void load() 
        {
            StreamReader file = new StreamReader(path);

            foreach (Sensors element in list)
            {
                string text = file.ReadLine();
                list.Add(new Sensors(text));
            }
            foreach (Sensors idcheck in list)
            {
                if (idcheck.getDeviceID() > idCount)
                {
                    idCount = idcheck.getDeviceID();
                }
            }
        }

        public void sortList()
        {
            list = list.OrderBy(s => s.GetDeviceID()).ToList();
        }
    }
}

*/