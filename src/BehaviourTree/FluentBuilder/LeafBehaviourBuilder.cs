namespace BehaviourTree.FluentBuilder
{
    public sealed class LeafBehaviourBuilder<TContext> : BehaviourBuilder<TContext>
    {
        public CreateBehaviour<TContext> Factory { get; set; }
        public override IBehaviour<TContext> Build() => Factory();
    }
}