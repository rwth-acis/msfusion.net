using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FusionFramework.Core
{
    public delegate void GetBufferData(dynamic data);
    class CoreBuffer<T>
    {
        GetBufferData BufferReadyTrigger;
        Dictionary<int, List<T>> MainBuffer = new Dictionary<int, List<T>>();


        public CoreBuffer(GetBufferData trigger, int count)
        {
            for(int i = 0; i < count; i++)
            {
                MainBuffer.Add(i, new List<T>());
            }
            BufferReadyTrigger = trigger;
        }

        public void Push(T data, int id)
        {
            MainBuffer[id].Add(data);
            CheckBuffer();
        }

        private void CheckBuffer()
        {
            bool isFull = true;
            List<T> TmpData = new List<T>();
            foreach(var Item in MainBuffer)
            {
                if(Item.Value.Count <= 0)
                {
                    isFull = false;
                    break;
                }
                TmpData.Add(Item.Value[0]);
            }
            if (isFull)
            {
                BufferReadyTrigger(TmpData);
            }
        }
    }
}
