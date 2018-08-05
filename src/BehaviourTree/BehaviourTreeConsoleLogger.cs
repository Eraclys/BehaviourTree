using System;
using System.Linq;
using BehaviourTree.Behaviours;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;

namespace BehaviourTree
{
    public sealed class BehaviourTreeConsoleLogger<TContext> :
      IVisitor<Condition<TContext>>,
      IVisitor<BaseBehaviour<TContext>>,
      IVisitor<CompositeBehaviour<TContext>>,
      IVisitor<DecoratorBehaviour<TContext>>,
      IVisitor<IBehaviour<TContext>>
      where TContext : IClock
    {
        private int _depth;

        public void Visit(IBehaviour<TContext> obj)
        {
            Visit((dynamic)obj);
        }

        public void Visit(Condition<TContext> obj)
        {
            PrintNode(obj);
        }

        public void Visit(BaseBehaviour<TContext> obj)
        {
            PrintNode(obj);
        }

        public void Visit(CompositeBehaviour<TContext> obj)
        {
            PrintNode(obj);
            VisitChildren(obj);
        }

        public void Visit(DecoratorBehaviour<TContext> obj)
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

        private void PrintNode(IBehaviour<TContext> obj)
        {
            var indentation = GetIndentation();
            var name = GetName(obj);
            var color = GetColor(obj.Status);

            Console.ForegroundColor = color;

            var nodeExpression = $"{indentation}{name}";

            Console.WriteLine(nodeExpression);

            Console.ResetColor();
        }

        private static ConsoleColor GetColor(BehaviourStatus status)
        {
            switch (status)
            {
                case BehaviourStatus.Ready: return ConsoleColor.Blue;
                case BehaviourStatus.Running: return ConsoleColor.Gray;
                case BehaviourStatus.Succeeded: return ConsoleColor.Green;
                case BehaviourStatus.Failed: return ConsoleColor.Red;
                default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        private string GetIndentation()
        {
            return string.Join(string.Empty, Enumerable.Repeat("   ", _depth));
        }

        private static string GetName(IBehaviour<TContext> obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.Name))
            {
                return obj.Name;
            }

            var type = obj.GetType();

            // TODO: check for generic

            return type.Name;
        }
    }
}
