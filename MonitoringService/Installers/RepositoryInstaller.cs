using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using DomainAEventProcessors.Interfaces;
using MonitoringService.Infrastructure;
using Castle.MicroKernel.SubSystems.Configuration;

namespace MonitoringService.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IRepository<>))
                    .ImplementedBy(typeof(InMemoryRepository<>))
                );
        }
    }
}