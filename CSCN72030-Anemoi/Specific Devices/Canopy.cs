using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class Canopy : Devices
    {

        public Canopy()
        {
            this.deviceID = 0;
            this.name = "";
            this.description = "";
            this.state = false;

        }

        public Canopy(uint id, string name, string desc, bool state = false)
        {
            this.deviceID = id;
            this.name = name;
            this.description = desc;
            this.state = state;
        }

        public Canopy(string loadingString)
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

                Console.WriteLine("The canopy is closed.\n");
            }
            else
            {
                Console.WriteLine("The canopy is already closed.\n");

            }


        }

        public override void TurnOn()
        {

            if (this.state)
            {
                this.state = true;

                Console.WriteLine("The canopy is open.\n");
            }
            else
            {
                Console.WriteLine("The canopy is already open.\n");

            }


        }
    }
}
