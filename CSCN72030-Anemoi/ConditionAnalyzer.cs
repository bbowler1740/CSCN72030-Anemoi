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
        //has a device list of Sensors and Devices
        //check all Enabled conditionalActions Conditions for true
            //if true, perform Actions

        //CRUD conditional actions


        private class ConditionalAction
        {
            private string name;
            private List<Condition> conditions;
            private List<Action> actions;
            private bool isEnabled;

            public string Name { get => name; set => name = value; }
            public List<Condition> Conditions { get => conditions; private set => conditions = value; }
            public List<Action> Actions { get => actions; private set => actions = value; }
            public bool IsEnabled { get => isEnabled; set => isEnabled = value; }
        }
    }
}
