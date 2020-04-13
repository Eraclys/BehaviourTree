namespace BehaviourTree.FluentBuilder
{
    public abstract class BehaviourBuilder<TContext>
    {
        public abstract IBehaviour<TContext> Build();
    }
}