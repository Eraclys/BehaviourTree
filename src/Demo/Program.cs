using BehaviourTree;
using BehaviourTree.InputBehaviours;
using System;
using InputController;

namespace Demo
{
    internal static class Program
    {
        private static void Main()
        {
            var context = new BtContext();
            context.Set(IbKeys.MouseTarget, new Point(0, 0));

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

            Console.Read();
        }

        private static IBtBehaviour GetBehaviourTree()
        {
            return new MoveMouseCursorToTarget();
        }
    }
}
