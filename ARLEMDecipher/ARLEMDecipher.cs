using ARLEMDecipher.Models.Activities;
using ARLEMDecipher.Models.Workplaces;
using ARLEMDecipher.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ARLEMDecipher
{
    public struct ExportedAction
    {
        public int Id;
        public string Module;
        public int[] ComparedValue;
        public int NextAction;
        public string Instruction;
    };

    public class ARLEMDecipher
    {
        
        string ServerUrl;
        RESTManager ApiManager;
        public Workplace Workplace;
        Activity Activity;

        public ARLEMDecipher(string url)
        {
            ServerUrl = url;
            ApiManager = new RESTManager(url);
        }

        public bool LoadWorkplace(int id)
        {
            Workplace = ApiManager.GET<Workplace>("workplace/" + id.ToString()); 
            if(Workplace == null)
            {
                Console.WriteLine("Workplace not found");
                return false;
            }
            return true;
        }

        public bool LoadWorkplaceJSON(int id)
        {
            Workplace = ApiManager.GETJSON<Workplace>("workplace/" + id.ToString());
            if (Workplace == null || Workplace.InternalID == 0)
            {
                Console.WriteLine("Workplace not found");
                return false;
            }
            return true;
        }

        public void LoadActivityJSON(int id)
        {
            Activity = ApiManager.GETJSON<Activity>("activity/" + id.ToString());
        }


        public int[] AvailableActivites()
        {
            List<int> Ids = new List<int>();
            Workplace.Activities.ForEach(x => Ids.Add(x.ID));
            if(Ids.Count == 0)
            {
                Console.WriteLine("No activites found");
            }
            return Ids.ToArray();
        }

        public List<string> AvailableSensors()
        {
            List<string> Sensors = new List<string>(); 
            Workplace.Sensors.ForEach(sensor =>
            {
                Sensors.Add(sensor.URI);
            });
            return Sensors;
        }

        public List<string> RequiredModules()
        {
            List<string> Modules = new List<string>();

            Activity.Actions.ForEach(action =>
            {
                action.Triggers.ForEach(trigger =>
                {
                    if(trigger.Mode == "module" && trigger.Modular.Name != "")
                    {
                        Modules.Add(trigger.Modular.Name);
                    }
                });
            });

            return Modules.Distinct().ToList();
        }

        public List<ExportedAction> GetActivityActions()
        {
            List<ExportedAction> RequiredActions = new List<ExportedAction>();
            Activity.Actions.ForEach(action =>
            {
                ExportedAction tmpAction = new ExportedAction();
                action.Triggers.ForEach(trigger =>
                {
                    if (trigger.Mode == "module" && trigger.Modular.Name != "" && trigger.Value != "")
                    {
                        int entity = 0;
                        if (trigger.EntityType == "action")
                        {
                            entity = trigger.Entity;
                        }
                        if (entity != 0)
                        {
                            tmpAction.Id = action.ID;
                            tmpAction.Module = trigger.Modular.Name;
                            tmpAction.ComparedValue = Array.ConvertAll(trigger.Value.Split(','), int.Parse);
                            tmpAction.NextAction = entity;
                            tmpAction.Instruction = action.InstructionDescription;
                            RequiredActions.Add(tmpAction);
                        }
                    }
                });
            });
            return RequiredActions;
        }
    }
}
