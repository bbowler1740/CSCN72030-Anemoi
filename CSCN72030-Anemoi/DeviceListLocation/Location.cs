using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;

namespace CSCN72030_Anemoi
{
    public class Location
    {
        private ConditionAnalyzer conditionAnalyzer = new ConditionAnalyzer();

        public string locationName;

        public ConditionAnalyzer getCA()
        {
            return conditionAnalyzer;
        }

        // This function creates a directory at the specified name by the user
        public void createLocation(string name)
        {
            string folderName = ApplicationData.Current.LocalFolder.Path + @"\" + name;
            if (Directory.Exists(folderName))
            {
                return;
            }
            locationName = name;
            Directory.CreateDirectory(folderName);
        }

        // This function deletes a directory at the specified name by the user
        public void deleteLocation(string name)
        {
            string folderName = ApplicationData.Current.LocalFolder.Path + @"\" + name;
            if (Directory.Exists(folderName))
            {
                Directory.Delete(folderName, true);
                return;
            }
            else
            {
                // some sort of error message saying location doesnt exist
            } 
        }

        public static string[] displayLocation()
        {
            DirectoryInfo[] lines = new DirectoryInfo(ApplicationData.Current.LocalFolder.Path).GetDirectories();
            int len = lines.Length;
            string[] arr = new string[len];
            int i = 0;
            foreach (DirectoryInfo info in lines)
            {
                arr[i] = info.Name; 
                i++;
            }

            return arr;
        }

        public void selectLocation(string name)
        {
            string folderName = ApplicationData.Current.LocalFolder.Path + @"\" + name; // where name is the user input for the name
            if (Directory.Exists(folderName))
            {
                conditionAnalyzer.Load(folderName);
                locationName = name;
            }
            else
            {
                // some sort of error message saying location doesnt exist
            }
        }

        public void save()
        {
            string folderName = ApplicationData.Current.LocalFolder.Path + @"\" + locationName;
            if (!Directory.Exists(folderName))
            {
                return;
            }
            conditionAnalyzer.Save(folderName);
        }
    }
}
