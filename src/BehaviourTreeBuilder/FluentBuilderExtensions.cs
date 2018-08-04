using System;
using BehaviourTree;
using BehaviourTreeBuilder.Nodes;

namespace BehaviourTreeBuilder
{
    public static class FluentBuilderExtensions
    {
        public static FluentBuilder<TContext> Subtree<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            IBehaviour<TContext> subBehaviour)
        {
            return builder.Push(new SubTreeNode<TContext>(subBehaviour) { Name = name });
        }

        public static FluentBuilder<TContext> Condition<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            Func<TContext, bool> condition)
        {
            return builder.Push(new ConditionNode<TContext>(condition) {Name = name});
        }

        public static FluentBuilder<TContext> Do<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            Func<TContext, BehaviourStatus> action)
        {
            return builder.Push(new ActionNode<TContext>(action) {Name = name});
        }

        public static FluentBuilder<TContext> Wait<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            int waitTimeInMilliseconds)
        {
            return builder.Push(new WaitNode(waitTimeInMilliseconds){Name = name});
        }

        public static FluentBuilder<TContext> PrioritySelector<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.Push(new PrioritySelectorNode { Name = name});
        }

        public static FluentBuilder<TContext> PrioritySequence<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.Push(new PrioritySequenceNode { Name = name});
        }

        public static FluentBuilder<TContext> Selector<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.Push(new SelectorNode { Name = name});
        }

        public static FluentBuilder<TContext> Sequence<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.Push(new SequenceNode { Name = name});
        }

        public static FluentBuilder<TContext> SimpleParallel<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            BehaviourTree.Composites.SimpleParallelPolicy policy = BehaviourTree.Composites.SimpleParallelPolicy.BothMustSucceed)
        {
            return builder.Push(new SimpleParallelNode(policy) {Name = name});
        }

        public static FluentBuilder<TContext> AutoReset<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.Push(new AutoResetNode { Name = name});
        }

        public static FluentBuilder<TContext> Cooldown<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            int cooldownTimeInMilliseconds)
        {
            return builder.Push(new CooldownNode(cooldownTimeInMilliseconds) { Name = name });
        }

        public static FluentBuilder<TContext> AlwaysFail<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.Push(new FailerNode { Name = name });
        }

        public static FluentBuilder<TContext> AlwaysSucceed<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.Push(new SucceederNode { Name = name });
        }

        public static FluentBuilder<TContext> Invert<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.Push(new InverterNode { Name = name });
        }

        public static FluentBuilder<TContext> LimitCallRate<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            int intervalInMilliseconds)
        {
            return builder.Push(new RateLimiterNode(intervalInMilliseconds) { Name = name });
        }

        public static FluentBuilder<TContext> Repeat<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            int repeatCount)
        {
            return builder.Push(new RepeatNode(repeatCount) { Name = name });
        }

        public static FluentBuilder<TContext> TimeLimit<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            int timeLimitInMilliseconds)
        {
            return builder.Push(new TimeLimitNode(timeLimitInMilliseconds) { Name = name });
        }

        public static FluentBuilder<TContext> UntilSuccess<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.Push(new UntilSuccessNode { Name = name });
        }

        public static FluentBuilder<TContext> UntilFailed<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.Push(new UntilFailedNode { Name = name });
        }
    }
}