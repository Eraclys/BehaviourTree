using BehaviourTree;
using BehaviourTree.Behaviours;
using System.Diagnostics;

namespace Demo
{
    public sealed class IsProcessRunning : BtCondition
    {
        private readonly string _instanceName;

        public IsProcessRunning(string instanceName)
        {
            _instanceName = instanceName;
        }

        protected override bool Condition(BtContext context)
        {
            var process = context.Get<Process>(_instanceName);

            if (process == null)
            {
                return false;
            }

            return !process.HasExited;
        }
    }
}