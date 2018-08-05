using System.Linq;
using System.Text;
using BehaviourTree;
using BehaviourTree.Behaviours;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;

namespace BehaviourTreeBuilder.Tests
{
    public sealed class BehaviourTreeExpressionPrinter<TContext> :
        IVisitor<ActionBehaviour<TContext>>,
        IVisitor<Condition<TContext>>,
        IVisitor<Wait<TContext>>,
        IVisitor<CompositeBehaviour<TContext>>,
        IVisitor<PrioritySelector<TContext>>,
        IVisitor<PrioritySequence<TContext>>,
        IVisitor<Selector<TContext>>,
        IVisitor<Sequence<TContext>>,
        IVisitor<SimpleParallel<TContext>>,
        IVisitor<AutoReset<TContext>>,
        IVisitor<Cooldown<TContext>>,
        IVisitor<DecoratorBehaviour<TContext>>,
        IVisitor<Failer<TContext>>,
        IVisitor<Inverter<TContext>>,
        IVisitor<RateLimiter<TContext>>,
        IVisitor<Repeat<TContext>>,
        IVisitor<SubTree<TContext>>,
        IVisitor<Succeeder<TContext>>,
        IVisitor<TimeLimit<TContext>>,
        IVisitor<UntilFailed<TContext>>,
        IVisitor<UntilSuccess<TContext>>,
        IVisitor<IBehaviour<TContext>>

        where TContext : IClock
    {
        private readonly StringBuilder _sb = new StringBuilder();
        private int _depth;

        public override string ToString()
        {
            return _sb.ToString();
        }

        public void Visit(IBehaviour<TContext> obj)
        {
            Visit((dynamic)obj);
        }

        public void Visit(ActionBehaviour<TContext> obj)
        {
            PrintNode(obj);
        }

        public void Visit(Condition<TContext> obj)
        {
            PrintNode(obj);
        }

        public void Visit(Wait<TContext> obj)
        {
            PrintNode(obj, obj.WaitTimeInMilliseconds);
        }

        public void Visit(CompositeBehaviour<TContext> obj)
        {
            PrintNode(obj);
            VisitChildren(obj);
        }

        public void Visit(PrioritySelector<TContext> obj)
        {
            PrintNode(obj);
            VisitChildren(obj);
        }

        public void Visit(PrioritySequence<TContext> obj)
        {
            PrintNode(obj);
            VisitChildren(obj);
        }

        public void Visit(Selector<TContext> obj)
        {
            PrintNode(obj);
            VisitChildren(obj);
        }

        public void Visit(Sequence<TContext> obj)
        {
            PrintNode(obj);
            VisitChildren(obj);
        }

        public void Visit(SimpleParallel<TContext> obj)
        {
            PrintNode(obj, obj.Policy);
            VisitChildren(obj);
        }

        public void Visit(AutoReset<TContext> obj)
        {
            PrintNode(obj);
            VisitChild(obj);
        }

        public void Visit(Cooldown<TContext> obj)
        {
            PrintNode(obj, obj.CooldownTimeInMilliseconds);
            VisitChild(obj);
        }

        public void Visit(DecoratorBehaviour<TContext> obj)
        {
            PrintNode(obj);
            VisitChild(obj);
        }

        public void Visit(Failer<TContext> obj)
        {
            PrintNode(obj);
            VisitChild(obj);
        }

        public void Visit(Inverter<TContext> obj)
        {
            PrintNode(obj);
            VisitChild(obj);
        }

        public void Visit(RateLimiter<TContext> obj)
        {
            PrintNode(obj, obj.IntervalInMilliseconds);
            VisitChild(obj);
        }

        public void Visit(Repeat<TContext> obj)
        {
            PrintNode(obj, obj.RepeatCount);
            VisitChild(obj);
        }

        public void Visit(SubTree<TContext> obj)
        {
            PrintNode(obj);
            VisitChild(obj);
        }

        public void Visit(Succeeder<TContext> obj)
        {
            PrintNode(obj);
            VisitChild(obj);
        }

        public void Visit(TimeLimit<TContext> obj)
        {
            PrintNode(obj, obj.TimeLimitInMilliseconds);
            VisitChild(obj);
        }

        public void Visit(UntilFailed<TContext> obj)
        {
            PrintNode(obj);
            VisitChild(obj);
        }

        public void Visit(UntilSuccess<TContext> obj)
        {
            PrintNode(obj);
            VisitChild(obj);
        }

        private void VisitChildren(CompositeBehaviour<TContext> obj)
        {
            _depth++;

            foreach (var child in obj.Children)
            {
                child.Accept(this);
            }

            _depth--;
        }

        private void VisitChild(DecoratorBehaviour<TContext> obj)
        {
            _depth++;

            obj.Child.Accept(this);

            _depth--;
        }

        private void PrintNode(IBehaviour<TContext> obj, params object[] parameters)
        {
            var paramsExpression = parameters.Any() ? $"({string.Join(",", parameters)})" : string.Empty;
            var nodeExpression =  $"{GetIndentation()}{obj.Name} {paramsExpression}";

            _sb.AppendLine(nodeExpression);
        }

        private string GetIndentation()
        {
            return string.Join(string.Empty, Enumerable.Repeat("   ", _depth));
        }
    }
}
