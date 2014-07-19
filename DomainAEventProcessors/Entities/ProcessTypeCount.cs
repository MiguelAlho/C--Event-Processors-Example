using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProcessing.Framework;

namespace DomainAEventProcessors.Entities
{
    public class ProcessTypeCount : Entity
    {
        public int Count { get; private set; }

        public ProcessTypeCount(string id)
            :base(id)
        {
            
        }

        public void Increment()
        {
            Count++;
        }

        internal static string GetIdFrom(Guid guid)
        {
            return guid.ToString();
        }
    }
}
