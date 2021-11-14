using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class Irrigation : Devices
    {

        public Irrigation()
        {
            this.deviceID = 0;
            this.name = "";
            this.description = "";
            this.state = false;

        }

        public Irrigation(uint id, string name, string desc, bool state = false)
        {
            this.deviceID = id;
            this.name = name;
            this.description = desc;
            this.state = state;
        }

        public Irrigation(string loadingString)
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

                Console.WriteLine("The irrigation is off.\n");
            }
            else
            {
                Console.WriteLine("The irrigation is already off.\n");

            }


        }

        public override void TurnOn()
        {

            if (this.state)
            {
                this.state = true;

                Console.WriteLine("The irrigation is turned on.\n");
            }
            else
            {
                Console.WriteLine("The irrigation is already turned on.\n");

            }


        }

        public override string Save()
        {

            return ("Irrigation:" + this.deviceID + "|" + this.name + "|" + this.description + "|" + this.state);
        }
    }
}
