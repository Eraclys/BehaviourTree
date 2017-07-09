using InputSimulator;

namespace BehaviourTree.InputBehaviours
{
    public sealed class DragAndDrop : BaseBtBehaviour
    {
        protected override BehaviourStatus DoTick(BtContext context)
        {
            var grabTarget = context.Get<Point?>(IbKeys.DragAndDropGrabTarget);
            var releaseTarget = context.Get<Point?>(IbKeys.DragAndDropReleaseTarget);

            if (grabTarget == null || releaseTarget == null)
            {
                return BehaviourStatus.Failed;
            }

            Input.Mouse
                .LeftDown(grabTarget.Value)
                .LeftUp(releaseTarget.Value);

            return BehaviourStatus.Succeeded;
        }
    }
}
