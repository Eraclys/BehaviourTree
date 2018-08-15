using BehaviourTree.Demo.Ai.BT;
using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Components
{
    public sealed class BTBehaviourComponent : IComponent
    {
        public readonly IBehaviour<BtContext> BehaviourTree;

        public BTBehaviourComponent(IBehaviour<BtContext> behaviourTree)
        {
            BehaviourTree = behaviourTree;
        }
    }
}
