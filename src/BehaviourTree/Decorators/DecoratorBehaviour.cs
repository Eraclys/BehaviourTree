namespace BehaviourTree.Decorators
{
    public abstract class DecoratorBehaviour : BaseBehaviour
    {
        protected IBehaviour Child { get; }

        protected DecoratorBehaviour(IBehaviour child)
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
    }
}
