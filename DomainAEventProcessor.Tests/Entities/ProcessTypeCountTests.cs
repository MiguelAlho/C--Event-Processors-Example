using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DomainA;
using EventProcessing.Framework;
using NUnit.Framework;
using DomainAEventProcessors.Entities;

namespace DomainAEventProcessor.Tests.Entities
{
    [TestFixture]
    public class ProcessTypeCountTests
    {
        [Test]
        public void CanCreateInstanceOfProcessTypeCount()
        {
            Guid guid = ProcessGuids.ProcessA;
            ProcessTypeCount instance = new ProcessTypeCount(guid.ToString());

            Assert.IsNotNull(instance);
            Assert.IsInstanceOf<Entity>(instance);
            Assert.AreEqual(guid.ToString(), instance.Id);
            Assert.AreEqual(0, instance.Count);
        }

        [Test]
        public void CanIncrementProcessTypeCounter()
        {
            Guid guid = ProcessGuids.ProcessA;
            ProcessTypeCount instance = new ProcessTypeCount(guid.ToString());

            instance.Increment();
            instance.Increment();

            Assert.AreEqual(2, instance.Count);
        }

        [TestCase(1, "00000001-0000-0000-0000-000000000000")]
        [TestCase(2, "00000002-0000-0000-0000-000000000000")]
        public void CanCreateIdKeyFromDate(int guidSeed, string expectedGuidAsString)
        {
            Guid guid = new Guid(guidSeed, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            Assert.AreEqual(expectedGuidAsString, guid.ToString());
        }
    }
}
