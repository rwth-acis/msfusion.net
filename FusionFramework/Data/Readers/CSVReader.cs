using System.IO;
using FusionFramework.Data.Segmentators;
using System.Linq;
using System.Collections.Generic;
using System;
using LumenWorks.Framework.IO.Csv;



namespace FusionFramework.Core.Data.Reader
{
    /// <summary>
    /// Reads data from a CSV file.
    /// </summary>
    public class CSVReader<T> : IReader
    {
        /// <summary>
        /// Segmentation if required by client for the incoming data.
        /// </summary>
        protected SlidingWindow<T> Segmentator;

        private CachedCsvReader Client;

        /// <summary>
        /// Instantiate the CSV reader with file details.
        /// </summary>
        /// <param name="address">Location of the file.</param>
        /// <param name="hasHeader">If file has headers or not.</param>
        /// <param name="onReadFinished">Trigger when reading finished.</param>
        public CSVReader(string address, bool hasHeader = false)
        {
            Client = new CachedCsvReader(new StreamReader(address), hasHeader);
        }

        /// <summary>
        /// Instantiate the CSV reader with file details.
        /// </summary>
        /// <param name="address">Location of the file.</param>
        /// <param name="hasHeader">If file has headers or not.</param>
        /// <param name="onReadFinished">Trigger when reading finished.</param>
        public CSVReader(string address, bool hasHeader, ReadFinished onReadFinished)
        {
            OnReadFinished = onReadFinished;
            Client = new CachedCsvReader(new StreamReader(address), hasHeader);
        }

        /// <summary>
        /// Instantiate the CSV reader with file details and segmentator.
        /// </summary>
        /// <param name="address">Location of the file.</param>
        /// <param name="hasHeader">If file has headers or not.</param>
        /// <param name="onReadFinished">Trigger when reading finished.</param>
        /// <param name="segmentator">Segmentation class that breaks the file in segementation / windows</param>
        public CSVReader(string address, bool hasHeader, ReadFinished onReadFinished, SlidingWindow<T> segmentator)
        {
            OnReadFinished = onReadFinished;
            Client = new CachedCsvReader(new StreamReader(address), hasHeader);
            Segmentator = segmentator;
        }

        /// <summary>
        /// Start Reading
        /// </summary>
        public override void Start()
        {
            if (Segmentator == null)
            {
                List<T> output = new List<T>();
                while (Client.ReadNextRecord())
                {
                    output.Add(Insert());
                }
                OnReadFinished(output);
            }
            else
            {
                List<List<T>> Output = new List<List<T>>();
                while (Client.ReadNextRecord())
                {
                    if (Segmentator.Push(Insert()) == true)
                    {
                        Output.Add(Segmentator.Window);
                    }
                }
                OnReadFinished(Output);
            }
        }

        /// <summary>
        /// Stop Reading
        /// </summary>
        public override void Stop()
        {
            Client.Dispose();
        }

        private T Insert()
        {
            if (typeof(T) == typeof(double[]))
            {
                List<double> Tmp = new List<double>();
                for (int i = 0; i < Client.FieldCount; i++)
                {
                    Tmp.Add(double.Parse(Client[i]));
                }
                return (T)Convert.ChangeType(Tmp.ToArray(), typeof(T));
            }
            else
            {
                return (T)Convert.ChangeType(Client[0], typeof(T));
            }

        }

        public void Add(SlidingWindow<T> slidingWindow)
        {
            Segmentator = slidingWindow;
        }

    }
}
