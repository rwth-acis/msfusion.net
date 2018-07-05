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
            { "XOR", new List<IModule>() { new XORModule1(new Binary("/i5/binary/0", 10, 50), new Binary("/i5/binary/1", 10, 50)) } }
        };

        public List<string> RequiredSensors;
        public abstract void Config(FusionFinished fusionFinished);
        public abstract bool IsCalculatable(List<string> availableSensors);
        public abstract void Start();
        public abstract void Stop();
        public abstract void Train();



    }
}
