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
        private float outputValue;

        public Device Device { get => device; set => device = value; }
        public float OutputValue { get => outputValue; set => outputValue = value; }

        public Action(Device device, float output)
        {
            Device = device;
            OutputValue = output;
        }

        public void Execute()
        {
            Device.Output = OutputValue;
        }
    }
}
