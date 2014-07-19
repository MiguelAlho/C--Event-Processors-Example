using System;
using Events;
using NUnit.Framework;
using DomainA.Events;

namespace DomainA.Tests
{
    [TestFixture]
    public class ProcessEndedTests
    {
        [Test]
        public void CanCreateInstanceOfProcessEndedEvent()
        {
            Guid processId = Guid.NewGuid();
            DateTime time = DateTime.UtcNow;
            bool withError = true;

            ProcessEnded @event = new ProcessEnded(processId, time, withError);

            Assert.IsNotNull(@event);
            Assert.IsInstanceOf<Event>(@event);

            Assert.AreEqual(processId, @event.ProcessId);
            Assert.AreEqual(time, @event.EndTime);
            Assert.AreEqual(withError, @event.WithError);

        }
    }
}
