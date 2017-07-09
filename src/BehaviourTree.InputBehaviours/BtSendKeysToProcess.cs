using BehaviourTree.Composites;

namespace BehaviourTree.InputBehaviours
{
    public sealed class BtSendKeysToProcess : BtSubTree
    {
        public BtSendKeysToProcess(string instanceName, string keysToSend)
        {
            var behaviour =
                new BtSequence(
                    new BtSetForegroundWindow(instanceName),
                    new BtSendKeys(keysToSend)
                );

            SetBehaviour(behaviour);
        }
    }
}