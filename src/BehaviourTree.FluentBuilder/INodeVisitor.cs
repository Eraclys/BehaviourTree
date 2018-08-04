using BehaviourTree.FluentBuilder.Nodes;

namespace BehaviourTree.FluentBuilder
{
    public interface INodeToBehaviourMapper<TContext>
    {
        IBehaviour<TContext> Map(Node node);
    }
}