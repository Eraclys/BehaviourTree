namespace BehaviourTree.FluentBuilder
{
    public delegate IBehaviour<TContext> CreateCompositeBehaviour<TContext>(IBehaviour<TContext>[] children);
}