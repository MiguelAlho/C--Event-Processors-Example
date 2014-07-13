using System;
using NUnit.Framework;
using DomainA.Events;

namespace DomainA.Tests
{
    [TestFixture]
    public class ProcessStartedTests
    {
        [Test]
        public void CanCreateInstanceOfProcessEndedEvent()
        {
            Guid processId = Guid.NewGuid();
            Guid processType = ProcessGuids.ProcessA;
            DateTime time = DateTime.UtcNow;


            ProcessStarted @event = new ProcessStarted(processId, processType);

            Assert.IsNotNull(@event);
            Assert.IsInstanceOf<Event>(@event);

            Assert.AreEqual(processId, @event.ProcessId);
            Assert.AreEqual(processType, @event.ProcessTypeId);

        }
    }
}
