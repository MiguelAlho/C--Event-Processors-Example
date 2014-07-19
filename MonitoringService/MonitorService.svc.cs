using CuttingEdge.Conditions;
using EventProcessing.Framework.Dispatch;
using Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MonitoringService
{
    public class MonitorService : IMonitor
    {
        private IEventDispatcher dispatcher;

        public MonitorService(IEventDispatcher dispatcher)
        {
            Condition.Requires(dispatcher, "dispatcher").IsNotNull();

            this.dispatcher = dispatcher;
        }

        public void ReceiveEvent(Event @event)
        {
            dispatcher.DispatchEvent(@event);
        }
    }
}
