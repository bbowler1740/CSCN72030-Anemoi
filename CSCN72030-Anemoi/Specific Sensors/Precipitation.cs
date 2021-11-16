﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class Precipitation : Sensor
    {

        public Precipitation(int sensorID, string sensorNickName, string sensorLocation) //Parameterized for brand new sensor.
        {
            this.SensorID = sensorID;
            this.SensorNickName = sensorNickName;
            this.SensorLocation = sensorLocation;
        }
        public Precipitation(int sensorID, string sensorNickName, string sensorLocation, float sensorData) //Parameterized for creation from load data.
        {
            this.SensorID = sensorID;
            this.SensorNickName = sensorNickName;
            this.SensorLocation = sensorLocation;
            this.SensorData = sensorData;
        }

    }
}
