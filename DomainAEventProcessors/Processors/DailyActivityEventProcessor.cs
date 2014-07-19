using CuttingEdge.Conditions;
using DomainA.Events;
using DomainAEventProcessors.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainAEventProcessors.Interfaces;
using EventProcessing.Framework;

namespace DomainAEventProcessors.Processors
{
    public class DailyActivityEventProcessor :
        IProcessEvents,
        IHandleEvent<ProcessStarted>,
        IHandleEvent<ProcessEnded>
    {
        private IRepository<DailyActivity> repository;
        
        public DailyActivityEventProcessor(
            IRepository<DailyActivity> repository
            )
        {
            Condition.Requires(repository, "repository").IsNotNull();

            this.repository = repository;
        }

        public void Handle(ProcessStarted @event)
        {
            string id = DailyActivity.GetIdFrom(@event.CreatedAt);
            DailyActivity activity = GetOrCreateActivityEntity(id);

            //execute change / update
            activity.AddProcess(@event.ProcessId, @event.CreatedAt);

            //save - persist?
            repository.Save(activity);
        }

        public void Handle(ProcessEnded @event)
        {
            string id = DailyActivity.GetIdFrom(@event.CreatedAt);
            DailyActivity activity = repository.Get(id);   //should exist for this event

            if (activity != null)
            {
                //execute change / update
                activity.EndProcess(@event.ProcessId, @event.EndTime, @event.WithError);

                //save - persist?
                repository.Save(activity);
            }
        }




        private DailyActivity GetOrCreateActivityEntity(string id)
        {
            DailyActivity activity = repository.Get(id);

            if (activity == null)
            {
                activity = new DailyActivity(id);
                repository.Save(activity);
            }

            return activity;
        }
    }

}
