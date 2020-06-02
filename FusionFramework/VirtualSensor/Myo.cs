using System;
using System.Collections.Generic;
using System.Text;
using FusionFramework.Features;
using FusionFramework.Data.Segmentators;
using FusionFramework.Features.Complex;
using FusionFramework.Features.TimeDomain;

namespace FusionFramework.VirtualSensor
{
    public class Myo : IVirtualSensor
    {
        public Myo(string mqttURL)
        {
            MqttUrl = mqttURL;
            Features = new IFeature[]
            {
                new HjorthParameters(8,9,10),
                new StandardDeviation(8,9,10),
                new Mean(8,9,10),
                new Max(8,9,10),
                new Min(8,9,10),
                new Percentile(5,  8,9,10),
                new Percentile(10, 8,9,10),
                new Percentile(25, 8,9,10),
                new Percentile(50, 8,9,10),
                new Percentile(75, 8,9,10),
                new Percentile(90, 8,9,10),
                new Percentile(95, 8,9,10),
                new ZeroCrossing(8,9,10),
                new MeanCrossing(8,9,10),
                new Entropy(8, 9, 10),
                new Correlation(9, 10),
                new Correlation(9, 11),
                new Correlation(10, 11),

                new HjorthParameters(11,12,13),
                new StandardDeviation(11,12,13),
                new Mean(11,12,13),
                new Max(11,12,13),
                new Min(11,12,13),
                new Percentile(5,  11,12,13),
                new Percentile(10, 11,12,13),
                new Percentile(25, 11,12,13),
                new Percentile(50, 11,12,13),
                new Percentile(75, 11,12,13),
                new Percentile(90, 11,12,13),
                new Percentile(95, 11,12,13),
                new ZeroCrossing(11,12,13),
                new MeanCrossing(11,12,13),
                new Entropy(11,12,13),

                new StandardDeviation(0,1,2,3,4,5,6,7),
                new Mean(0,1,2,3,4,5,6,7 ),
                new Max(0,1,2,3,4,5,6,7 ),
                new Min(0,1,2,3,4,5,6,7 ),
                new Percentile(5, 0,1,2,3,4,5,6,7 ),
                new Percentile(10, 0,1,2,3,4,5,6,7 ),
                new Percentile(25,  0,1,2,3,4,5,6,7 ),
                new Percentile(50, 0,1,2,3,4,5,6,7 ),
                new Percentile(75, 0,1,2,3,4,5,6,7 ),
                new Percentile(90, 0,1,2,3,4,5,6,7 ),
                new Percentile(95, 0,1,2,3,4,5,6,7 ),

                new SumLargerThan(25, 0,1,2,3,4,5,6,7 ),
                new SumLargerThan(50, 0,1,2,3,4,5,6,7 ),
                new SumLargerThan(100, 0,1,2,3,4,5,6,7 )
            };

            Window = new SlidingWindow<double[]>(200, 0);
        }
        public Myo(string mqttURL, int windowSize, int overlap)
        {
            MqttUrl = mqttURL;
            Features = new IFeature[]
            {
                new HjorthParameters(8,9,10),
                new StandardDeviation(8,9,10),
                new Mean(8,9,10),
                new Max(8,9,10),
                new Min(8,9,10),
                new Percentile(5,  8,9,10),
                new Percentile(10, 8,9,10),
                new Percentile(25, 8,9,10),
                new Percentile(50, 8,9,10),
                new Percentile(75, 8,9,10),
                new Percentile(90, 8,9,10),
                new Percentile(95, 8,9,10),
                new ZeroCrossing(8,9,10),
                new MeanCrossing(8,9,10),
                new Entropy(8, 9, 10),
                new Correlation(9, 10),
                new Correlation(9, 11),
                new Correlation(10, 11),

                new HjorthParameters(11,12,13),
                new StandardDeviation(11,12,13),
                new Mean(11,12,13),
                new Max(11,12,13),
                new Min(11,12,13),
                new Percentile(5,  11,12,13),
                new Percentile(10, 11,12,13),
                new Percentile(25, 11,12,13),
                new Percentile(50, 11,12,13),
                new Percentile(75, 11,12,13),
                new Percentile(90, 11,12,13),
                new Percentile(95, 11,12,13),
                new ZeroCrossing(11,12,13),
                new MeanCrossing(11,12,13),
                new Entropy(11,12,13),

                new StandardDeviation(0,1,2,3,4,5,6,7),
                new Mean(0,1,2,3,4,5,6,7 ),
                new Max(0,1,2,3,4,5,6,7 ),
                new Min(0,1,2,3,4,5,6,7 ),
                new Percentile(5, 0,1,2,3,4,5,6,7 ),
                new Percentile(10, 0,1,2,3,4,5,6,7 ),
                new Percentile(25,  0,1,2,3,4,5,6,7 ),
                new Percentile(50, 0,1,2,3,4,5,6,7 ),
                new Percentile(75, 0,1,2,3,4,5,6,7 ),
                new Percentile(90, 0,1,2,3,4,5,6,7 ),
                new Percentile(95, 0,1,2,3,4,5,6,7 ),

                new SumLargerThan(25, 0,1,2,3,4,5,6,7 ),
                new SumLargerThan(50, 0,1,2,3,4,5,6,7 ),
                new SumLargerThan(100, 0,1,2,3,4,5,6,7 )
            };

            Window = new SlidingWindow<double[]>(windowSize, overlap);
        }

        public Myo(string mqttURL, int windowSize, int overlap, IFeature[] features)
        {
            MqttUrl = mqttURL;
            Features = features;
            Window = new SlidingWindow<double[]>(windowSize, overlap);
        }
    }
}
