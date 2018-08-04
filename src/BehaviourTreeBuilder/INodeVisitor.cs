using BehaviourTree;
using BehaviourTreeBuilder.Nodes;

namespace BehaviourTreeBuilder
{
    public interface INodeToBehaviourMapper<TContext>
    {
        IBehaviour<TContext> Map(Node node);
    }
}