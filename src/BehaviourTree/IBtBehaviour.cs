namespace BehaviourTree
{
    public interface IBtBehaviour
    {
        BehaviourStatus Status { get; }

        BehaviourStatus Tick(ElaspedTicks elaspedTicks);

        void Reset();
    }
}
