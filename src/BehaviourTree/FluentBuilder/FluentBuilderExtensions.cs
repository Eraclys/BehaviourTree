using System;
using BehaviourTree.Behaviours;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;

namespace BehaviourTree.FluentBuilder
{
    public static class FluentBuilderExtensions
    {
        public static FluentBuilder<TContext> Subtree<TContext>(
            this FluentBuilder<TContext> builder,
            IBehaviour<TContext> subBehaviour)
        {
            return builder.PushLeaf(() => subBehaviour);
        }

        public static FluentBuilder<TContext> Condition<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            Func<TContext, bool> condition)
        {
            return builder.PushLeaf(()=> new Condition<TContext>(name, condition));
        }

        public static FluentBuilder<TContext> Do<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            Func<TContext, BehaviourStatus> action)
        {
            return builder.PushLeaf(() => new ActionBehaviour<TContext>(name, action));
        }

        public static FluentBuilder<TContext> Wait<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            int waitTimeInMilliseconds) where TContext : IClock
        {
            return builder.PushLeaf(() => new Wait<TContext>(name, waitTimeInMilliseconds));
        }

        public static FluentBuilder<TContext> PrioritySelector<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.PushComposite(children => new PrioritySelector<TContext>(name, children));
        }

        public static FluentBuilder<TContext> PrioritySequence<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.PushComposite(children => new PrioritySequence<TContext>(name, children));
        }

        public static FluentBuilder<TContext> Selector<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.PushComposite(children => new Selector<TContext>(name, children));
        }

        public static FluentBuilder<TContext> Sequence<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.PushComposite(children => new Sequence<TContext>(name, children));
        }

        public static FluentBuilder<TContext> RandomSequence<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            IRandomProvider randomProvider = null)
        {
            return builder.PushComposite(children => new RandomSequence<TContext>(name, children, randomProvider));
        }

        public static FluentBuilder<TContext> RandomSelector<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            IRandomProvider randomProvider = null)
        {
            return builder.PushComposite(children => new RandomSelector<TContext>(name, children, randomProvider));
        }

        public static FluentBuilder<TContext> SimpleParallel<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            SimpleParallelPolicy policy = SimpleParallelPolicy.BothMustSucceed)
        {
            return builder.PushComposite(children => new SimpleParallel<TContext>(name, policy, children[0], children[1]));
        }

        public static FluentBuilder<TContext> AutoReset<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.PushComposite(children => new AutoReset<TContext>(name, children[0]));
        }

        public static FluentBuilder<TContext> Cooldown<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            int cooldownTimeInMilliseconds) where TContext : IClock
        {
            return builder.PushComposite(children => new Cooldown<TContext>(name, children[0], cooldownTimeInMilliseconds));
        }

        public static FluentBuilder<TContext> AlwaysFail<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.PushComposite(children => new Failer<TContext>(name, children[0]));
        }

        public static FluentBuilder<TContext> AlwaysSucceed<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.PushComposite(children => new Succeeder<TContext>(name, children[0]));
        }

        public static FluentBuilder<TContext> Invert<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.PushComposite(children => new Inverter<TContext>(name, children[0]));
        }

        public static FluentBuilder<TContext> LimitCallRate<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            int intervalInMilliseconds) where TContext : IClock
        {
            return builder.PushComposite(children => new RateLimiter<TContext>(name, children[0], intervalInMilliseconds));
        }

        public static FluentBuilder<TContext> Repeat<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            int repeatCount)
        {
            return builder.PushComposite(children => new Repeat<TContext>(name, children[0], repeatCount));
        }

        public static FluentBuilder<TContext> TimeLimit<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            int timeLimitInMilliseconds) where TContext : IClock
        {
            return builder.PushComposite(children => new TimeLimit<TContext>(name, children[0], timeLimitInMilliseconds));
        }

        public static FluentBuilder<TContext> UntilSuccess<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.PushComposite(children => new UntilSuccess<TContext>(name, children[0]));
        }

        public static FluentBuilder<TContext> UntilFailed<TContext>(
            this FluentBuilder<TContext> builder,
            string name)
        {
            return builder.PushComposite(children => new UntilFailed<TContext>(name, children[0]));
        }

        public static FluentBuilder<TContext> Random<TContext>(
            this FluentBuilder<TContext> builder,
            string name,
            double threshold,
            IRandomProvider randomProvider = null)
        {
            return builder.PushComposite(children => new Random<TContext>(name, children[0], threshold, randomProvider));
        }
    }
}