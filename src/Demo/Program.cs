using BehaviourTree;
using System;
using System.Threading;

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
            var runner = new BehaviourTreeRunner(new BtRoot(GetBehaviourTree()), 3000);

            return runner;
        }

        private static IBtBehaviour GetBehaviourTree()
        {
            return new BtGoogleSearch("foo");
        }
    }
}
