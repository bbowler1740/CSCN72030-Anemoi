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
        private bool outputState;

        public Device Device { get => device; set => device = value; }
        public bool OutputState { get => outputState; set => outputState = value; }

        public Action(Device device, bool output)
        {
            Device = device;
            OutputState = output;
        }

        public void Execute()
        {
            if (outputState)
            {
                Device.TurnOn();
            }
            else
            {
                Device.TurnOff();
            }
        }
    }
}
