using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventProcessing.Framework;
using MonitoringService.Infrastructure;
using MonitoringService.Tests.Fakes;
using NUnit.Framework;
using DomainAEventProcessors.Interfaces;

namespace MonitoringService.Tests.Infrastructure
{
    [TestFixture]
    public class InMemoryRepositoryTests
    {
        [Test]
        public void CanCreateInstanceOfRepository()
        {
            IRepository<SomeEntity> repo = new InMemoryRepository<SomeEntity>();

            Assert.IsNotNull(repo);
            Assert.IsInstanceOf<InMemoryRepository<SomeEntity>>(repo);
        }

        [Test]
        public void CanSaveAndGetAnEntity()
        {
            IRepository<SomeEntity> repo = new InMemoryRepository<SomeEntity>();
            SomeEntity entity = new SomeEntity("a");

            var unstoredEntity = repo.Get("a");
            Assert.IsNull(unstoredEntity);

            repo.Save(entity);

            var storedEntity = repo.Get("a");
            Assert.IsNotNull(storedEntity);
            Assert.AreSame(entity, storedEntity);

        }
    }
}
