using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class Sensors //Specific sensor types: temperature, precipitation, air pressure, sunlight, ground, humidity, wind direction, wind speed
    {
        public int SensorID { get; private set; } = -1;
        public string SensorNickName { get; set; } = "Unset";
        public string SensorLocation { get; set; } = "Unset";
        public Sensors(int sensorID, string sensorNickName, string sensorLocation) //Parameterized
        {
            this.SensorID = sensorID;
            this.SensorNickName = sensorNickName;
            this.SensorLocation = sensorLocation;
        }
        public void Print()
        {
            Console.WriteLine("Sensor ID: {0}", this.SensorID);
            Console.WriteLine("Sensor Nickname: {0}", this.SensorNickName);
            Console.WriteLine("Sensor Location: {0}", this.SensorLocation);
            return;
        }
        public void Save(string fileName)
        {

            //How do we want to save? Binary, XML, JSON?

        }
        public Sensors Load()
        {

            //How do we want to load? Binary, XML, JSON?

        }


    }
}
