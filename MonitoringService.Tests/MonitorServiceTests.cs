using EventProcessing.Framework;
using EventProcessing.Framework.Dispatch;
using EventProcessing.Framework.Tests.Dispatch;
using MonitoringService.Tests.Fakes;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringService.Tests
{
    [TestFixture]
    public class MonitorServiceTests
    {
        [Test]
        public void CanCreateInstanceOfmonitorService()
        {
            Mock<IEventDispatcher> dispatcher = new Mock<IEventDispatcher>();
            IMonitor monitor = new MonitorService(dispatcher.Object);

            Assert.IsNotNull(monitor);
            Assert.IsInstanceOf<MonitorService>(monitor);
            Assert.IsInstanceOf<IMonitor>(monitor);
        }

        [Test]
        public void CanReceiveEevntsAndPassToHandlers()
        {
            Mock<IEventDispatcher> dispatcher = new Mock<IEventDispatcher>();
            dispatcher.Setup(o => o.DispatchEvent(It.IsAny<EventThatHappened>())).Verifiable();

            IMonitor monitor = new MonitorService(dispatcher.Object);

            monitor.ReceiveEvent(new EventThatHappened());

            dispatcher.Verify();
        }
    }
}
