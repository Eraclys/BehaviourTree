using System;
using System.Linq;
using BehaviourTree.Composites;
using BehaviourTree.Decorators;

namespace BehaviourTree
{
    public sealed class BehaviourTreeConsoleLogger<TContext>
    {
        public static void LogToConsole(IBehaviour<TContext> obj)
        {
            LogToConsole(obj, 0);
        }

        private static void LogToConsole(IBehaviour<TContext> obj, int depth)
        {
            LogToConsole((dynamic)obj, depth);
        }

        private static void LogToConsole(CompositeBehaviour<TContext> obj, int depth)
        {
            InternalPrint(obj, depth);

            var childDepth = depth + 1;

            foreach (var child in obj.Children)
            {
                LogToConsole(child, childDepth);
            }
        }

        private static void LogToConsole(DecoratorBehaviour<TContext> obj, int depth)
        {
            InternalPrint(obj, depth);
            LogToConsole(obj.Child, ++depth);
        }

        private static void LogToConsole(BaseBehaviour<TContext> obj, int depth)
        {
            InternalPrint(obj, depth);
        }

        private static void InternalPrint(IBehaviour<TContext> obj, int depth)
        {
            var indentation = GetIndentation(depth);
            var name = GetName(obj);
            var color = GetColor(obj.Status);

            Console.ForegroundColor = color;

            var nodeExpression = $"{indentation}{name}";

            Console.WriteLine(nodeExpression);

            Console.ResetColor();
        }

        private static ConsoleColor GetColor(BehaviourStatus status)
        {
            switch (status)
            {
                case BehaviourStatus.Ready: return ConsoleColor.DarkGray;
                case BehaviourStatus.Running: return ConsoleColor.Yellow;
                case BehaviourStatus.Succeeded: return ConsoleColor.Green;
                case BehaviourStatus.Failed: return ConsoleColor.Red;
                default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        private static string GetIndentation(int depth)
        {
            return string.Join(string.Empty, Enumerable.Repeat("   ", depth));
        }

        private static string GetName(IBehaviour<TContext> obj)
        {
            if (!string.IsNullOrWhiteSpace(obj.Name))
            {
                return obj.Name;
            }

            var type = obj.GetType();

            // TODO: check for generic

            return type.Name;
        }
    }
}
