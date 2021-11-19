using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSCN72030_Anemoi.DeviceListLocation
{
    class Location
    {
        public static string locationFile = @"e:\LocationFile";

        // This function creates a directory at the specified name by the user
        public void createLocation(string name)
        {
            string folderName = @"e:\" + name;
            if (Directory.Exists(folderName))
            {
                return;
            }

            TextWriter writer = new StreamWriter(locationFile);
            writer.WriteLine(name);
            Directory.CreateDirectory(folderName);

            // must pass down the directory folderName to CA -> DLL
        }

        // This function deletes a directory at the specified name by the user
        public void deleteLocation(string name)
        {
            string folderName = @"e:\" + name;
            if (Directory.Exists(folderName))
            {
                Directory.Delete(folderName);
                return;
            }
            else
            {
                // some sort of error message saying location doesnt exist
            } 
        }

        public static string[] displayLocation()
        {
            string[] lines = File.ReadAllLines(locationFile);
            return lines;
        }

        public void selectLocation(string name)
        { 
            string folderName = @"e:\" + name; // where name is the user input for the name
            if (Directory.Exists(folderName))
            {
                // must pass down the directory folderName to CA -> DLL
            }
            else
            {
                // some sort of error message saying location doesnt exist
            }
        }
    }
}
