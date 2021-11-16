using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    public class Sensor //Generalized class. See "Specific Sensors" folder for specializations.
    {
        protected int SensorID { get; set; }
        protected string SensorNickName { get; set; }
        protected string SensorLocation { get; set; }
        protected float SensorData { get; set; } //Continuosly overridden when reading from file. Real-time updating is ideal, but perhaps consider 5 second intervals.
        public Sensor()
        {
            this.SensorID = -1;
            this.SensorNickName = "Unset";
            this.SensorLocation = "Unset";
        }
        public Sensor(int sensorID, string sensorNickName, string sensorLocation) //Parameterized for brand new sensor.
        {
            this.SensorID = sensorID;
            this.SensorNickName = sensorNickName;
            this.SensorLocation = sensorLocation;
        }
        public Sensor(int sensorID, string sensorNickName, string sensorLocation, float sensorData) //Parameterized for creation from load data.
        {
            this.SensorID = sensorID;
            this.SensorNickName = sensorNickName;
            this.SensorLocation = sensorLocation;
            this.SensorData = sensorData;
        }

        //Overloaded twice. This version with 4 parameters is intended for creating an object previously saved with data already set.
        public static Sensor CreateSensor(string type, int id, string nickname, string location, float data)
        {
            switch (type)
            {
                case "Temperature":
                    Temperature temperatureSensor = new Temperature(id, nickname, location, data);
                    return temperatureSensor;
                    
                case "Precipitation":
                    Precipitation precipitationSensor = new Precipitation(id, nickname, location, data);
                    return precipitationSensor;
                    
                case "Air Pressure":
                    AirPressure airPressureSensor = new AirPressure(id, nickname, location, data);
                    return airPressureSensor;
                
                case "Sunlight":
                    Sunlight sunlightSensor = new Sunlight(id, nickname, location, data);
                    return sunlightSensor;
                    
                case "Ground Moisture":
                    GroundMoisture moistureSensor = new GroundMoisture(id, nickname, location, data);
                    return moistureSensor;
                    
                case "Humidity":
                    Humidity humiditySensor = new Humidity(id, nickname, location, data);
                    return humiditySensor;
                    
                case "Wind Speed":
                    WindSpeed windSpeedSensor = new WindSpeed(id, nickname, location, data);
                    return windSpeedSensor;
                    
                case "Wind Direction":
                    WindDirection windDirectionSensor = new WindDirection(id, nickname, location, data);
                    return windDirectionSensor;
                    
                case "Custom":
                    Custom customSensor = new Custom(id, nickname, location, data);
                    return customSensor;

                //Discuss with group about how cases should fall through to 'default', where is 'type' error checking occuring?
                default:
                    Custom customSensorTwo = new Custom(id, nickname, location, data);
                    return customSensorTwo;
            }
        }

        //Overloaded twice. This version with 3 parameters is intended for creating a new object.
        public static Sensor CreateSensor(string type, int id, string nickname, string location)
        {
            switch (type)
            {
                case "Temperature":
                    Temperature temperatureSensor = new Temperature(id, nickname, location);
                    return temperatureSensor;

                case "Precipitation":
                    Precipitation precipitationSensor = new Precipitation(id, nickname, location);
                    return precipitationSensor;

                case "Air Pressure":
                    AirPressure airPressureSensor = new AirPressure(id, nickname, location);
                    return airPressureSensor;

                case "Sunlight":
                    Sunlight sunlightSensor = new Sunlight(id, nickname, location);
                    return sunlightSensor;

                case "Ground Moisture":
                    GroundMoisture moistureSensor = new GroundMoisture(id, nickname, location);
                    return moistureSensor;

                case "Humidity":
                    Humidity humiditySensor = new Humidity(id, nickname, location);
                    return humiditySensor;

                case "Wind Speed":
                    WindSpeed windSpeedSensor = new WindSpeed(id, nickname, location);
                    return windSpeedSensor;

                case "Wind Direction":
                    WindDirection windDirectionSensor = new WindDirection(id, nickname, location);
                    return windDirectionSensor;

                case "Custom":
                    Custom customSensor = new Custom(id, nickname, location);
                    return customSensor;

                //Discuss with group about how cases should fall through to 'default', where is 'type' error checking occuring?
                default:
                    Custom customSensorTwo = new Custom(id, nickname, location);
                    return customSensorTwo;
            }
        }

        public void Display()
        {
            //Needs to be converted to interact with a UI, not console.
            Console.WriteLine("Sensor ID: {0}", this.SensorID);
            Console.WriteLine("Sensor Nickname: {0}", this.SensorNickName);
            Console.WriteLine("Sensor Location: {0}", this.SensorLocation);
            return;
        }

        public void ReadScenarioDataFromFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                //Display message about lack of file existence.
                return;
            }

            string[] weatherData = File.ReadAllLines(fileName);

            int numDataPoints = weatherData.Length;
            var random = new Random();
            this.SensorData = float.Parse(weatherData[random.Next(numDataPoints)]);
        }

        public string SaveInfoToString()
        {
            string saveInfo;

            saveInfo = this.GetType().ToString() + ':' + this.SensorID.ToString() + ',' + this.SensorNickName + ',' + this.SensorLocation + ',' + this.SensorData.ToString();

            return saveInfo;
        }

        /// <summary>
        /// This function manipulates a string of internal object data to instantiate a new object.
        /// </summary>
        /// <param name="loadDataAsString"> A string of internal object data in the form of: {[Type]: [ID], [nickname], [location], [data]}. </param>
        /// <returns> A new instance of a specific sensor decided by sensor type contained in the data string. </returns>
        public static Sensor CreateObjectFromString(string loadDataAsString)
        {
            //Agreed upon delimiter chars.
            char[] delimiterChars = { ':', ',' };

            //Split each string into its own index in a string array.
            string[] objectDataAsStrings = loadDataAsString.Split(delimiterChars);

            //Assign each individual word to its data type.
            string type = objectDataAsStrings[0];
            int id = Int32.Parse(objectDataAsStrings[1]);
            string nickname = objectDataAsStrings[2];
            string location = objectDataAsStrings[3];
            float data = float.Parse(objectDataAsStrings[4]);

            //Bundle everything and create a sensor.
            Sensor newSensor = CreateSensor(type, id, nickname, location, data);

            return newSensor;
        }
    }
}
