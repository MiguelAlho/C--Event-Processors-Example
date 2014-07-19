using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel;
using EventProcessing.Framework;
using MonitoringService.Infrastructure;
using MonitoringService.Tests.Fakes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events;

namespace MonitoringService.Tests.Infrastructure
{
    [TestFixture]
    public class HandleEventFactorySelectorTests
    {
        [Test]
        public void CanCreateInstanceOfHadleEventFactorySelector()
        {
            HandleEventFactorySelector selector = new HandleEventFactorySelector();

            Assert.IsNotNull(selector);
            Assert.IsInstanceOf<DefaultTypedFactoryComponentSelector>(selector);
        }

        [Test]
        public void DoesNotUseNameForTypeSelection()
        {
            SelectorAccess selector = new SelectorAccess();
            var name = selector.CallGetCompentenName(
                new DynamicMethod("Handle", typeof (void), new Type[] { typeof(Event)}),
                new object[] {});

            Assert.IsNull(name);
        }

        [Test]
        public void DoesUseArgumentTypeForTypeSelection()
        {
            SelectorAccess selector = new SelectorAccess();
            var type = selector.CallGetCompentenType(
                new DynamicMethod("Handle", typeof(void), new Type[] { typeof(Event) }),
                new object[] { new EventThatHappened() });

            Assert.IsNotNull(type);
            Assert.AreEqual(typeof(IHandleEvent<EventThatHappened>), type);
        }

        [Test]
        public void BuildFactoryComponentReturnsFunctor()
        {
            SelectorAccess selector = new SelectorAccess();
            var func = selector.CallBuildFactoryComponent(
                new DynamicMethod("Handle", typeof(void), new Type[] { typeof(Event) }),
                "test",
                typeof(IHandleEvent<EventThatHappened>),
                null);

            Assert.IsNotNull(func);
            Assert.IsInstanceOf<Func<IKernelInternal, IReleasePolicy, object>>(func);
        }

        public class SelectorAccess : HandleEventFactorySelector
        {
            public Func<IKernelInternal, IReleasePolicy, object> CallBuildFactoryComponent(
                MethodInfo method,
                string componentName,
                Type componentType,
                IDictionary additionalArguments)
            {
                return base.BuildFactoryComponent(method, componentName, componentType, additionalArguments);
            }

            public string CallGetCompentenName(MethodInfo method, object[] arguments)
            {
                return base.GetComponentName(method, arguments);
            }

            public Type CallGetCompentenType(MethodInfo method, object[] arguments)
            {
                return base.GetComponentType(method, arguments);
            }
        }
    }
}
