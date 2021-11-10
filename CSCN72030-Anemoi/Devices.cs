using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    abstract class Devices
    {
        protected uint deviceID;
        protected string name;
        protected string description;
        protected bool state;   //true = on      false = off


        public string Save()
        {
            return (this.deviceID + "|" + this.name + "|" + this.description + "|" + this.state);
        }

        public uint GetDeviceID()
        {
            return this.deviceID;
        }

        public void SetDeviceID(uint id)
        {
            this.deviceID = id;
        }

        public string GetName()
        {
            return this.name;

        }

        public void SetName(string name)
        {
            this.name = name;

        }

        public string GetDescription()
        {
            return this.description;

        }

        public void SetDescription(string desc)
        {
            this.description = desc;

        }

        public bool GetState()
        {
            return this.state;

        }

        protected void SetState(bool state)
        {
            this.state = state;

        }

        public abstract void TurnOn();


        public abstract void TurnOff();

    }
}
