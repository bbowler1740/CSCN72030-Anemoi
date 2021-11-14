using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class CustomDevice : Devices
    {

        public CustomDevice()
        {
            this.deviceID = 0;
            this.name = "";
            this.description = "";
            this.state = false;

        }

        public CustomDevice(uint id, string name, string desc, bool state = false)
        {
            this.deviceID = id;
            this.name = name;
            this.description = desc;
            this.state = state;
        }

        public  CustomDevice(string loadingString)
        {
            string[] parsedString = loadingString.Split('|');

            
            uint.TryParse(parsedString[0], out this.deviceID);

            this.name = parsedString[1];
            this.description = parsedString[2];
            this.state = Convert.ToBoolean(parsedString[3]);

        }

        public override void TurnOff()
        {
            if (this.state)
            {
                this.state = false;

                Console.WriteLine(this.name + " is turned off.\n");
            }
            else
            {
                Console.WriteLine(this.name + " is already turned off.\n");

            }


        }

        public override void TurnOn()
        {

            if (this.state)
            {
                this.state = true;

                Console.WriteLine(this.name + " is turned on.\n");
            }
            else
            {
                Console.WriteLine(this.name + " is already turned on.\n");

            }


        }

        public override string Save()
        {
                 
            return ("CustomDevice:"+ this.deviceID + "|" + this.name + "|" + this.description + "|" + this.state);
        }
    
    }
}
