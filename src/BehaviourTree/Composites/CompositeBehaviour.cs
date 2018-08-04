using System;
using System.Linq;

namespace BehaviourTree.Composites
{
    public abstract class CompositeBehaviour<TContext> : BaseBehaviour<TContext>
    {
        protected IBehaviour<TContext>[] Children { get; }

        protected CompositeBehaviour(string name, IBehaviour<TContext>[] children) : base(name)
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

        protected override void OnTerminate(BehaviourStatus status)
        {
            DoReset(status);
        }

        protected override void DoReset(BehaviourStatus status)
        {
            ResetChildren();
        }

        private void ResetChildren()
        {
            foreach (var child in Children)
            {
                child.Reset();
            }
        }
    }
}
