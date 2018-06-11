using FusionFramework.Fusion;
using FusionFramework.Fusion.Strategies;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FusionFramework
{
    delegate void FusionFinished(int decision);
    class FusionTask
    {
        private List<IFusionStrategy> FusionTaskList = new List<IFusionStrategy>();

        public int Add(IFusionStrategy fusionTask)
        {
            FusionTaskList.Add(fusionTask);
            return FusionTaskList.IndexOf(fusionTask);
        }

        public void Fuse(int miliSeconds, FusionFinished callBack)
        {
            FusionTaskList.ForEach(Execute);
        }

        public void Execute(IFusionStrategy fusionTask)
        {
            fusionTask.Start();
        }
    }
}
