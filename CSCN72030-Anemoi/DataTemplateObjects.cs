using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    public class ConditionalActionData
    {
        public string Conditions { get; set; }
        public string Actions { get; set; }
        public string Status { get; set; }
    }
    
    public class DeviceData
    {
        public Devices Device { get; set; }
        public string Name { get; set; }
        public bool State { 
            get { 
                return Device.GetState(); 
            } 
            set {
                if (value)
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
}
