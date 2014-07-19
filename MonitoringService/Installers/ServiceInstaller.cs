using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using EventProcessing.Framework.Dispatch;
using EventProcessing.Framework.Tests.Dispatch;
using Castle.Facilities.TypedFactory;
using MonitoringService.Infrastructure;

namespace MonitoringService.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMonitor>()
                    .ImplementedBy<MonitorService>()
                    .Named("MonitorService")
                    .LifeStyle.HybridPerWebRequestPerThread()
                );

            container.Register(
                Component.For<IEventDispatcher>()
                    .ImplementedBy<EventDispatcher>()
                );


            container.Register(
                Component.For<ITypedFactoryComponentSelector>()
                    .ImplementedBy<HandleEventFactorySelector>());

            container.Register(
                Component.For<EventProcessing.Framework.Tests.Dispatch.IHandlerFactory>()
                    .AsFactory(c => c.SelectedWith<HandleEventFactorySelector>())
                );


        }
    }
}