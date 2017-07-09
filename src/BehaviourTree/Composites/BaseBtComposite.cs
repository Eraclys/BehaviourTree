using System;
using System.Linq;

namespace BehaviourTree.Composites
{
    public abstract class BaseBtComposite : BaseBtBehaviour, IBtComposite
    {
        public IBtBehaviour[] Children { get; }

        protected BaseBtComposite(IBtBehaviour[] children)
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