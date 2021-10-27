using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class Action
    {
        private Device device;
        private int outputValue;

        public Device Device { get => device; set => device = value; }
        public int OutputValue { get => outputValue; set => outputValue = value; }

        //functions: Execute(), constructor(Device, output)
    }
}
