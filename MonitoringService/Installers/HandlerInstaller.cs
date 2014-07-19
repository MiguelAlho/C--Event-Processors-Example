using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using EventProcessing.Framework;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MonitoringService.Installers
{
    public class HandlerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ///equivalent to SimpleInjector's RegisterManyForOpenGeneric:
            ///container.RegisterManyForOpenGeneric( 
            ///     typeof(IHandler<>), 
            ///     AppDomain.CurrentDomain.GetAssemblies());
            /// ref: http://simpleinjector.codeplex.com/wikipage?title=Castle%20Windsor

            container.Register(AllTypes.From(
                AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => !a.IsDynamic) //dynamic assemblys don't export types...
                    .SelectMany(a => a.GetExportedTypes()))
                .BasedOn(typeof (IHandleEvent<>))
                .Unless(t => t.IsGenericTypeDefinition)
                .WithService.Select((_, baseTypes) =>
                {
                    return
                        from t in baseTypes
                        where t.IsGenericType
                        let td = t.GetGenericTypeDefinition()
                        where td == typeof (IHandleEvent<>)
                        select t;
                }));
            //.Configure(c => c.LifestylePerWebRequest()));

            //load all eventTypes
            //var knownMonitoringEvents = Assembly.GetAssembly(typeof (WorkOrderRequested))
            //    .GetExportedTypes()
            //    .Where(o => typeof(MonitoringEvent).IsAssignableFrom(o))
            //    .ToList();
        }
    }
}