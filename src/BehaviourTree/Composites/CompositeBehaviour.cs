using System;
using System.Linq;

namespace BehaviourTree.Composites
{
    public abstract class CompositeBehaviour : BaseBehaviour
    {
        protected IBehaviour[] Children { get; }

        protected CompositeBehaviour(IBehaviour[] children)
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var child in Children)
                {
                    child.Dispose();
                }
            }
        }
    }
}
