using InputSimulator;

namespace BehaviourTree.InputBehaviours
{
    public sealed class DragAndDrop : BaseBtBehaviour
    {
        protected override BehaviourStatus DoTick(ElaspedTicks elaspedTicks, BtContext context)
        {
            var grabTarget = context.GetValue<Point?>(IbKeys.DragAndDropGrabTarget);
            var releaseTarget = context.GetValue<Point?>(IbKeys.DragAndDropReleaseTarget);

            if (grabTarget == null || releaseTarget == null)
            {
                return BehaviourStatus.Failed;
            }

            Input.Mouse
                .LeftDown(grabTarget.Value)
                .LeftUp(releaseTarget.Value);

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
