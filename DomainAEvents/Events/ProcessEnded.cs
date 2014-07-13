using System;

namespace DomainA.Events
{
    /// <summary>
    /// Class describes an event signaling the end of a process
    /// </summary>
    public class ProcessEnded : Event
    {
        /// <summary>
        /// The terminated process's Id
        /// </summary>
        public Guid ProcessId { get; private set; }
        /// <summary>
        /// The termination Time
        /// </summary>
        public DateTime EndTime { get; private set; }
        /// <summary>
        /// Indicates if the process termianted due to error
        /// </summary>
        public bool WithError { get; private set; }


        /* Any other processing end data could be added here, such as a calculated duration, ticks, error messages... */



        /// <summary>
        /// Creates a message that describes the ending of a process
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="terminationTime"></param>
        public ProcessEnded(Guid processId, DateTime terminationTime, bool withError)
            :base()
        {
            ProcessId = processId;
            EndTime = terminationTime;
            WithError = withError;
        }
    }
}
