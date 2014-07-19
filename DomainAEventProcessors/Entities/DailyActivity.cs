using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProcessing.Framework;

namespace DomainAEventProcessors.Entities
{
    public class DailyActivity : Entity
    {
        
        /// <summary>
        /// The Date associated to the activity
        /// </summary>
        public DateTime Date { get; private set; }


        public int ActiveProcesses { get { return TotalProcessCount - CompletionCount; } }   //number of active work orders (snapshot count - could also be calculated if need be)
        
        public int TotalProcessCount {
            get { return statuses.Keys.Count; }
        }
        public int CompletionCount {
            get { return statuses.Values.Count(o => o.EndedAt.HasValue); }
        }

        public int CompletedWithErrorCount{
            get { return statuses.Values.Count(o => o.EndedWithError); }
        }

        private Dictionary<Guid, ProcessStatus> statuses = new Dictionary<Guid, ProcessStatus>();
        


        public DailyActivity(string id)
            :base(id)
        {
            
        }


        
        public void AddProcess(Guid processId, DateTime startTime)
        {
            statuses.Add(processId, new ProcessStatus(startTime));
        }

        
        public void EndProcess(Guid processId, DateTime endTime, bool withErrors)
        {
            if (statuses.ContainsKey(processId))
            {
                statuses[processId].Complete(endTime, withErrors);
            }
        }
        

        /// <summary>
        /// Creates Id's for the entity
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string GetIdFrom(DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
