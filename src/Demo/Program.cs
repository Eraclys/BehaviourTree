using BehaviourTree;
using BehaviourTree.InputBehaviours;
using InputSimulator;
using System;
using System.Threading;
using System.Threading.Tasks;

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

            var task = Task.Run(async () =>
            {
                var runner = CreateRunner();

                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        await ExecuteCycle(runner, 1000);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    runner.Dispose();
                }
            });

            QuitEvent.WaitOne();

            tokenSource.Cancel();
            task.Wait();
        }

        private static Task ExecuteCycle(BehaviourTreeRunner runner, int cycleInMilliseconds)
        {
            var behaviourStatus = runner.Tick();
            Console.WriteLine("TreeStatus {0}", behaviourStatus);

            return Task.Delay(1000);
        }

        private static BehaviourTreeRunner CreateRunner()
        {
            var context = new BtContext();
            var runner = new BehaviourTreeRunner(GetBehaviourTree(), context);

            context.Set(IbKeys.DragAndDropGrabTarget, new Point(0, 0));
            context.Set(IbKeys.DragAndDropReleaseTarget, new Point(100, 100));

            return runner;
        }

        private static IBtBehaviour GetBehaviourTree()
        {
            return new GoogleSearch("foo");
        }
    }
}
