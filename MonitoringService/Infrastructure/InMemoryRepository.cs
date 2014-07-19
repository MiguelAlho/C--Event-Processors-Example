using DomainAEventProcessors.Interfaces;
using EventProcessing.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitoringService.Infrastructure
{
    public class InMemoryRepository<T> : IRepository<T> where T : Entity
    {
        Dictionary<string, T> holder = new Dictionary<string, T>();

        public T Get(string id)
        {
            return holder.ContainsKey(id)
                ? holder[id]
                : null as T;
        }

        public void Save(T entity)
        {
            holder[entity.Id] = entity;
        }



    }
}