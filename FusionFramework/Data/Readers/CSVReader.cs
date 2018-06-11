using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvHelper;
using FusionFramework.Data.Segmentators;
using System.Linq;

namespace FusionFramework.Core.Data.Reader
{
    class CSVReader : IReader
    {
        private CsvReader Client;


        public CSVReader(string address, bool hasHeader, ReadFinished onReadFinished)
        {
            OnReadFinished = onReadFinished;
            Client = new CsvReader(File.OpenText(address), hasHeader);
        }

        public CSVReader(string address, bool hasHeader, ReadFinished onReadFinished, ISegmentator segmentator)
        {
            OnReadFinished = onReadFinished;
            Client = new CsvReader(File.OpenText(address), hasHeader);
            Segmentator = segmentator;
        }

        public override void Start()
        {
            if (Segmentator == null)
            {
                OnReadFinished(Client.GetRecords<double[]>().ToList<double[]>());
            } else
            {
                while (Client.Read())
                {
                    if (Segmentator.Push(Client.GetRecord<double[]>()) == true)
                    {
                        OnReadFinished(Segmentator.Window);
                    }
                }
            }
        }

       
    }
}
