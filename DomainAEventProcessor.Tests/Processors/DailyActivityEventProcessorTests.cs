using System.Diagnostics;
using DomainA;
using DomainA.Events;
using DomainAEventProcessor.Tests.Fakes;
using DomainAEventProcessor.Tests.Interfaces;
using DomainAEventProcessors.Entities;
using DomainAEventProcessors.Interfaces;
using DomainAEventProcessors.Processors;
using EventProcessing.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainAEventProcessor.Tests
{
    [TestFixture]
    public class DailyActivityEventProcessorTests
    {
        [Test]
        public void CanCreateInstanceOfDailyActivityEventProcessor()
        {
            IRepository<DailyActivity> repo = GetRepositoryForTest();
            DailyActivityEventProcessor processor = new DailyActivityEventProcessor(
                repo    
                );

            Assert.IsNotNull(processor);
            Assert.IsInstanceOf<IProcessEvents>(processor);
            Assert.IsInstanceOf<IHandleEvent<ProcessStarted>>(processor);
            Assert.IsInstanceOf<IHandleEvent<ProcessEnded>>(processor);

            //Make sure repository is initialized and no activity is registered
            var activity = (repo as ITestRepository<DailyActivity>).GetPersistedData();

            Assert.IsNotNull(activity);
            CollectionAssert.IsEmpty(activity.Keys);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullRepositoryThrowsExceptionOnConstructorCall()
        {
            DailyActivityEventProcessor processor = new DailyActivityEventProcessor(
                null
                );
        }

        [Test]
        public void CanProcessAProcessStartedEvent()
        {
            Guid guid = Guid.NewGuid();
            Guid processType = ProcessGuids.ProcessA;
            ProcessStarted worEvent = new ProcessStarted(guid, processType);

            IRepository<DailyActivity> repo = GetRepositoryForTest();

            DailyActivityEventProcessor processor = new DailyActivityEventProcessor(
                repo
                );

            processor.Handle(worEvent);

            DailyActivity activity = repo.Get(GetIdForFirstActivityPersisted(repo));

            Assert.IsNotNull(activity);
            Assert.AreEqual(1, activity.ActiveProcesses);
            Assert.AreEqual(1, activity.TotalProcessCount);
            Assert.AreEqual(0, activity.CompletionCount);
            Assert.AreEqual(0, activity.CompletedWithErrorCount);

        }

        [Test]
        public void CanProcessAProcessEndededEvent()
        {
            Guid guid = Guid.NewGuid();
            Guid processType = ProcessGuids.ProcessA;
            ProcessStarted startEvent = new ProcessStarted(guid, processType);
            ProcessEnded endEvent = new ProcessEnded(guid, DateTime.UtcNow, false);

            IRepository<DailyActivity> repo = GetRepositoryForTest();

            DailyActivityEventProcessor processor = new DailyActivityEventProcessor(
                repo
                );

            processor.Handle(startEvent);
            processor.Handle(endEvent);

            DailyActivity activity = repo.Get(GetIdForFirstActivityPersisted(repo));

            Assert.IsNotNull(activity);
            Assert.AreEqual(0, activity.ActiveProcesses);
            Assert.AreEqual(1, activity.TotalProcessCount);
            Assert.AreEqual(1, activity.CompletionCount);
            Assert.AreEqual(0, activity.CompletedWithErrorCount);

        }

        [Test]
        public void IgnoreAProcessEndedEventForAUnkownProcess()
        {
            Guid guid = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid processType = ProcessGuids.ProcessA;
            ProcessStarted worEvent = new ProcessStarted(guid, processType);
            ProcessEnded worcEvent = new ProcessEnded(guid2, DateTime.UtcNow,  false);

            IRepository<DailyActivity> repo = GetRepositoryForTest();

            DailyActivityEventProcessor processor = new DailyActivityEventProcessor(
                repo
                );

            processor.Handle(worEvent);
            processor.Handle(worcEvent);

            DailyActivity activity = repo.Get(GetIdForFirstActivityPersisted(repo));

            Assert.IsNotNull(activity);
            Assert.AreEqual(1, activity.ActiveProcesses);
            Assert.AreEqual(1, activity.TotalProcessCount);
            Assert.AreEqual(0, activity.CompletionCount);
            Assert.AreEqual(0, activity.CompletedWithErrorCount);

        }
        
        private static InProcessRepository<DailyActivity> GetRepositoryForTest()
        {
            return new InProcessRepository<DailyActivity>();
        }

        private static string GetIdForFirstActivityPersisted(IRepository<DailyActivity> repo)
        {
            var activities = (repo as ITestRepository<DailyActivity>).GetPersistedData();
            return activities.Keys.FirstOrDefault();
        }

        
    }
}
