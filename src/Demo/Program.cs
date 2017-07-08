using BehaviourTree;
using BehaviourTree.InputBehaviours;
using System;
using System.Threading;
using System.Threading.Tasks;
using BehaviourTree.Behaviours;
using InputSimulator;

namespace Demo
{
    internal static class Program
    {
        static readonly ManualResetEvent QuitEvent = new ManualResetEvent(false);

        private static void Main()
        {
            Console.CancelKeyPress += (sender, eArgs) => {
                QuitEvent.Set();
                eArgs.Cancel = true;
            };

            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;


            Console.WriteLine("Ctrl+C to terminate");

            try
            {
                Task.Run(async () =>
                {
                    var context = new BtContext();
                    var runner = new BehaviourTreeRunner(GetBehaviourTree(), context);

                    context.Set(IbKeys.DragAndDropGrabTarget, new Point(0, 0));
                    context.Set(IbKeys.DragAndDropReleaseTarget, new Point(100, 100));

                    while (!token.IsCancellationRequested)
                    {
                        var behaviourStatus = runner.Tick();
                        Console.WriteLine("TreeStatus {0}", behaviourStatus);

                        await Task.Delay(1000, token);
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            QuitEvent.WaitOne();
            tokenSource.Cancel();
        }

        private static IBtBehaviour GetBehaviourTree()
        {
            return new BtWait(1000);
        }
    }
}
