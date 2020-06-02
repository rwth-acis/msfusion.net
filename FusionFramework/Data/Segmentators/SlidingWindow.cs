using FusionFramework.Data.Segmentators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Data.Segmentators
{
    /// <summary>
    /// Sliding Window breaks down the data stream in to operlapping windows.
    /// </summary>
    public class SlidingWindow<T>
    {
        /// <summary>
        /// Gets or sets the data window.
        /// </summary>
        List<T> TempWindow = new List<T>();

        public List<T> Window = new List<T>();


        /// <summary>
        /// Number of data rows in a window.
        /// </summary>
        int NumberOfRows;

        /// <summary>
        /// Percentage of the two window overlapping.
        /// </summary>
        int Overlapping;

        /// <summary>
        /// Instantiate Sliding window with number of rows and overlapping settings.
        /// </summary>
        /// <param name="numRows">Number of data rows in a window.</param>
        /// <param name="percentageOverlap">Percentage of the two window overlapping.</param>
        public SlidingWindow(int numRows, double percentageOverlap)
        {
            NumberOfRows = numRows;
            Overlapping = (int)Math.Round(numRows * percentageOverlap / 100);
        }

        /// <summary>
        /// Push data within a window
        /// </summary>
        /// <param name="v">Vector to be pushed in the data.</param>
        /// <returns>If window is filled or not.</returns>
        public bool Push(T v)
        {
            TempWindow.Add(v);
            if (TempWindow.Count >= NumberOfRows)
            {
                Window = new List<T>(TempWindow);
                TempWindow.RemoveRange(0, NumberOfRows - Overlapping);
                return true;
            } else
            {
                return false;
            }
        }
    }
}
