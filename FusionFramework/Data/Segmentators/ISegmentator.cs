using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Data.Segmentators
{
    abstract class ISegmentator
    {
        public List<double[]> Window = new List<double[]>();

        public abstract bool Push(double[] v);    
    }
}
