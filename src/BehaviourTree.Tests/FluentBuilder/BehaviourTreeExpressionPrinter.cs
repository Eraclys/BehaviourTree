using System;
using System.Linq;
using BehaviourTree.Behaviours;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;

namespace BehaviourTree.Tests.FluentBuilder
{
    public sealed class BehaviourTreeExpressionPrinter<TContext>
        where TContext : IClock
    {
        public static string GetExpression(IBehaviour<TContext> obj)
        {
            return GetExpression(obj, 0);
        }

        private static string GetExpression(IBehaviour<TContext> obj, int depth)
        {
            return GetExpression((dynamic)obj, depth);
        }

        private static string GetExpression(CompositeBehaviour<TContext> obj, int depth)
        {
            var expression = InternalGetExpression(obj, depth);

            var childDepth = depth + 1;

            foreach (var child in obj.Children)
            {
                expression += GetExpression(child, childDepth);
            }

            return expression;
        }

        private static string GetExpression(DecoratorBehaviour<TContext> obj, int depth)
        {
            return
                InternalGetExpression(obj, depth) +
                GetExpression(obj.Child, ++depth);
        }

        private static string GetExpression(BaseBehaviour<TContext> obj, int depth)
        {
            return InternalGetExpression(obj, depth);
        }

        private static string GetExpression(Wait<TContext> obj, int depth)
        {
            return InternalGetExpression(obj, depth, obj.WaitTimeInMilliseconds);
        }

        private static string GetExpression(Cooldown<TContext> obj, int depth)
        {
            return
                InternalGetExpression(obj, depth, obj.CooldownTimeInMilliseconds) +
                GetExpression(obj.Child, ++depth);
        }

        private static string GetExpression(RateLimiter<TContext> obj, int depth)
        {
            return
                InternalGetExpression(obj, depth, obj.IntervalInMilliseconds) +
                GetExpression(obj.Child, ++depth);
        }

        private static string GetExpression(Repeat<TContext> obj, int depth)
        {
            return
                InternalGetExpression(obj, depth, obj.RepeatCount) +
                GetExpression(obj.Child, ++depth);
        }

        private static string GetExpression(Random<TContext> obj, int depth)
        {
            return
                InternalGetExpression(obj, depth, obj.Threshold) +
                GetExpression(obj.Child, ++depth);
        }

        private static string GetExpression(TimeLimit<TContext> obj, int depth)
        {
            return
                InternalGetExpression(obj, depth, obj.TimeLimitInMilliseconds) +
                GetExpression(obj.Child, ++depth);
        }

        private static string InternalGetExpression(IBehaviour<TContext> obj, int depth, params object[] parameters)
        {
            var paramsExpression = parameters.Any() ? $"({string.Join(",", parameters)})" : string.Empty;
            return  $"{GetIndentation(depth)}{GetName(obj)} {paramsExpression}{Environment.NewLine}";
        }

        private static string GetIndentation(int depth)
        {
            return string.Join(string.Empty, Enumerable.Repeat("   ", depth));
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
