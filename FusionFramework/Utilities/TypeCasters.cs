using System;
using System.Collections.Generic;
using System.Linq;

namespace FusionFramework.Utilities
{
    class TypeCasters
    {
        public static List<int[]> DoubleMultiArrayToInt(List<double[]> data)
        {
            List<int[]> Output = new List<int[]>();
            data.ForEach(ar => Output.Add(ar.Select(Convert.ToInt32).ToArray()));
            return Output;
        }

        public static List<int> DoubleArrayToInt(List<double> data)
        {
            return data.ToArray().Select(Convert.ToInt32).ToList<int>();
        }
    }
}
