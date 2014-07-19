using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainAEventProcessors.Entities;
using EventProcessing.Framework;
using NUnit.Framework;

namespace DomainAEventProcessor.Tests.Entities
{
    [TestFixture]
    public class DailyActivityTests
    {
        [Test]
        public void CanCreateInstanceOfDailyActivity()
        {
            string id = DateTime.UtcNow.ToString("yyyy-MM-dd");
            DailyActivity instance = new DailyActivity(id);

            Assert.IsNotNull(instance);
            Assert.IsInstanceOf<Entity>(instance);
        }

        [TestCase("2014-07-04", 2014, 7, 4, 0, 0, 0)]
        [TestCase("2014-07-04", 2014, 7, 4, 1, 1, 1)]
        public void CanCreateIdKeyFromDate(string expectedKey, int year, int month, int day, int hour, int min, int sec)
        {
            DateTime date = new DateTime(year, month, day, hour, min, sec);
            Assert.AreEqual(expectedKey, DailyActivity.GetIdFrom(date));
        }

        [Test]
        public void AddingAProcessIncrementsActiveCounter()
        {
            string id = DailyActivity.GetIdFrom(DateTime.UtcNow);
            DailyActivity activity = new DailyActivity(id);

            Guid processId = Guid.NewGuid();
            DateTime startedAt = DateTime.UtcNow;
            activity.AddProcess(processId, startedAt);

            Assert.AreEqual(1, activity.ActiveProcesses);
            Assert.AreEqual(1, activity.TotalProcessCount);
            Assert.AreEqual(0, activity.CompletionCount);
            Assert.AreEqual(0, activity.CompletedWithErrorCount);
        }

        [Test]
        public void CompletingAnExistingProcessDecrementsActiveCounter_ErrorCountIsZero()
        {
            string id = DailyActivity.GetIdFrom(DateTime.UtcNow);
            DailyActivity activity = new DailyActivity(id);

            Guid processId = Guid.NewGuid();
            DateTime startedAt = DateTime.UtcNow.AddSeconds(-10);
            activity.AddProcess(processId, startedAt);

            DateTime endedAt = DateTime.UtcNow;
            activity.EndProcess(processId, endedAt, false);

            Assert.AreEqual(0, activity.ActiveProcesses);
            Assert.AreEqual(1, activity.TotalProcessCount);
            Assert.AreEqual(1, activity.CompletionCount);
            Assert.AreEqual(0, activity.CompletedWithErrorCount);
        }

        [Test]
        public void CompletingAnExistingProcessDecrementsActiveCounter_ErrorCountIsNotZero()
        {
            string id = DailyActivity.GetIdFrom(DateTime.UtcNow);
            DailyActivity activity = new DailyActivity(id);

            Guid processId = Guid.NewGuid();
            DateTime startedAt = DateTime.UtcNow.AddSeconds(-10);
            activity.AddProcess(processId, startedAt);

            DateTime endedAt = DateTime.UtcNow;
            activity.EndProcess(processId, endedAt, true);

            Assert.AreEqual(0, activity.ActiveProcesses);
            Assert.AreEqual(1, activity.TotalProcessCount);
            Assert.AreEqual(1, activity.CompletionCount);
            Assert.AreEqual(1, activity.CompletedWithErrorCount);
        }

    }
}
