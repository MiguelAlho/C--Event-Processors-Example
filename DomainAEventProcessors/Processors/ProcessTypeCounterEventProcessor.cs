using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using DomainA.Events;
using DomainAEventProcessors.Entities;
using EventProcessing.Framework;
using DomainAEventProcessors.Interfaces;

namespace DomainAEventProcessors.Processors
{
    public class ProcessTypeCounterEventProcessor :
        IProcessEvents,
        IHandleEvent<ProcessStarted>
    {
        private IRepository<ProcessTypeCount> repository;

        public ProcessTypeCounterEventProcessor(
            IRepository<ProcessTypeCount> repository
            )
        {
            Condition.Requires(repository, "repository").IsNotNull();

            this.repository = repository;
        }


        public void Handle(ProcessStarted @event)
        {
            string id = ProcessTypeCount.GetIdFrom(@event.ProcessTypeId);
            ProcessTypeCount counter = GetOrCreateProcessTypeCountEntity(id);

            //execute change / update
            counter.Increment();

            //save - persist?
            repository.Save(counter);
        }


        private ProcessTypeCount GetOrCreateProcessTypeCountEntity(string id)
        {
            ProcessTypeCount counter = repository.Get(id);

            if (counter == null)
            {
                counter = new ProcessTypeCount(id);
                repository.Save(counter);
            }

            return counter;
        }
    }
}
