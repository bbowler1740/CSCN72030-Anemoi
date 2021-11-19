using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class CustomDevice : Devices
    {
        /// <summary>
        /// Default Constuctor for CustomDevice
        /// </summary>
        public CustomDevice()
        {
            this.deviceID = 0;
            this.name = "";
            this.description = "";
            this.state = false;

        }

        /// <summary>
        /// Paramerterized constructor for CustomDevice
        /// </summary>
        /// <param name="id">The identification number of the device. Must be unique.</param>
        /// <param name="name">The name of the device. Must not conain | and must be unique.</param>
        /// <param name="desc">The description of the device. Must not contain |.</param>
        /// <param name="state">State of the device, and is defaulted to false (Off).</param>
        public CustomDevice(uint id, string name, string desc, bool state = false)
        {
            this.deviceID = id;
            this.name = stripString(name);
            this.description = stripString(desc);
            this.state = state;
        }

        /// <summary>
        /// The loading constructor. 
        /// </summary>
        /// <param name="loadingString">The formated string of data.</param>
        public  CustomDevice(string loadingString)
        {
            string[] parsedString = loadingString.Split('|');

            
            uint.TryParse(parsedString[0], out this.deviceID);

            this.name = parsedString[1];
            this.description = parsedString[2];
            this.state = Convert.ToBoolean(parsedString[3]);

        }

        /// <summary>
        /// Turning off a device. Changes the state to false.  
        /// </summary>
        public override void TurnOff()
        {
            if (this.state)
            {
                this.state = false;

                Debug.WriteLine(this.name + " is turned off.\n");
            }
            else
            {
                Debug.WriteLine(this.name + " is already turned off.\n");

            }


        }

        /// <summary>
        /// Turning on the device. Changes the state to true. 
        /// </summary>
        public override void TurnOn()
        {

            if (!this.state)
            {
                this.state = true;

                Debug.WriteLine(this.name + " is turned on.\n");
            }
            else
            {
                Debug.WriteLine(this.name + " is already turned on.\n");

            }


        }

        /// <summary>
        /// The saving functonality which, returns a formatted string. 
        /// </summary>
        /// <returns>Formatted string of data. </returns>
        public override string Save()
        {
                 
            return ("CustomDevice:"+ this.deviceID + "|" + this.name + "|" + this.description + "|" + this.state);
        }
    
    }
}
