using System;
using System.Threading;
using System.Threading.Tasks;

namespace BehaviourTree
{
    public sealed class BehaviourTreeRunner : IDisposable
    {
        private readonly int _intervalInMilliseconds;
        private readonly BtContext _context;
        private readonly IBtBehaviour _behaviourTree;
        private CancellationTokenSource _tokenSource;

        public BehaviourTreeRunner(IBtBehaviour behaviourTree, int intervalInMilliseconds)
            : this(behaviourTree, new BtContext(), intervalInMilliseconds)
        {
        }

        public BehaviourTreeRunner(IBtBehaviour behaviourTree, BtContext context, int intervalInMilliseconds)
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

            var status = await ExecuteCycle(_tokenSource.Token);

            while (!shouldStop(status) && !_tokenSource.IsCancellationRequested)
            {
                status = await ExecuteCycle(_tokenSource.Token);
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

            await Task.Delay(_intervalInMilliseconds, token);

            return behaviourStatus;
        }

        public void Dispose()
        {
            _behaviourTree.Dispose();
        }
    }
}
