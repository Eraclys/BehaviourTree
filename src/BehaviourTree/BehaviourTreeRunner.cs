using System;
using System.Threading;
using System.Threading.Tasks;

namespace BehaviourTree
{
    public sealed class BehaviourTreeRunner<TContext> : IDisposable where TContext : class
    {
        private readonly int _intervalInMilliseconds;
        private readonly TContext _context;
        private readonly IBehaviour<TContext> _behaviourTree;
        private CancellationTokenSource _tokenSource;

        public BehaviourTreeRunner(IBehaviour<TContext> behaviourTree, TContext context, int intervalInMilliseconds)
        {
            _intervalInMilliseconds = intervalInMilliseconds;
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _behaviourTree = behaviourTree ?? throw new ArgumentNullException(nameof(behaviourTree));
        }

        public Task<BehaviourStatus> RunToFailureOrSuccess()
        {
            return DoWork(status =>
                status == BehaviourStatus.Succeeded ||
                status == BehaviourStatus.Failed);
        }

        public Task<BehaviourStatus> RunUntilStopped()
        {
            return DoWork(status => false);
        }

        private async Task<BehaviourStatus> DoWork(Predicate<BehaviourStatus> shouldStop)
        {
            Stop();

            _tokenSource = new CancellationTokenSource();

            var status = await ExecuteCycle(_tokenSource.Token).ConfigureAwait(false);

            while (!shouldStop(status) && !_tokenSource.IsCancellationRequested)
            {
                status = await ExecuteCycle(_tokenSource.Token).ConfigureAwait(false);
            }

            return status;
        }

        public void Stop()
        {
            _tokenSource?.Cancel();
        }

        private async Task<BehaviourStatus> ExecuteCycle(CancellationToken token)
        {
            var behaviourStatus = _behaviourTree.Tick(_context);

            await Task.Delay(_intervalInMilliseconds, token).ConfigureAwait(false);

            return behaviourStatus;
        }

        public void Dispose()
        {
            _behaviourTree.Dispose();
        }
    }
}
