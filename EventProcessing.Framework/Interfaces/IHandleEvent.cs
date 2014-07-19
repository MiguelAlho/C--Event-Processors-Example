using Events;

namespace EventProcessing.Framework
{
    public interface IHandleEvent<TEvent> where TEvent : Event
    {
        void Handle(TEvent @event);
    }
}
