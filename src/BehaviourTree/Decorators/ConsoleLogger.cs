namespace BehaviourTree.Decorators
{
    public sealed class ConsoleLogger<TContext> : DecoratorBehaviour<TContext> where TContext : IClock
    {
        public ConsoleLogger(string name, IBehaviour<TContext> child) : base(name, child)
        {

        }

        protected override BehaviourStatus Update(TContext context)
        {
            var childStatus = Child.Tick(context);

            BehaviourTreeConsoleLogger<TContext>.LogToConsole(Child);

            return childStatus;
        }
    }
}
