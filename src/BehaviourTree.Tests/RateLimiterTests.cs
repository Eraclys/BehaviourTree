using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class RateLimiterTests
    {
        [Test]
        public void WhenChildReturnSuccess_ReturnSuccessAndCacheValue()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
            var sut = new RateLimiter<MockContext>(child, 1000);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
            Assert.That(child.UpdateCallCount, Is.EqualTo(1));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));

            behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
            Assert.That(child.UpdateCallCount, Is.EqualTo(1));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
        }

        [Test]
        public void WhenChildReturnFailure_ReturnFailureAndCacheValue()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Failed };
            var sut = new RateLimiter<MockContext>(child, 1000);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(child.UpdateCallCount, Is.EqualTo(1));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));

            behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(child.UpdateCallCount, Is.EqualTo(1));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
        }

        [Test]
        public void WhenChildReturnRunning_ReturnRunningButDoNotCacheValue()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };
            var sut = new RateLimiter<MockContext>(child, 1000);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
            Assert.That(child.UpdateCallCount, Is.EqualTo(1));
            Assert.That(child.TerminateCallCount, Is.EqualTo(0));

            behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
            Assert.That(child.UpdateCallCount, Is.EqualTo(2));
            Assert.That(child.TerminateCallCount, Is.EqualTo(0));
        }

        [Test]
        public void WhenCacheExpires_ChildMustBeReevaluated()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
            var sut = new RateLimiter<MockContext>(child, 1000);
            var context = new MockContext();

            var behaviourStatus = sut.Tick(context);

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
            Assert.That(child.UpdateCallCount, Is.EqualTo(1));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));

            context.AddMilliseconds(2000);
            child.ReturnStatus = BehaviourStatus.Failed;

            behaviourStatus = sut.Tick(context);

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(child.UpdateCallCount, Is.EqualTo(2));
            Assert.That(child.TerminateCallCount, Is.EqualTo(2));
        }
    }
}
