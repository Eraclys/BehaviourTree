using InputSimulator;

namespace BehaviourTree.InputBehaviours
{
    public sealed class MoveMouseCursorToTarget : BaseBtBehaviour
    {
        protected override BehaviourStatus DoTick(BtContext context)
        {
            var mouseTarget = context.GetValue<Point?>(IbKeys.MouseTarget);

            if (!mouseTarget.HasValue)
            {
                return BehaviourStatus.Failed;
            }

            Input.Mouse.SetCursorPosition(mouseTarget.Value);

            return BehaviourStatus.Succeeded;
        }
    }
}
