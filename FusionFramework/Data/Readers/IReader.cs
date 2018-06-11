using FusionFramework.Data.Segmentators;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionFramework.Core.Data.Reader
{
    delegate void ReadFinished(dynamic Output);
    abstract class IReader : Transformable
    {
        public ReadFinished OnReadFinished;
        protected ISegmentator Segmentator;
        protected string Path;

        public abstract void Start();
        
    }
}
