using Castle.Facilities.TypedFactory;
using Castle.Facilities.WcfIntegration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonitoringService.Infrastructure
{
    public static class ContainerManager
    {
        private static IWindsorContainer _container;

        static ContainerManager()
        {
            _container = new WindsorContainer()
                    .AddFacility<WcfFacility>()
                    .AddFacility<TypedFactoryFacility>();

           
        }

        public static void Install()
        {
            _container.Install(FromAssembly.InThisApplication());
        }

        public static IWindsorContainer Container
        {
            get
            {
                return _container;
            }

        }
    }
}