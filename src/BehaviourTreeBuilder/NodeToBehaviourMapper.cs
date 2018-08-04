using System;
using System.Linq;
using BehaviourTree;
using BehaviourTree.Behaviours;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;
using BehaviourTreeBuilder.Nodes;
// ReSharper disable SuggestBaseTypeForParameter

namespace BehaviourTreeBuilder
{
    public class NodeToBehaviourMapper : INodeToBehaviourMapper
    {
        public IBehaviour Map(Node node)
        {
            switch (node)
            {
                case ActionNode n: return MapActionNode(n);
                case ConditionNode n: return MapConditionNode(n);
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
                case RateLimiterNode n: return MapRateLimiterNode(n);
                case RepeatNode n: return MapRepeatNode(n);
                case SubTreeNode n: return MapSubTreeNode(n);
                case SucceederNode n: return MapSucceederNode(n);
                case TimeLimitNode n: return MapTimeLimitNode(n);
                case UntilFailedNode n: return MapUntilFailedNode(n);
                case UntilSuccessNode n: return MapUntilSuccessNode(n);
                default: return MapUnknownNode(node);
            }
        }

        private SubTree MapSubTreeNode(SubTreeNode node)
        {
            return new SubTree(node.Name, node.SubBehaviour);
        }

        private UntilFailed MapUntilFailedNode(UntilFailedNode node)
        {
            return new UntilFailed(node.Name, Map(node.Child));
        }

        private UntilSuccess MapUntilSuccessNode(UntilSuccessNode node)
        {
            return new UntilSuccess(node.Name, Map(node.Child));
        }

        private TimeLimit MapTimeLimitNode(TimeLimitNode node)
        {
            return new TimeLimit(node.Name, Map(node.Child), node.TimeLimitInMilliseconds);
        }

        private Repeat MapRepeatNode(RepeatNode node)
        {
            return new Repeat(node.Name, Map(node.Child), node.RepeatCount);
        }

        private RateLimiter MapRateLimiterNode(RateLimiterNode node)
        {
            return new RateLimiter(node.Name, Map(node.Child), node.IntervalInMilliseconds);
        }

        private Inverter MapInverterNode(InverterNode node)
        {
            return new Inverter(node.Name, Map(node.Child));
        }

        private Succeeder MapSucceederNode(SucceederNode node)
        {
            return new Succeeder(node.Name, Map(node.Child));
        }

        private Failer MapFailerNode(FailerNode node)
        {
            return new Failer(node.Name, Map(node.Child));
        }

        private Cooldown MapCooldownNode(CooldownNode node)
        {
            return new Cooldown(node.Name, Map(node.Child), node.CooldownTimeInMilliseconds);
        }

        private AutoReset MapAutoResetNode(AutoResetNode node)
        {
            return new AutoReset(node.Name, Map(node.Child));
        }

        private SimpleParallel MapSimpleParallelNode(SimpleParallelNode node)
        {
            if (node.Children.Count != 2)
            {
                throw new ArgumentException("Simple parallel should have exactly two children nodes");
            }

            return new SimpleParallel(node.Name, node.Policy, Map(node.Children[0]), Map(node.Children[1]));
        }

        private Sequence MapSequenceNode(SequenceNode node)
        {
            return new Sequence(node.Name, node.Children.Select(Map).ToArray());
        }

        private Selector MapSelectorNode(SelectorNode node)
        {
            return new Selector(node.Name, node.Children.Select(Map).ToArray());
        }

        private PrioritySelector MapPrioritySelectorNode(PrioritySelectorNode node)
        {
            return new PrioritySelector(node.Name, node.Children.Select(Map).ToArray());
        }

        private PrioritySequence MapPrioritySequenceNode(PrioritySequenceNode node)
        {
            return new PrioritySequence(node.Name, node.Children.Select(Map).ToArray());
        }

        private static Wait MapWaitNode(WaitNode node)
        {
            return new Wait(node.WaitTimeInMilliseconds);
        }

        private static ActionBehaviour MapActionNode(ActionNode node)
        {
            return new ActionBehaviour(node.Name, node.Action);
        }

        private static Condition MapConditionNode(ConditionNode node)
        {
            return new Condition(node.Name, node.Predicate);
        }

        protected virtual IBehaviour MapUnknownNode(Node node)
        {
            throw new Exception($"Unkown node '{node.GetType().Name}' encountered. Extend NodeToBehaviourMapper to add your custom nodes");
        }
    }
}