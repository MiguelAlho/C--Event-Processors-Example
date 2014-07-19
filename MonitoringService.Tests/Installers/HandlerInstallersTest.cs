using Castle.Windsor;
using DomainA.Events;
using DomainAEventProcessors.Entities;
using DomainAEventProcessors.Interfaces;
using DomainAEventProcessors.Processors;
using EventProcessing.Framework;
using MonitoringService.Installers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MonitoringService.Tests.Installers
{
    [TestFixture]
    public class HandlerInstallersTest
    {
        [Test]
        public void CanInstallHandlersInTheContainer()
        {
            using (IWindsorContainer container = new WindsorContainer())
            {
                //This line just forces the domain events lib to be loaded, and handlers available in the app domain.
                IProcessEvents proc = new DailyActivityEventProcessor(new Mock<IRepository<DailyActivity>>().Object);

                container.Install(new HandlerInstaller(), new RepositoryInstaller());

                ProcessStarted started = new ProcessStarted(Guid.NewGuid(), Guid.NewGuid());
                var handlerType = typeof (IHandleEvent<>).MakeGenericType(started.GetType());

                IEnumerable<dynamic> handlers = container.ResolveAll(handlerType).Cast<dynamic>();

                Assert.IsNotNull(handlers);
                Assert.AreEqual(2, handlers.Count());

                var handler = handlers.FirstOrDefault(o => o.GetType() == typeof (DailyActivityEventProcessor));
                Assert.IsNotNull(handler);


            }


        }

    }
}
