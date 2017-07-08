namespace BehaviourTree
{
    public interface IBehaviour
    {
        BehaviourStatus Status { get; }

        BehaviourStatus Tick(ElaspedTicks elaspedTicks);

        void Reset();
    }
}
