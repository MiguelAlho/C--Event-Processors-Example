using EventProcessing.Framework.Dispatch;
using EventProcessing.Framework.Tests.Fakes;
using Events;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventProcessing.Framework.Tests.Dispatch
{
    [TestFixture]
    public class EventDispatcherTests
    {
        [Test]
        public void CanCreateInstanceOfEventDispatcher()
        {
            Mock<IHandlerFactory> factoryMock = new Mock<IHandlerFactory>();

            IEventDispatcher dispatcher = new EventDispatcher(factoryMock.Object);

            Assert.IsNotNull(dispatcher);

        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullFactoryThrowsExceptionInConstructor()
        {
            IEventDispatcher dispatcher = new EventDispatcher(null);
        }


        [Test]
        public void DispatcherDispatchesEventsToHandlers()
        {
            Mock<IHandleEvent<EventThatHappened>> handler1 = new Mock<IHandleEvent<EventThatHappened>>();
            handler1.Setup(o => o.Handle(It.IsAny<EventThatHappened>())).Verifiable();

            Mock<IHandleEvent<EventThatHappened>> handler2 = new Mock<IHandleEvent<EventThatHappened>>();
            handler2.Setup(o => o.Handle(It.IsAny<EventThatHappened>())).Verifiable();

            Mock<IHandlerFactory> factory = new Mock<IHandlerFactory>();
            factory.Setup(o => o.GetHandlersFor(It.IsAny<EventThatHappened>()))
                .Returns(new List<dynamic>()
                {
                    handler1.Object as dynamic, 
                    handler2.Object as dynamic
                });

            EventDispatcher dispatcher = new EventDispatcher(factory.Object);

            dispatcher.DispatchEvent(new EventThatHappened());

            handler1.Verify(mock => mock.Handle(It.IsAny<EventThatHappened>()), Times.Once);
            handler2.Verify(mock => mock.Handle(It.IsAny<EventThatHappened>()), Times.Once);
        }

        [Test]
        public void DispatcherDoesNothingForInexistentHandlers()
        {
            Mock<IHandlerFactory> factory = new Mock<IHandlerFactory>();
            factory.Setup(o => o.GetHandlersFor(It.IsAny<EventThatHappened>()))
                .Returns(null as IEnumerable<dynamic>);

            EventDispatcher dispatcher = new EventDispatcher(factory.Object);

            dispatcher.DispatchEvent(new EventThatHappened());
        }
    }
}
