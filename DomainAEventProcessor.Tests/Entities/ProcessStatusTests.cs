using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DomainAEventProcessors.Entities;

namespace DomainAEventProcessor.Tests.Entities
{
    [TestFixture]
    public class ProcessStatusTests
    {
        [Test]
        public void CanCreateInstanceOfProcessStatus()
        {
            DateTime started = DateTime.UtcNow; 

            ProcessStatus status = new ProcessStatus(started);

            Assert.IsNotNull(status);
            Assert.AreEqual(started, status.StartedAt);
            Assert.IsNull(status.EndedAt);
            Assert.IsFalse(status.EndedWithError);
        }

        [Test]
        public void CanTerminateAProcessStatus()
        {
            DateTime started = DateTime.UtcNow.AddSeconds(-10);
            DateTime ended = DateTime.UtcNow;

            ProcessStatus status = new ProcessStatus(started);

            status.Complete(ended, true);

            Assert.AreEqual(started, status.StartedAt);
            Assert.IsTrue(status.EndedAt.HasValue);
            Assert.AreEqual(ended, status.EndedAt.Value);
            Assert.IsTrue(status.EndedWithError);
        }
    }
}
