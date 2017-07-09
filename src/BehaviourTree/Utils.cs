using System;
using System.Linq;
using System.Text;

namespace BehaviourTree
{
    internal static class Utils
    {
        public static string ToFriendlyString(this IBtBehaviour behaviour)
        {
            StringBuilder sb = new StringBuilder();

            ToFriendlyString(behaviour, sb, 0);

            return sb.ToString();
        }

        private static void ToFriendlyString(IBtBehaviour behaviour, StringBuilder sb, int depth)
        {
            var decorator = behaviour as IBtDecorator;
            var composite = behaviour as IBtComposite;

            sb.AppendLine($"{GetIndentation(depth)}{behaviour.GetType().Name} -> {behaviour.Status}");

            if (decorator != null)
            {
                ToFriendlyString(decorator.Child, sb, depth + 1);
            }

            if (composite != null)
            {
                foreach (var child in composite.Children)
                {
                    ToFriendlyString(child, sb, depth + 1);
                }
            }
        }

        private static string GetIndentation(int depth)
        {
            return String.Join(String.Empty, Enumerable.Repeat("   ", depth));
        }

    }
}
