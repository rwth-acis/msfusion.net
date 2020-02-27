using ContestManager.Modules.HAR;
using ContestManager.Modules.HGR;
using ContestManager.Modules.XOR;
using FusionFramework.Fusion.Strategies;
using FusionFramework.VirtualSensor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContestManager.Modules
{
    public abstract class IModule
    {
        public static Dictionary<string, List<IModule>> ModuleMap = new Dictionary<string, List<IModule>>()
        {
            { "XOR", new List<IModule>() { new XORModule1(new Binary("/i5/binary/0", 10, 50), new Binary("/i5/binary/1", 10, 50)) } },
            { "HAR", new List<IModule>() { new WISDM("/i5/mobileMotion/accelerometer") } },
            { "HGR", new List<IModule>() { new MyoGym("/i5/myo/accelerometer", "/i5/myo/gyroscope", "/i5/myo/emg"), new MyoGym1("/i5/myo/full") } },
        };

        public List<string> RequiredSensors;
        public abstract void Config(FusionFinished fusionFinished);
        public abstract IFusionStrategy Config();
        public abstract void Start();
        public abstract void Stop();
        public abstract void Train();

        public abstract void DecisionToMessage(int decision);

        public bool IsCalculatable(List<string> availableSensors)
        {
            bool DidFound = true;
            foreach(string name in RequiredSensors)
            {
                if(availableSensors.Find(x => x == name) == null)
                {
                    DidFound = false;
                }
            }
            return DidFound;
        }


    }
}
