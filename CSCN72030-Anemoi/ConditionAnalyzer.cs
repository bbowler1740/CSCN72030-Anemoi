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
        private SensorList sensorList = new SensorList();
        private DeviceList deviceList = new DeviceList();
        private const string filename = "\\ConditionAnalyzer.moi";

        public DeviceList DeviceList { get => deviceList; set => deviceList = value; }
        public SensorList SensorList { get => sensorList; set => sensorList = value; }

        /// <summary>
        /// Creates a new conditional action
        /// </summary>
        /// <param name="conditions">all conditions that must be met</param>
        /// <param name="actions">all actions taken when conditions are met</param>
        /// <param name="isEnabled">indicate whether this conditional action is enabled, default true</param>
        /// <returns>true when addition was successful</returns>
        public bool AddConditionalAction(List<Condition> conditions, List<Action> actions, bool isEnabled = true)
        {
            if (conditions.Count == 0 || actions.Count == 0)
            {
                return false;
            }
            conditionalActions.Add(new ConditionalAction(conditions, actions, isEnabled));
            return true;
        }

        /// <summary>
        /// Updates an existing conditional action
        /// </summary>
        /// <param name="id">the id of the conditional action to find it within collection</param>
        /// <param name="conditions">all conditions that must be met</param>
        /// <param name="actions">all actions taken when conditions are met</param>
        /// <param name="isEnabled">indicate whether this conditional action is enabled</param>
        /// <returns>true when update was successful</returns>
        public bool UpdateConditionalAction(int id, List<Condition> conditions, List<Action> actions, bool isEnabled)
        {
            if (!conditionalActions.Exists(x => x.Id == id) || conditions.Count == 0 || actions.Count == 0)
            {
                return false;
            }
            var conditionalAction = conditionalActions.Where(c => c.Id == id).FirstOrDefault();
            var index = conditionalActions.IndexOf(conditionalAction);
            conditionalActions[index].Conditions = conditions;
            conditionalActions[index].Actions = actions;
            conditionalActions[index].IsEnabled = isEnabled;
            return true;
        }

        /// <summary>
        /// Delete an existing conditional action from managed list
        /// </summary>
        /// <param name="id">id of the conditional action to remove</param>
        /// <returns>true when removal was successful</returns>
        public bool RemoveConditionalAction(int id)
        {
            if (conditionalActions.Exists(x => x.Id == id))
            {
                return false;
            }
            var conditionalAction = conditionalActions.Where(c => c.Id == id).FirstOrDefault();
            var index = conditionalActions.IndexOf(conditionalAction);
            conditionalActions.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Gets the list of ids for the conditional actions
        /// </summary>
        /// <returns>list of ids</returns>
        public List<int> GetListOfName()
        {
            var listOfIds = new List<int>();
            foreach (var conditionalAction in conditionalActions)
            {
                listOfIds.Add(conditionalAction.Id);
            }
            return listOfIds;
        }

        /// <summary>
        /// Gets the list of conditions from a conditional action
        /// </summary>
        /// <param name="id">id of the conditional action to get conditions from</param>
        /// <returns>list of conditions</returns>
        public List<Condition> GetConditions(int id)
        {
            var conditionalAction = conditionalActions.Where(c => c.Id == id).FirstOrDefault();
            return conditionalAction.Conditions;
        }

        /// <summary>
        /// Gets the list of actions from a conditional action
        /// </summary>
        /// <param name="id">id of the conditional action to get actions from</param>
        /// <returns>list of actions</returns>
        public List<Action> GetActions(int id)
        {
            var conditionalAction = conditionalActions.Where(c => c.Id == id).FirstOrDefault();
            return conditionalAction.Actions;
        }

        /// <summary>
        /// Gets the boolean indicating is the conditional action is enabled
        /// </summary>
        /// <param name="id">id of the conditional action to check if enabled</param>
        /// <returns>true if is enabled</returns>
        public bool IsActionEnabled(int id)
        {
            var conditionalAction = conditionalActions.Where(c => c.Id == id).FirstOrDefault();
            return conditionalAction.IsEnabled;
        }

        /// <summary>
        /// Iterates through all the conditional actions to check conditions and execute actions 
        /// </summary>
        public void ProcessAllConditionalActions()
        {
            var currentWeather = WeatherConditions.GetCurrentWeather(SensorList.getSensorList());
            foreach (var conditionalAction in conditionalActions)
            {
                conditionalAction.CheckAndExecute(currentWeather);
            }
        }

        /// <summary>
        /// Saves the conditional actions to a file within the given directory path
        /// </summary>
        /// <param name="directoryPath">path to the directory that save file will be made</param>
        /// <returns>true if saved successfully</returns>
        public bool Save(string directoryPath)
        {
            //name
            //SensorId,isBetween,high,low
            //SensorId,isBetween,high,low
            //}
            //DeviceId,output
            //DeviceId,output
            //}
            //true
            using (StreamWriter writer = new StreamWriter(directoryPath + filename, false))
            {
                foreach (var conditionalAction in conditionalActions)
                {
                    writer.WriteLine(conditionalAction.Id);

                    foreach (var condition in conditionalAction.Conditions)
                    {
                        if (condition.Sensor != null)
                        {
                            writer.WriteLine(string.Format("{0},{1},{2},{3}",
                                condition.Sensor.SensorID, condition.IsBetweenTrigger, condition.LowThreshold, condition.HighThreshold)); 
                        }
                        else
                        {
                            writer.WriteLine(string.Format("{0},{1}",condition.Weather, condition.IsAboveTrigger));
                        }
                    }
                    writer.WriteLine("}");

                    foreach (var action in conditionalAction.Actions)
                    {
                        writer.WriteLine(string.Format("{0},{1}", action.Device.GetDeviceID(), action.OutputState));
                    }
                    writer.WriteLine("}");

                    writer.WriteLine(conditionalAction.IsEnabled);
                }
            }

            deviceList.save(directoryPath);
            sensorList.save(directoryPath);
            return true;
        }

        /// <summary>
        /// Loads the conditional actions from file within given directory path
        /// </summary>
        /// <param name="directoryPath">path to the directory that file will be read from</param>
        /// <returns>this object created using the loaded information</returns>
        public void Load(string directoryPath)
        {
            deviceList.load(directoryPath);
            sensorList.load(directoryPath);

            if (File.Exists(directoryPath + filename))
            {
                using (StreamReader reader = new StreamReader(directoryPath + filename))
                {
                    while (!reader.EndOfStream)
                    {
                        var id = Convert.ToInt32(reader.ReadLine());

                        var conditions = new List<Condition>();
                        var line = reader.ReadLine();
                        while (line != "}")
                        {
                            var parts = line.Split(',');
                            if (parts.Length == 4)
                            {
                                conditions.Add(new Condition(this.SensorList.search(Convert.ToInt32(parts[0])),
                                    Convert.ToBoolean(parts[1]), Convert.ToSingle(parts[2]), Convert.ToSingle(parts[3])));
                            }
                            else
                            {
                                conditions.Add(new Condition((Weather)Enum.Parse(typeof(Weather), parts[0]), Convert.ToBoolean(parts[1])));
                            }
                            line = reader.ReadLine();
                        }

                        var actions = new List<Action>();
                        line = reader.ReadLine();
                        while (line != "}")
                        {
                            var parts = line.Split(',');
                            actions.Add(new Action(this.DeviceList.search(Convert.ToInt32(parts[0])), Convert.ToBoolean(parts[1])));
                            line = reader.ReadLine();
                        }

                        var isEnabled = Convert.ToBoolean(reader.ReadLine());

                        conditionalActions.Add(new ConditionalAction(id, conditions, actions, isEnabled));
                    }
                }
            }
        }

        private class ConditionalAction
        {
            private int id;
            private List<Condition> conditions;
            private List<Action> actions;
            private bool isEnabled;

            private static int masterId = 1;

            public int Id { get => id; private set => id = value; }
            public List<Condition> Conditions { get => conditions; set => conditions = value; }
            public List<Action> Actions { get => actions; set => actions = value; }
            public bool IsEnabled { get => isEnabled; set => isEnabled = value; }

            /// <summary>
            /// Parameterized constructor for a collection of conditions pairs with their respective collection of actions
            /// </summary>
            /// <param name="id">id given to the conditional action</param>
            /// <param name="conditions">all conditions that must be met</param>
            /// <param name="actions">all actions taken when conditions are met</param>
            /// <param name="isEnabled">indicate whether this conditional action is enabled, default true</param>
            public ConditionalAction(int id, List<Condition> conditions, List<Action> actions, bool isEnabled)
            {
                Id = id;
                if (Id > masterId)
                {
                    masterId = Id + 1;
                }
                Conditions = conditions;
                Actions = actions;
                IsEnabled = isEnabled;
            }

            /// <summary>
            /// Parameterized constructor for a collection of conditions pairs with their respective collection of actions
            /// </summary>
            /// <param name="conditions">all conditions that must be met</param>
            /// <param name="actions">all actions taken when conditions are met</param>
            /// <param name="isEnabled">indicate whether this conditional action is enabled, default true</param>
            public ConditionalAction(List<Condition> conditions, List<Action> actions, bool isEnabled = true)
            {
                Id = masterId++;
                Conditions = conditions;
                Actions = actions;
                IsEnabled = isEnabled;
            }

            /// <summary>
            /// When enabled, checks all the conditions, and executes all the actions if the conditions have been met
            /// </summary>
            /// <param name="currentWeather">the current Weather as an enum to use for Conditions</param>
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
