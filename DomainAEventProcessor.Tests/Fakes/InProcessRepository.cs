using DomainAEventProcessor.Tests.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainAEventProcessors.Interfaces;
using EventProcessing.Framework;

namespace DomainAEventProcessor.Tests.Fakes
{
    /// <summary>
    /// Simple InProcess storage, based on a distionary. Serves as a double for testing
    /// ITestRepository interface allows for inspection (spy)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    internal class InProcessRepository<TEntity>: 
        IRepository<TEntity>, 
        ITestRepository<TEntity> 
        where TEntity : Entity
    {        
        Dictionary<string, TEntity> holder = new Dictionary<string, TEntity>();

        public TEntity Get(string id)
        {
            return holder.ContainsKey(id)
                ? holder[id]
                : null as TEntity;
        }

        public void Save(TEntity entity)
        {
            holder[entity.Id] = entity;
        }




        public Dictionary<string, TEntity> GetPersistedData()
        {
            return holder;
        }
        
    }
}
