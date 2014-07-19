using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventProcessing.Framework.Tests.Dispatch
{
    public interface IHandlerFactory
    {
        IEnumerable<dynamic> GetHandlersFor(Event eventHandler);
    }
}
