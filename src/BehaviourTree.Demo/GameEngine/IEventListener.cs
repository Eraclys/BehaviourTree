namespace BehaviourTree.Demo.GameEngine
{
    public interface IEventListener<in TEvent>
    {
        void Handle(Engine engine, TEvent @event);
    }
}