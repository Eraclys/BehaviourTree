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

        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, BtContext context)
        {
            Input.Keyboard.SendKeys(_keysToSend);

            return BehaviourStatus.Succeeded;
        }

        protected override void DoReset()
        {
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
