using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class ConditionAnalyzer
    {
        private List<ConditionalAction> conditionalActions = new List<ConditionalAction>();
        private DeviceList deviceList;
        private const string filename = "\\ConditionAnalyzer.moi";

        public DeviceList DeviceList { get => deviceList; set => deviceList = value; }

        public bool AddConditionalAction(string name, List<Condition> conditions, List<Action> actions, bool isEnabled = true)
        {
            if (conditionalActions.Exists(x => x.Name == name) || actions.Count == 0)
            {
                return false;
            }
            conditionalActions.Add(new ConditionalAction(name, conditions, actions, isEnabled));
            return true;
        }

        public bool UpdateConditionalAction(string oldName, string newName, List<Condition> conditions, List<Action> actions, bool isEnabled)
        {
            if (!conditionalActions.Exists(x => x.Name == oldName) || conditionalActions.Exists(x => x.Name == newName) || actions.Count == 0)
            {
                return false;
            }
            var conditionalAction = conditionalActions.Where(c => c.Name == oldName).FirstOrDefault();
            var index = conditionalActions.IndexOf(conditionalAction);
            conditionalActions[index] = new ConditionalAction(newName, conditions, actions, isEnabled);
            return true;
        }

        public bool RemoveConditionalAction(string name)
        {
            if (conditionalActions.Exists(x => x.Name == name))
            {
                return false;
            }
            var conditionalAction = conditionalActions.Where(c => c.Name == name).FirstOrDefault();
            var index = conditionalActions.IndexOf(conditionalAction);
            conditionalActions.RemoveAt(index);
            return true;
        }

        public List<string> GetListOfName()
        {
            var listOfNames = new List<string>();
            foreach (var conditionalAction in conditionalActions)
            {
                listOfNames.Add(conditionalAction.Name);
            }
            return listOfNames;
        }

        public List<Condition> GetConditions(string name)
        {
            var conditionalAction = conditionalActions.Where(c => c.Name == name).FirstOrDefault();
            return conditionalAction.Conditions;
        }

        public List<Action> GetActions(string name)
        {
            var conditionalAction = conditionalActions.Where(c => c.Name == name).FirstOrDefault();
            return conditionalAction.Actions;
        }

        public bool IsActionEnabled(string name)
        {
            var conditionalAction = conditionalActions.Where(c => c.Name == name).FirstOrDefault();
            return conditionalAction.IsEnabled;
        }

        public void ProcessAllConditionalActions()
        {
            var currentWeather = WeatherConditions.GetCurrentWeather(DeviceList.Sensors);
            foreach (var conditionalAction in conditionalActions)
            {
                conditionalAction.CheckAndExecute(currentWeather);
            }
        }

        public void Save(string directoryPath)
        {
            //name
            //Sensor,isBetween,high,low
            //Sensor,isBetween,high,low
            //}
            //Device,output
            //Device,output
            //}
            //true
            using (StreamWriter writer = new StreamWriter(directoryPath + filename, false))
            {
                foreach (var conditionalAction in conditionalActions)
                {
                    writer.WriteLine(conditionalAction.Name);

                    foreach (var condition in conditionalAction.Conditions)
                    {
                        if (condition.Sensor != null)
                        {
                            writer.WriteLine(string.Format("{0},{1},{2},{3}",
                                condition.Sensor.GetType().Name, condition.IsBetweenTrigger, condition.LowThreshold, condition.HighThreshold));
                        }
                        else
                        {
                            writer.WriteLine(string.Format("{0},{1}",condition.Weather, condition.IsAboveTrigger));
                        }
                    }
                    writer.WriteLine("}");

                    foreach (var action in conditionalAction.Actions)
                    {
                        writer.WriteLine(string.Format("{0},{1}", action.Device.GetType().Name, action.OutputState));
                    }
                    writer.WriteLine("}");

                    writer.WriteLine(conditionalAction.IsEnabled);
                }
            }
        }

        public static ConditionAnalyzer Load(string directoryPath)
        {
            var conditionAnalyzer = new ConditionAnalyzer();
            //LOAD DEVICELIST SO I CAN GET THE SENSORS/DEVICES INSTANCES

            if (File.Exists(directoryPath + filename))
            {
                using (StreamReader reader = new StreamReader(directoryPath + filename))
                {
                    while (!reader.EndOfStream)
                    {
                        var name = reader.ReadLine();

                        var conditions = new List<Condition>();
                        while (reader.ReadLine() != "}")
                        {
                            var line = reader.ReadLine();
                            var parts = line.Split(',');
                            if (parts.Length == 4)
                            {
                                conditions.Add(new Condition(conditionAnalyzer.DeviceList.Sensors[0], //need search function
                                    Convert.ToBoolean(parts[1]), Convert.ToSingle(parts[2]), Convert.ToSingle(parts[3])));
                            }
                            else
                            {
                                conditions.Add(new Condition((Weather)Enum.Parse(typeof(Weather), parts[0]), Convert.ToBoolean(parts[1])));
                            }
                        }

                        var actions = new List<Action>();
                        while (reader.ReadLine() != "}")
                        {
                            var line = reader.ReadLine();
                            var parts = line.Split(',');
                            actions.Add(new Action(conditionAnalyzer.DeviceList.Devices[0], Convert.ToBoolean(parts[1])));  //need search function
                        }

                        var isEnabled = Convert.ToBoolean(reader.ReadLine());

                        conditionAnalyzer.AddConditionalAction(name, conditions, actions, isEnabled);
                    }
                }
            }
            return conditionAnalyzer;
        }

        private class ConditionalAction
        {
            private string name;
            private List<Condition> conditions;
            private List<Action> actions;
            private bool isEnabled;

            public string Name { get => name; set => name = value; }
            public List<Condition> Conditions { get => conditions; set => conditions = value; }
            public List<Action> Actions { get => actions; set => actions = value; }
            public bool IsEnabled { get => isEnabled; set => isEnabled = value; }

            public ConditionalAction(string name, List<Condition> conditions, List<Action> actions, bool isEnabled = true)
            {
                Name = name;
                Conditions = conditions;
                Actions = actions;
                IsEnabled = IsEnabled;
            }

            public void CheckAndExecute(Weather currentWeather)
            {
                if (isEnabled)
                {
                    var allConditionsMet = true;
                    foreach (var condition in Conditions)
                    {
                        if (!condition.Check(currentWeather))
                        {
                            allConditionsMet = false;
                        }
                    }
                    if (allConditionsMet)
                    {
                        foreach (var action in Actions)
                        {
                            action.Execute();
                        }
                    }
                }
            }
        }
    }
}
