using BehaviourTree;
using BehaviourTreeBuilder.Nodes;

namespace BehaviourTreeBuilder
{
    public interface INodeToBehaviourMapper
    {
        IBehaviour Map(Node node);
    }
}