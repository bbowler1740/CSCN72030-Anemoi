using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    public class ConditionalActionData
    {
        public int Id { get; set; }
        public List<Condition> Conditions { get; set; }
        public List<Action> Actions { get; set; }
        public bool Status { get; set; }

        public string ConditionsString
        {
            get
            {
                var conditionsString = "";
                foreach (var condition in Conditions)
                {

                    if (condition.IsBetweenTrigger)
                    {
                        conditionsString += string.Format("{0} < {1} < {2} ",
                            condition.LowThreshold.ToString(),
                            condition.Sensor.GetType().Name,
                            condition.HighThreshold.ToString()
                            );
                    }
                    else
                    {
                        conditionsString += string.Format("{1} < {0} or {1} > {2} ",
                            condition.LowThreshold.ToString(),
                            condition.Sensor.GetType().Name,
                            condition.HighThreshold.ToString()
                            );
                    }
                }
                return conditionsString;
            }
        }
        public string ActionsString 
        { 
            get
            {
                var actionsString = "";
                foreach (var action in Actions)
                {
                    actionsString += string.Format("{0} = {1} ", action.Device.GetType().Name, action.OutputState ? "On" : "Off");
                }
                return actionsString;
            } 
        }
        public string StatusString { get => Status ? "Enabled" : "Disabled"; }
    }
    
    public class DeviceData
    {
        public Devices Device { get; set; }
        public string Name { get; set; }
        public bool State
        {
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
