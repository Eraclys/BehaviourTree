using InputSimulator;

namespace BehaviourTree.InputBehaviours
{
    public sealed class SendKeys : BaseBtBehaviour
    {
        private readonly string _keysToSend;

        public SendKeys(string keysToSend)
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
