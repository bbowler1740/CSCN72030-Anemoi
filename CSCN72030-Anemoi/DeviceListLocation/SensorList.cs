using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSCN72030_Anemoi
{
    public class SensorList : aList
    {
        public string name = @"\SensorList.txt";
        List<Sensor> list = new List<Sensor>(); // Must change Sensor List to Sensors when intergrate
        int idCount = 1;

        public List<Sensor> getSensorList()
        {
            return list;
        }

        public List<Sensor> getCustomSensorList()
        {

            List<Sensor> customList = new List<Sensor>();

            foreach (Sensor sensor in list)
            {

                if (sensor.GetType().Name.ToString() == "Custom")
                {

                    customList.Add(sensor);

                }

            }

            return customList;

        }

        // This function allows for the creation of sensors
        public void createSensor(string type, string name, string location, float data)
        {
            Sensor s = Sensor.CreateSensor(type, idCount, name, location, data);
            idCount++;
            list.Add(s);
        }

        public void createSensor(string type, string name, string location)
        {
            Sensor s = Sensor.CreateSensor(type, idCount, name, location);
            idCount++;
            list.Add(s);
        }

        public Sensor search(int id)
        {
            foreach (Sensor sensor in list)
            {
                if (id == sensor.SensorID) // must be changed from SensorList to sensor and sensor.id to however id is accessed in sensors class
                {
                    return sensor;
                }
            }

            return null;
        }

        // This fucntion removes a sensor at a given index from the list
        // Takes the user input for what sensor they wish to delete by id
        public void removeSensor(int id)
        {

            foreach (Sensor sensor in list)
            {
                if (id == sensor.SensorID) // must be changed from SensorList to sensor and sensor.id to however id is accessed in sensors class
                {
                    list.Remove(sensor);
                    return;
                }
            }
        }


        // This fucntion saves the text data taken from the sensors module and writes it to a text file generated inside of the solution
        // The text is formated with a idnetifier ahead of it so that it can be determined to be a sensor
        public void save(string path)
        {
            string SensorListPath = path + name;
            TextWriter writer = new StreamWriter(SensorListPath);

            if (!File.Exists(SensorListPath))
            {
                File.Create(SensorListPath);
            }

            foreach (Sensor element in list)
            {
                writer.WriteLine(element.SaveInfoToString()); // this will call the created save function from sensor
            }
            writer.Flush();
            writer.Close();

        }

        // This takes the list and then reads from the hradcoded file path by line
        // The function splits the file into text that is needed for loading the data
        // The correct text is then sent to the constructor cooresponding to the type of device that is meant for
        public void load(string path)
        {
            string SensorListPath = path + name;

            string[] fileLines = File.ReadAllLines(SensorListPath);

            foreach (string lines in fileLines)
            {
                list.Add(Sensor.CreateObjectFromString(lines));
            }

            foreach (Sensor sensor in list)
            {
                if (sensor.SensorID >= idCount)
                {
                    idCount = sensor.SensorID + 1;
                }
            }
        }

        public void sortList()
        {
            list = list.OrderBy(s => s.SensorID).ToList();
        }
    }
}