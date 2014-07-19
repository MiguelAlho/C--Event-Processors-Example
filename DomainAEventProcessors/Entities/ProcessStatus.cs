using System;

namespace DomainAEventProcessors.Entities
{
    public class ProcessStatus
    {
        public DateTime StartedAt { get; private set; }
        public DateTime? EndedAt { get; private set; }
        public bool EndedWithError { get; private set; }

        public ProcessStatus(DateTime startedAt)
        {
            StartedAt = startedAt;
        }

        public void Complete(DateTime completedAt, bool completedWithError)
        {
            EndedAt = completedAt;
            EndedWithError = completedWithError;
        }
    }
}