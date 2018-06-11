using FusionFramework.Data.Segmentators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Transformer.Data
{
    class SlidingWindow : ISegmentator
    {
        int NumberOfRows;
        int Overlapping;
        

        public SlidingWindow(int numRows, double percentageOverlap)
        {
            NumberOfRows = numRows;
            Overlapping = (int)Math.Round(numRows * percentageOverlap / 100);
        }

        public override bool Push(double[] v)
        {
            Window.Add(v);
            if (Window.Count >= NumberOfRows)
            {
                Window.RemoveRange(0, Overlapping);
                return true;
            } else
            {
                return false;
            }
        }
    }
}
