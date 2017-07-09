namespace BehaviourTree
{
    public interface IBtComposite : IBtBehaviour
    {
        IBtBehaviour[] Children { get; }
    }
}