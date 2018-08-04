using System;

namespace BehaviourTree.Decorators
{
    public sealed class Repeat : DecoratorBehaviour
    {
        private int _repeatCount;
        public int Counter { get; private set; }

        public Repeat(IBehaviour child, int repeatCount) : this("Repeat", child, repeatCount)
        {
        }

        public Repeat(string name, IBehaviour child, int repeatCount) : base(name, child)
        {
            if (repeatCount < 1)
            {
                throw new ArgumentException("repeatCount must be at least one", nameof(repeatCount));
            }

            _repeatCount = repeatCount;
        }

        protected override BehaviourStatus Update(BtContext context)
        {
            var childStatus = Child.Tick(context);

            if (childStatus == BehaviourStatus.Succeeded)
            {
                Counter++;

                if (Counter < _repeatCount)
                {
                    return BehaviourStatus.Running;
                }
            }

            return childStatus;
        }

        protected override void OnTerminate(BehaviourStatus status)
        {
            Counter = 0;
        }

        protected override void DoReset(BehaviourStatus status)
        {
            Counter = 0;
            base.DoReset(status);
        }
    }
}
