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
            IMonitor monitor = new MonitorService();

            Assert.IsNotNull(monitor);
            Assert.IsInstanceOf<MonitorService>(monitor);
            Assert.IsInstanceOf<IMonitor>(monitor);
        }

    }
}
