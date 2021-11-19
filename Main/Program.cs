using System;
using CSCN72030_Anemoi;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Sensor blankSensor = new Sensor();
            blankSensor.Display();
            Console.WriteLine();

            int sensorID = 100;
            string sensorNickName = "Precip";
            string sensorLocation = "Cottage";
            Sensor Filledsensor = new Sensor(sensorID, sensorNickName, sensorLocation);
            Filledsensor.Display();
            string saveString = Filledsensor.SaveInfoToString();
            Console.WriteLine();

            sensorID = 95;
            sensorNickName = "Humidity";
            sensorLocation = "Front Porch";
            Humidity newHumiditySensor = new Humidity(sensorID, sensorNickName, sensorLocation);
            newHumiditySensor.Display();
            string newSaveString = newHumiditySensor.SaveInfoToString();
            Console.WriteLine();

            sensorID = 105;
            sensorNickName = "Wind Speed";
            sensorLocation = "Office";
            string sensorType = "Wind Speed";
            Sensor windSpeed = Sensor.CreateSensor(sensorType, sensorID, sensorNickName, sensorLocation);
            windSpeed.Display();

            Console.WriteLine();
            Console.WriteLine("Type: " + windSpeed.GetType().ToString());
            Console.WriteLine();

            string loadString = "Temperature: 115,Temp,Pool,30.05";
            Sensor tempSensor = Sensor.LoadObjectFromString(loadString);

            tempSensor.Display();

            Environment.Exit(0);
        }
    }
}
