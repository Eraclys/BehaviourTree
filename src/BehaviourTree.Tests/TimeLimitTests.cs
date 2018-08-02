using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class TimeLimitTests
    {
        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Failed)]
        [TestCase(BehaviourStatus.Running)]
        public void WhileTimeLimitHasNotExpired_ReturnChildStatus(BehaviourStatus status)
        {
            var child = new MockBehaviour { ReturnStatus = status };
            var sut = new TimeLimit(child, 1000);

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(status));
            Assert.That(child.UpdateCallCount, Is.EqualTo(1));
        }

        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Failed)]
        public void WhenTimeLimitHasExpired_ReturnFailureAndResetChild(BehaviourStatus status)
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };
            var sut = new TimeLimit(child, 1000);
            var clock = new MockClock();

            sut.Tick(new BtContext(clock));

            clock.AddMilliseconds(2000);

            var behaviourStatus = sut.Tick(new BtContext(clock));

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(child.UpdateCallCount, Is.EqualTo(1));

            child.ReturnStatus = status;

            behaviourStatus = sut.Tick(new BtContext(clock));

            Assert.That(behaviourStatus, Is.EqualTo(status));
            Assert.That(child.UpdateCallCount, Is.EqualTo(2));
        }

        [Test]
        public void WhenResettingWhileRunning_ReInitializeTimer()
        {
            var child = new MockBehaviour
            {
                ReturnStatus = BehaviourStatus.Running
            };

            var clock = new MockClock();

            var sut = new TimeLimit(child, 1000);

            sut.Tick(new BtContext(clock));

            clock.AddMilliseconds(2000);

            sut.Reset();

            var behaviourStatus = sut.Tick(new BtContext(clock));

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
        }
    }
}
