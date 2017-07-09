using InputSimulator;

namespace BehaviourTree.InputBehaviours
{
    public sealed class BtSendKey : BaseBtBehaviour
    {
        private readonly Key _keyToSend;

        public BtSendKey(Key keyToSend)
        {
            _keyToSend = keyToSend;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            Input.Keyboard.KeyPress(_keyToSend);

            return BehaviourStatus.Succeeded;
        }
    }
}