using InputSimulator;

namespace BehaviourTree.InputBehaviours
{
    public sealed class BtSendKeys : BaseBtBehaviour
    {
        private readonly string _keysToSend;

        public BtSendKeys(string keysToSend)
        {
            _keysToSend = keysToSend;
        }

        protected override BehaviourStatus DoTick(BtContext context)
        {
            Input.Keyboard.SendKeys(_keysToSend);

            return BehaviourStatus.Succeeded;
        }
    }
}
