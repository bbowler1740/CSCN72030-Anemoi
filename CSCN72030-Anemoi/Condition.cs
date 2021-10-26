using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class Condition
    {
        private Sensor sensor;
        private bool isBetween;
        private int lowThreshold;
        private int highThreshold;

        public Sensor Sensor { get => sensor; set => sensor = value; }
        public bool IsBetween { get => isBetween; set => isBetween = value; }
        public int LowThreshold { get => lowThreshold; set => lowThreshold = value; }
        public int HighThreshold { get => highThreshold; set => highThreshold = value; }

        //functions: Check(), constructor(Sensor, isBetween, high, low)
    }
}
