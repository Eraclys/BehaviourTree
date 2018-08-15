namespace BehaviourTree.Demo.GameEngine
{
    public interface IEventManager
    {
        void PublishEvent<TEvent>(TEvent @event);
        void SubscribeToEvent<TEvent>(IEventListener<TEvent> eventListener);
        void UnsubscribeFromEvent<TEvent>(IEventListener<TEvent> eventListener);
    }
}