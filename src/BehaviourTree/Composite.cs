using System;
using System.Linq;

namespace BehaviourTree
{
    public abstract class Composite : BaseBehaviour
    {
        protected readonly IBehaviour[] Children;

        protected Composite(IBehaviour[] children)
        {
            if (children == null)
            {
                throw new ArgumentNullException(nameof(children));
            }

            if (children.Length == 0)
            {
                throw new ArgumentException("Must have at least one child", nameof(children));
            }

            if (children.Any(x => x == null))
            {
                throw new ArgumentException("Children cannot contain null elements", nameof(children));
            }


            Children = children;
        }

        protected override void DoReset()
        {
            foreach (var child in Children)
            {
                child.Reset();
            }
        }
    }
}