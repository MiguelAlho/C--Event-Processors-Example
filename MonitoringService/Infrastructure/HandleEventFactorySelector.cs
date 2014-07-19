using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using EventProcessing.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MonitoringService.Infrastructure
{
    public class HandleEventFactorySelector : DefaultTypedFactoryComponentSelector
    {
        protected override Func<IKernelInternal, IReleasePolicy, object> BuildFactoryComponent(
            MethodInfo method,
            string componentName,
            Type componentType,
            IDictionary additionalArguments)
        {
            return (k, s) => k.ResolveAll(componentType).Cast<dynamic>(); // as IEnumerable<dynamic>;
        }

        protected override string GetComponentName(MethodInfo method, object[] arguments)
        {
            return null;
        }

        protected override Type GetComponentType(MethodInfo method, object[] arguments)
        {
            var message = arguments[0];
            var handlerType = typeof(IHandleEvent<>).MakeGenericType(message.GetType());
            return handlerType;
        }
    }
}