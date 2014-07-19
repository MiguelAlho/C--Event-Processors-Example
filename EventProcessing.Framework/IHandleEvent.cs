namespace EventProcessing.Framework
{
    public interface IHandleEvent<TEvent>
    {
        void Handle(TEvent @event);
    }
}
