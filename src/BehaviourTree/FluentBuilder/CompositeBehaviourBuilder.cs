using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.FluentBuilder
{
    public sealed class CompositeBehaviourBuilder<TContext> : BehaviourBuilder<TContext>
    {
        public CompositeBehaviourBuilder()
        {
            Children = new List<BehaviourBuilder<TContext>>();
        }

        public CreateCompositeBehaviour<TContext> Factory { get; set; }
        public IList<BehaviourBuilder<TContext>> Children { get; }

        public override IBehaviour<TContext> Build()
        {
            var behaviours = Children
                .Select(x => x.Build())
                .ToArray();

            return Factory(behaviours);
        }
    }
}