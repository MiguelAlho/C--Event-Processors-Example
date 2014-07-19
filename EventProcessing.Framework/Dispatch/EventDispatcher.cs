using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using EventProcessing.Framework.Tests.Dispatch;
using Events;

namespace EventProcessing.Framework.Dispatch
{
    public class EventDispatcher : IEventDispatcher
    {
        private IHandlerFactory factory;

        public EventDispatcher(IHandlerFactory factory)
        {
            Condition.Requires(factory, "factory").IsNotNull();

            this.factory = factory;
        }


        public void DispatchEvent(Event @event)
        {
            IEnumerable<dynamic> eventHandlers = factory.GetHandlersFor(@event);

            if (eventHandlers != null && eventHandlers.Any())
            {
                Parallel.ForEach(eventHandlers, handler => handler.Handle((dynamic)@event));
            }
        }
    }
}
