using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE_Reporting.Model
{
    public class IEnumerator <T>
    {
        List<T> data;
        int current;

        public IEnumerator(List<T> list)
        {
            data = list;
            current = 0;
        }

        public T Get()
        {
            return data[current];
        }

        public bool HasNext()
        {
            if (current < data.Count)
            {
                return true;
            }
            return false;
        }

        public void Next()
        {
            if (current < data.Count)
            {
                current++;
            }
            else Reset();
        }
       
        public void Reset()
        {
            current = 0; 
        }

    }
}
