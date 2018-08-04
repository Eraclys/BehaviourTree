namespace BehaviourTree.Decorators
{
    public abstract class DecoratorBehaviour<TContext> : BaseBehaviour<TContext>
    {
        protected readonly IBehaviour<TContext> Child;

        protected DecoratorBehaviour(string name, IBehaviour<TContext> child) : base(name)
        {
            Child = child;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Child.Dispose();
            }
        }

        protected override void DoReset(BehaviourStatus status)
        {
            Child.Reset();
        }
    }
}
