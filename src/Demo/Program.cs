using BehaviourTree;
using BehaviourTree.InputBehaviours;
using System;
using BehaviourTree.Composites;
using InputSimulator;

namespace Demo
{
    internal static class Program
    {
        private static void Main()
        {
            var context = new BtContext();
            context.Set(IbKeys.DragAndDropGrabTarget, new Point(0, 0));
            context.Set(IbKeys.DragAndDropReleaseTarget, new Point(100, 100));

            var runner = new BehaviourTreeRunner(GetBehaviourTree(), context);

            try
            {
                var behaviourStatus = runner.Tick();

                Console.WriteLine("TreeStatus {0}", behaviourStatus);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Finished");

            Console.ReadLine();
            Console.Read();
        }

        private static IBtBehaviour GetBehaviourTree()
        {
            return new BtSequence(
                new SendKeys("Hello world"),
                new DragAndDrop());
        }
    }
}
