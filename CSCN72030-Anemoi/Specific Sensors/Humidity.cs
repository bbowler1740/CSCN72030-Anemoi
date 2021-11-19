using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class Humidity : Sensor
    {

        public Humidity(int sensorID, string sensorNickName, string sensorLocation) //Parameterized for brand new sensor.
        {
            this.SensorID = sensorID;
            this.SensorNickName = sensorNickName;
            this.SensorLocation = sensorLocation;
        }
        public Humidity(int sensorID, string sensorNickName, string sensorLocation, float sensorData) //Parameterized for creation from load data.
        {
            this.SensorID = sensorID;
            this.SensorNickName = sensorNickName;
            this.SensorLocation = sensorLocation;
            this.SensorData = sensorData;
        }

        /// <summary>
        /// A method to allow a Sensor object to pull data from an external file.
        /// </summary>
        /// <param name="fileName"> The filename that the Sensor object should pull its data from.</param>
        public override void ReadScenarioDataFromFile()
        {
            string fullFilePath = weatherScenarioPath + "\\" + "humidity.txt";

            if (!File.Exists(fullFilePath))
            {
                //Display message about lack of file existence.
                return;
            }

            string[] weatherData = File.ReadAllLines(fullFilePath);

            int numDataPoints = weatherData.Length;
            var random = new Random();
            this.SensorData = float.Parse(weatherData[random.Next(numDataPoints)]);
        }

    }
}
