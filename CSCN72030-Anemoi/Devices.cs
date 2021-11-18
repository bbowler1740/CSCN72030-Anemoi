using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    abstract public class Devices
    {
        protected uint deviceID;
        protected string name;
        protected string description;
        protected bool state;   //true = on      false = off

        /// <summary>
        /// Saving method which returns the string. 
        /// </summary>
        /// <returns>A formatted string of device data.</returns>
        public abstract string Save(); 

        /// <summary>
        /// Retrives the identification unsigned integer of the device. 
        /// </summary>
        /// <returns>Unsigned integer of the ID.</returns>
        public uint GetDeviceID()
        {
            return this.deviceID;
        }

        /// <summary>
        /// Set the device identification. 
        /// </summary>
        /// <param name="id">Uses an unsigned integer.</param>
        public void SetDeviceID(uint id)
        {
            this.deviceID = id;
        }

        /// <summary>
        /// Retreives the name of the device. 
        /// </summary>
        /// <returns>Returns a string name of the device.</returns>
        public string GetName()
        {
            return this.name;

        }

        /// <summary>
        /// Sets the name of the device. 
        /// </summary>
        /// <param name="name">Uses a string name. Must not include | and must be unique.</param>
        public void SetName(string name)
        {
            this.name = name;

        }

        /// <summary>
        /// Retrives the description of a device. 
        /// </summary>
        /// <returns>The description as a string.</returns>
        public string GetDescription()
        {
            return this.description;

        }

        /// <summary>
        /// Sets the description me of the device. 
        /// </summary>
        /// <param name="name">Uses a string description. Must not include.</param>
        public void SetDescription(string desc)
        {
            this.description = desc;

        }

        /// <summary>
        /// Retrives the state of the device. (True = On, False = Off)
        /// </summary>
        /// <returns>A boolean of the current device state.</returns>
        public bool GetState()
        {
            return this.state;

        }

        /// <summary>
        /// Sets the state of a device.
        /// </summary>
        /// <param name="state">A boolean to change the state.</param>
        protected void SetState(bool state)
        {
            this.state = state;

        }

        /// <summary>
        /// Turning on the device. Changes the state to true. 
        /// </summary>
        public abstract void TurnOn();

        /// <summary>
        /// Turning off the device. Changes the state to false. 
        /// </summary>
        public abstract void TurnOff();

    }
}
