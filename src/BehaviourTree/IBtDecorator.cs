namespace BehaviourTree
{
    public interface IBtDecorator : IBtBehaviour
    {
        IBtBehaviour Child { get; }
    }
}