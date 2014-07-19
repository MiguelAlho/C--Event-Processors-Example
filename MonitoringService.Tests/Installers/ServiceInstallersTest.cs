using Castle.Facilities.TypedFactory;
using Castle.Windsor;
using EventProcessing.Framework.Dispatch;
using EventProcessing.Framework.Tests.Dispatch;
using MonitoringService.Installers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Facilities.TypedFactory;

namespace MonitoringService.Tests.Installers
{
    [TestFixture]
    public class ServiceInstallersTest
    {
        [Test]
        public void CanInstallServicesInTheContainer()
        {
            IWindsorContainer container = new WindsorContainer()
                .AddFacility<TypedFactoryFacility>();

            container.Install(new ServiceInstaller());

            IMonitor monitor = container.Resolve<IMonitor>();
            IMonitor monitor2 = container.Resolve<IMonitor>();
            
            Assert.IsInstanceOf<MonitorService>(monitor);
            Assert.AreSame(monitor, monitor2);

            container.Release(monitor);
            container.Release(monitor2);

            //check dependencies also
            IEventDispatcher dispatcher1 = container.Resolve<IEventDispatcher>();
            IEventDispatcher dispatcher2 = container.Resolve<IEventDispatcher>();

            Assert.IsInstanceOf<EventDispatcher>(dispatcher1);
            Assert.AreSame(dispatcher1, dispatcher2);

            container.Release(dispatcher1);
            container.Release(dispatcher2);

            
        }

    }
}
