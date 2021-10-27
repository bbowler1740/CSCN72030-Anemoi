using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCN72030_Anemoi
{
    class ConditionAnalyzer
    {
        private List<ConditionalAction> conditionalActions = new List<ConditionalAction>();
        private DeviceList deviceList;

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

        //check all Enabled conditionalActions Conditions for true
        //if true, perform Actions

        public string Stringify()
        {
            //name{C:(Sensor,isBetween,high,low),(Sensor,isBetween,high,low)A:(Device, output),(Device, output)E:true}
            var stringifiedConditionalAnalyzer = "";
            foreach (var conditionalAction in conditionalActions)
            {
                stringifiedConditionalAnalyzer += conditionalAction.Name + "{C:";
                foreach (var condition in conditionalAction.Conditions)
                {
                    stringifiedConditionalAnalyzer += string.Format("({0},{1},{2},{3}),", 
                        condition.Sensor.GetType().Name, condition.IsBetween, condition.LowThreshold, condition.HighThreshold);
                }
                var lastIndex = stringifiedConditionalAnalyzer.LastIndexOf(',');
                if (lastIndex != -1)
                {
                    stringifiedConditionalAnalyzer = stringifiedConditionalAnalyzer.Remove(lastIndex);
                }

                stringifiedConditionalAnalyzer += "A:";
                foreach (var action in conditionalAction.Actions)
                {
                    stringifiedConditionalAnalyzer += string.Format("({0},{1}),",
                        action.Device.GetType().Name, action.OutputValue);
                }
                lastIndex = stringifiedConditionalAnalyzer.LastIndexOf(',');
                if (lastIndex != -1)
                {
                    stringifiedConditionalAnalyzer = stringifiedConditionalAnalyzer.Remove(lastIndex);
                }

                stringifiedConditionalAnalyzer += "E:" + conditionalAction.IsEnabled;
            }
            return stringifiedConditionalAnalyzer;
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
        }
    }
}
