using System;
using System.Linq;

namespace BehaviourTree.Composites
{
    public abstract class BaseBtComposite<TContext> : BaseBtBehaviour<TContext>
    {
        protected readonly IBtBehaviour<TContext>[] Children;

        protected BaseBtComposite(IBtBehaviour<TContext>[] children)
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
            if (!disposing)
            {
                return;
            }

            foreach (var child in Children)
            {
                child.Dispose();
            }
        }
    }
}