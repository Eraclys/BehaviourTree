using System;
using BehaviourTree;
using BehaviourTreeBuilder.Nodes;

namespace BehaviourTreeBuilder
{
    public static class FluentBuilderExtensions
    {
        public static FluentBuilder Subtree(
            this FluentBuilder builder,
            string name,
            IBehaviour subBehaviour)
        {
            return builder.Push(new SubTreeNode(subBehaviour) { Name = name });
        }

        public static FluentBuilder Condition(
            this FluentBuilder builder,
            string name,
            Func<BtContext, bool> condition)
        {
            return builder.Push(new ConditionNode(condition) {Name = name});
        }

        public static FluentBuilder Do(
            this FluentBuilder builder,
            string name,
            Func<BtContext, BehaviourStatus> action)
        {
            return builder.Push(new ActionNode(action) {Name = name});
        }

        public static FluentBuilder Wait(
            this FluentBuilder builder,
            string name,
            int waitTimeInMilliseconds)
        {
            return builder.Push(new WaitNode(waitTimeInMilliseconds){Name = name});
        }

        public static FluentBuilder PrioritySelector(
            this FluentBuilder builder,
            string name)
        {
            return builder.Push(new PrioritySelectorNode {Name = name});
        }

        public static FluentBuilder PrioritySequence(
            this FluentBuilder builder,
            string name)
        {
            return builder.Push(new PrioritySequenceNode {Name = name});
        }

        public static FluentBuilder Selector(
            this FluentBuilder builder,
            string name)
        {
            return builder.Push(new SelectorNode {Name = name});
        }

        public static FluentBuilder Sequence(
            this FluentBuilder builder,
            string name)
        {
            return builder.Push(new SequenceNode {Name = name});
        }

        public static FluentBuilder SimpleParallel(
            this FluentBuilder builder,
            string name,
            BehaviourTree.Composites.SimpleParallel.Policy policy = BehaviourTree.Composites.SimpleParallel.Policy.BothMustSucceed)
        {
            return builder.Push(new SimpleParallelNode(policy) {Name = name});
        }

        public static FluentBuilder AutoReset(
            this FluentBuilder builder,
            string name)
        {
            return builder.Push(new AutoResetNode {Name = name});
        }

        public static FluentBuilder Cooldown(
            this FluentBuilder builder,
            string name,
            int cooldownTimeInMilliseconds)
        {
            return builder.Push(new CooldownNode(cooldownTimeInMilliseconds) { Name = name });
        }

        public static FluentBuilder AlwaysFail(
            this FluentBuilder builder,
            string name)
        {
            return builder.Push(new FailerNode { Name = name });
        }

        public static FluentBuilder AlwaysSucceed(
            this FluentBuilder builder,
            string name)
        {
            return builder.Push(new SucceederNode { Name = name });
        }

        public static FluentBuilder Invert(
            this FluentBuilder builder,
            string name)
        {
            return builder.Push(new InverterNode { Name = name });
        }

        public static FluentBuilder LimitCallRate(
            this FluentBuilder builder,
            string name,
            int intervalInMilliseconds)
        {
            return builder.Push(new RateLimiterNode(intervalInMilliseconds) { Name = name });
        }

        public static FluentBuilder Repeat(
            this FluentBuilder builder,
            string name,
            int repeatCount)
        {
            return builder.Push(new RepeatNode(repeatCount) { Name = name });
        }

        public static FluentBuilder TimeLimit(
            this FluentBuilder builder,
            string name,
            int timeLimitInMilliseconds)
        {
            return builder.Push(new TimeLimitNode(timeLimitInMilliseconds) { Name = name });
        }

        public static FluentBuilder UntilSuccess(
            this FluentBuilder builder,
            string name)
        {
            return builder.Push(new UntilSuccessNode { Name = name });
        }

        public static FluentBuilder UntilFailed(
            this FluentBuilder builder,
            string name)
        {
            return builder.Push(new UntilFailedNode { Name = name });
        }
    }
}