using System;
using System.Diagnostics;

namespace BehaviourTree
{
    public sealed class BtRoot : IBtDecorator
    {
        public IBtBehaviour Child { get; }

        public BtRoot(IBtBehaviour child)
        {
            Child = child ?? throw new ArgumentNullException(nameof(child));
        }

        public BehaviourStatus Status { get; private set; }

        public BehaviourStatus Tick(BtContext context)
        {
            var behaviourStatus = Child.Tick(context);
            Status = behaviourStatus;

            Debug.WriteLine(this.ToFriendlyString());

            if (behaviourStatus == BehaviourStatus.Succeeded || behaviourStatus == BehaviourStatus.Failed)
            {
                Reset();
                Debug.WriteLine($"Reset {Environment.NewLine}");
            }

            return behaviourStatus;
        }

        public void Reset()
        {
            Child.Reset();
        }

        public void Dispose()
        {
            Child.Dispose();
        }
    }
}
