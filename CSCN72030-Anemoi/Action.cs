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

        /// <summary>
        /// Parameterized constructor for action taken on a device
        /// </summary>
        /// <param name="device">the device to be controlled by the action</param>
        /// <param name="output">the output state the device will set to</param>
        public Action(Device device, bool output)
        {
            Device = device;
            OutputState = output;
        }

        /// <summary>
        /// Set the device state to the desired state
        /// </summary>
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
