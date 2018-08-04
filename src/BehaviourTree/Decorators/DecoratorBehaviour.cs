namespace BehaviourTree.Decorators
{
    public abstract class DecoratorBehaviour : BaseBehaviour
    {
        protected readonly IBehaviour Child;

        protected DecoratorBehaviour(string name, IBehaviour child) : base(name)
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
