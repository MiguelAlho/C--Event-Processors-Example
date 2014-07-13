using System;

namespace DomainA
{
    /// <summary>
    /// Event Base class. Could be in another project / class library for sharing
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Unique Id Event
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// DateTime the event was created
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Base constructor for Events. Initializes Id and creation time
        /// </summary>
        public Event()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
