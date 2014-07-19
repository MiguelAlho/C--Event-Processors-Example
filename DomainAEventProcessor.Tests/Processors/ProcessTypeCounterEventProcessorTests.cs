using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainA;
using DomainAEventProcessors.Processors;
using EventProcessing.Framework;
using NUnit.Framework;
using DomainAEventProcessors.Interfaces;
using DomainA.Events;
using DomainAEventProcessor.Tests.Interfaces;
using DomainAEventProcessor.Tests.Fakes;
using DomainAEventProcessors.Entities;

namespace DomainAEventProcessor.Tests.Processors
{
    [TestFixture]
    public class ProcessTypeCounterEventProcessorTests
    {
        [Test]
        public void CanCreateAnInstanceOfProcessTypeCounterEventProcessor()
        {
            IRepository<ProcessTypeCount> repo = GetRepositoryForTest();
            ProcessTypeCounterEventProcessor processor = new ProcessTypeCounterEventProcessor(
                repo
                );

            Assert.IsNotNull(processor);
            Assert.IsInstanceOf<IProcessEvents>(processor);
            Assert.IsInstanceOf<IHandleEvent<ProcessStarted>>(processor);

            //Make sure repository is initialized and no activity is registered
            var activity = (repo as ITestRepository<ProcessTypeCount>).GetPersistedData();

            Assert.IsNotNull(activity);
            CollectionAssert.IsEmpty(activity.Keys);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullRepositoryThrowsExceptionOnConstructorCall()
        {
            ProcessTypeCounterEventProcessor processor = new ProcessTypeCounterEventProcessor(
                null
                );
        }

        [Test]
        public void CanProcessProcessStartedEvents()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();

            Guid processA = ProcessGuids.ProcessA;
            Guid processB = ProcessGuids.ProcessB;

            ProcessStarted startA1 = new ProcessStarted(guid1, processA);
            ProcessStarted startB1 = new ProcessStarted(guid2, processB);
            ProcessStarted startA2 = new ProcessStarted(guid3, processA);

            IRepository<ProcessTypeCount> repo = GetRepositoryForTest();

            ProcessTypeCounterEventProcessor processor = new ProcessTypeCounterEventProcessor(
                repo
                );

            processor.Handle(startA1);
            processor.Handle(startB1);
            processor.Handle(startA2);

            ProcessTypeCount counterA = repo.Get(processA.ToString());
            Assert.IsNotNull(counterA);
            Assert.AreEqual(2, counterA.Count);

            ProcessTypeCount counterB = repo.Get(processB.ToString());
            Assert.IsNotNull(counterB);
            Assert.AreEqual(1, counterB.Count);

        }




        private static InProcessRepository<ProcessTypeCount> GetRepositoryForTest()
        {
            return new InProcessRepository<ProcessTypeCount>();
        }

    }
}
