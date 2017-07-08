using InputController;

namespace BehaviourTree.InputBehaviours
{
    public sealed class MoveMouseCursorToTarget : BaseBtBehaviour
    {
        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, BtContext context)
        {
            var mouseTarget = context.GetValue<Point?>(IbKeys.MouseTarget);

            if (!mouseTarget.HasValue)
            {
                return BehaviourStatus.Failed;
            }

            Mouse.SetCursorPosition(mouseTarget.Value);

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
