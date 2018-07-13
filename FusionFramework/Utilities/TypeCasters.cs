using System;
using System.Collections.Generic;
using System.Linq;

namespace FusionFramework.Utilities
{
    public static class TypeCasters
    {
        public static List<int[]> DoubleMultiArrayToInt(List<double[]> data)
        {
            List<int[]> Output = new List<int[]>();
            data.ForEach(ar => Output.Add(ar.Select(Convert.ToInt32).ToArray()));
            return Output;
        }

        public static List<int> DoubleArrayToInt(List<int> data)
        {
            return data.ToArray().Select(Convert.ToInt32).ToList<int>();
        }

        public static T[,] CreateRectangularArray<T>(IList<T[]> arrays)
        {
            // TODO: Validation and special-casing for arrays.Count == 0
            int minorLength = arrays[0].Length;
            T[,] ret = new T[arrays.Count, minorLength];
            for (int i = 0; i < arrays.Count; i++)
            {
                var array = arrays[i];
                if (array.Length != minorLength)
                {
                    throw new ArgumentException
                        ("All arrays must be the same length");
                }
                for (int j = 0; j < minorLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }
            return ret;
        }

        public static List<T[]> CreateArrayOfArrays<T>(T[,] arrays)
        {
            List<T[]> Return = new List<T[]>();

            for(int i = 0; i < arrays.GetLength(0); i++)
            {
                T[] s = new T[arrays.GetLength(1)];
                for(int j = 0; j < arrays.GetLength(1); j++) {
                    s[j] = arrays[i, j];
                }
                Return.Add(s);
            }
            
            return Return;
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

    }
}
