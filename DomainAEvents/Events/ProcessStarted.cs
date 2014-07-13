using System;

namespace DomainA.Events
{
    public class ProcessStarted : Event
    {
        /// <summary>
        /// The Process Id that was started
        /// </summary>
        public Guid ProcessId { get; private set; }
        /// <summary>
        /// The Id of the specific process type
        /// </summary>
        public Guid ProcessTypeId { get; private set; }

        /* Include any other relevant properties here like process name, user, etc, etc..*/


        /// <summary>
        /// Creates and object that describes an event related to some process being initialized
        /// <param name="processId">The processId for the process that was started</param>
        /// </summary>
        public ProcessStarted(Guid processId, Guid processTypeId)
            :base()
        {
            ProcessId = processId;
            ProcessTypeId = processTypeId;
        }
    }
}
