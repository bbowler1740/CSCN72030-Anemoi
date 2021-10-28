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

        public Condition(Sensor sensor, bool isBetweenTrigger, float lowThreshold, float highThreshold)
        {
            Sensor = sensor;
            IsBetweenTrigger = isBetweenTrigger;
            LowThreshold = lowThreshold;
            HighThreshold = highThreshold;
        }

        public Condition(Weather weather, bool isAboveTrigger)
        {
            Weather = weather;
            IsAboveTrigger = isAboveTrigger;

            Sensor = null;
        }

        public bool Check(Weather currentWeather)
        {
            if (Sensor != null)
            {
                if (Sensor.Data > LowThreshold && Sensor.Data < HighThreshold)
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
