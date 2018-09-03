using System;
using System.Linq;
using BehaviourTree.Behaviours;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;
using BehaviourTree.FluentBuilder.Nodes;

// ReSharper disable SuggestBaseTypeForParameter

namespace BehaviourTree.FluentBuilder
{
    public class NodeToBehaviourMapper<TContext> : INodeToBehaviourMapper<TContext> 
        where TContext : IClock, IRandomProvider
    {
        public IBehaviour<TContext> Map(Node node)
        {
            switch (node)
            {
                case ActionNode<TContext> n: return MapActionNode(n);
                case ConditionNode<TContext> n: return MapConditionNode(n);
                case WaitNode n: return MapWaitNode(n);
                case PrioritySelectorNode n: return MapPrioritySelectorNode(n);
                case PrioritySequenceNode n: return MapPrioritySequenceNode(n);
                case SelectorNode n: return MapSelectorNode(n);
                case SequenceNode n: return MapSequenceNode(n);
                case SimpleParallelNode n: return MapSimpleParallelNode(n);
                case AutoResetNode n: return MapAutoResetNode(n);
                case CooldownNode n: return MapCooldownNode(n);
                case FailerNode n: return MapFailerNode(n);
                case InverterNode n: return MapInverterNode(n);
                case RandomNode n: return MapRandomNode(n);
                case RateLimiterNode n: return MapRateLimiterNode(n);
                case RepeatNode n: return MapRepeatNode(n);
                case SubTreeNode<TContext> n: return n.SubBehaviour;
                case SucceederNode n: return MapSucceederNode(n);
                case TimeLimitNode n: return MapTimeLimitNode(n);
                case UntilFailedNode n: return MapUntilFailedNode(n);
                case UntilSuccessNode n: return MapUntilSuccessNode(n);
                default: return MapUnknownNode(node);
            }
        }

        private Random<TContext> MapRandomNode(RandomNode node)
        {
            return new Random<TContext>(node.Name, Map(node.Child), node.Threshold);
        }

        private UntilFailed<TContext> MapUntilFailedNode(UntilFailedNode node)
        {
            return new UntilFailed<TContext>(node.Name, Map(node.Child));
        }

        private UntilSuccess<TContext> MapUntilSuccessNode(UntilSuccessNode node)
        {
            return new UntilSuccess<TContext>(node.Name, Map(node.Child));
        }

        private TimeLimit<TContext> MapTimeLimitNode(TimeLimitNode node)
        {
            return new TimeLimit<TContext>(node.Name, Map(node.Child), node.TimeLimitInMilliseconds);
        }

        private Repeat<TContext> MapRepeatNode(RepeatNode node)
        {
            return new Repeat<TContext>(node.Name, Map(node.Child), node.RepeatCount);
        }

        private RateLimiter<TContext> MapRateLimiterNode(RateLimiterNode node)
        {
            return new RateLimiter<TContext>(node.Name, Map(node.Child), node.IntervalInMilliseconds);
        }

        private Inverter<TContext> MapInverterNode(InverterNode node)
        {
            return new Inverter<TContext>(node.Name, Map(node.Child));
        }

        private Succeeder<TContext> MapSucceederNode(SucceederNode node)
        {
            return new Succeeder<TContext>(node.Name, Map(node.Child));
        }

        private Failer<TContext> MapFailerNode(FailerNode node)
        {
            return new Failer<TContext>(node.Name, Map(node.Child));
        }

        private Cooldown<TContext> MapCooldownNode(CooldownNode node)
        {
            return new Cooldown<TContext>(node.Name, Map(node.Child), node.CooldownTimeInMilliseconds);
        }

        private AutoReset<TContext> MapAutoResetNode(AutoResetNode node)
        {
            return new AutoReset<TContext>(node.Name, Map(node.Child));
        }

        private SimpleParallel<TContext> MapSimpleParallelNode(SimpleParallelNode node)
        {
            if (node.Children.Count != 2)
            {
                throw new ArgumentException("Simple parallel should have exactly two children nodes");
            }

            return new SimpleParallel<TContext>(node.Name, node.Policy, Map(node.Children[0]), Map(node.Children[1]));
        }

        private Sequence<TContext> MapSequenceNode(SequenceNode node)
        {
            return new Sequence<TContext>(node.Name, node.Children.Select(Map).ToArray());
        }

        private Selector<TContext> MapSelectorNode(SelectorNode node)
        {
            return new Selector<TContext>(node.Name, node.Children.Select(Map).ToArray());
        }

        private PrioritySelector<TContext> MapPrioritySelectorNode(PrioritySelectorNode node)
        {
            return new PrioritySelector<TContext>(node.Name, node.Children.Select(Map).ToArray());
        }

        private PrioritySequence<TContext> MapPrioritySequenceNode(PrioritySequenceNode node)
        {
            return new PrioritySequence<TContext>(node.Name, node.Children.Select(Map).ToArray());
        }

        private static Wait<TContext> MapWaitNode(WaitNode node)
        {
            return new Wait<TContext>(node.WaitTimeInMilliseconds);
        }

        private static ActionBehaviour<TContext> MapActionNode(ActionNode<TContext> node)
        {
            return new ActionBehaviour<TContext>(node.Name, node.Action);
        }

        private static Condition<TContext> MapConditionNode(ConditionNode<TContext> node)
        {
            return new Condition<TContext>(node.Name, node.Predicate);
        }

        protected virtual IBehaviour<TContext> MapUnknownNode(Node node)
        {
            throw new Exception($"Unkown node '{node.GetType().Name}' encountered. Extend NodeToBehaviourMapper to add your custom nodes");
        }
    }
}