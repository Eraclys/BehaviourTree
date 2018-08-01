using BehaviourTree;
using System;
using System.Threading;
using BehaviourTree.Behaviours;
using BehaviourTree.Decorators;

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

            Console.WriteLine("Ctrl+C to terminate");

            var runner = CreateRunner();

            runner.RunUntilStopped()
                .ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        Console.WriteLine(t.Exception);
                    }
                });

            QuitEvent.WaitOne();

            runner.Stop();
            runner.Dispose();
        }

        private static BehaviourTreeRunner CreateRunner()
        {
            var runner = new BehaviourTreeRunner(GetBehaviourTree(), 1000);

            return runner;
        }

        private static IBtBehaviour GetBehaviourTree()
        {
            return
                //new BtRegulator(
                new BtAutoReset(
                    new BtDebugLogger(
                        //new BtGoogleSearch("foo")
                        new BtTimeOut(
                            new BtWait(4000),
                            3000)
                    )
                );
                //,5000);
        }
    }
}
