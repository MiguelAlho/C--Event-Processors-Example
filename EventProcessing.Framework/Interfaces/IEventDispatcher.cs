using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Events;

namespace EventProcessing.Framework.Dispatch
{
    public interface IEventDispatcher
    {
        void DispatchEvent(Event @event);
    }
}
