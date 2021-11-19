using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class Condition
    {
        private Sensor sensor = null;
        private bool isBetweenTrigger;
        private float lowThreshold;
        private float highThreshold;
        private Weather weather;
        private bool isAboveTrigger;

        public Sensor Sensor { get => sensor; set => sensor = value; }
        public bool IsBetweenTrigger { get => isBetweenTrigger; set => isBetweenTrigger = value; }
        public float LowThreshold { get => lowThreshold; set => lowThreshold = value; }
        public float HighThreshold { get => highThreshold; set => highThreshold = value; }
        public Weather Weather { get => weather; set => weather = value; }
        public bool IsAboveTrigger { get => isAboveTrigger; set => isAboveTrigger = value; }

        /// <summary>
        /// Parameterized constructor for a condition using a sensor
        /// </summary>
        /// <param name="sensor">the sensor providing the data to check</param>
        /// <param name="isBetweenTrigger">indicate if sensor data reading between thresholds means the condition is met (explicitly between)</param>
        /// <param name="lowThreshold">the lower bounds of the range</param>
        /// <param name="highThreshold">the upper bounds of the range</param>
        public Condition(Sensor sensor, bool isBetweenTrigger, float lowThreshold, float highThreshold)
        {
            Sensor = sensor;
            IsBetweenTrigger = isBetweenTrigger;
            LowThreshold = lowThreshold;
            HighThreshold = highThreshold;
        }

        /// <summary>
        /// Parameterized constructor for a condition using Weather enum
        /// </summary>
        /// <param name="weather">the weather condition to compare to</param>
        /// <param name="isAboveTrigger">indicate if the weather condition and anything worse means the condition is met (inclusive)</param>
        public Condition(Weather weather, bool isAboveTrigger)
        {
            Weather = weather;
            IsAboveTrigger = isAboveTrigger;

            Sensor = null;
        }

        /// <summary>
        /// Compare the sensor data or currentWeather to condition fields
        /// </summary>
        /// <param name="currentWeather">the current weather enum that is used against the set weather of the condition (if Sensor is condition, this is not used)</param>
        /// <returns>true if condition has been met</returns>
        public bool Check(Weather currentWeather)
        {
            if (Sensor != null)
            {
                if (Sensor.SensorData > LowThreshold && Sensor.SensorData < HighThreshold)
                {
                    return IsBetweenTrigger;
                }
                return !IsBetweenTrigger;
            }

            if (IsAboveTrigger)
            {
                if (currentWeather >= Weather)
                {
                    return true;
                }
                return false;
            }
            if (currentWeather <= Weather)
            {
                return true;
            }
            return false;
        }
    }
}
