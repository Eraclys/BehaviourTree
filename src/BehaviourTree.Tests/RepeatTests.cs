using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class RepeatTests
    {
        [Test]
        public void WhileRepeatCountNotReached_ReturnRunning()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };

            var sut = new Repeat<MockContext>(child, 10);

            for (var i = 0; i < 9; i++)
            {
                var behaviourStatus = sut.Tick(new MockContext());

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
                Assert.That(sut.Counter, Is.EqualTo(i + 1));
                Assert.That(child.TerminateCallCount, Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void WhenRepeatCountIsReached_ReturnSuccessAndResetCounter()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };

            var sut = new Repeat<MockContext>(child, 10);

            var behaviourStatus = BehaviourStatus.Ready;

            for (var i = 0; i < 10; i++)
            {
                behaviourStatus = sut.Tick(new MockContext());
            }

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
            Assert.That(sut.Counter, Is.EqualTo(0));

            behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
            Assert.That(sut.Counter, Is.EqualTo(1));
        }

        [Test]
        public void WhenChildReturnFailure_ReturnFailureAndResetCounter()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };

            var sut = new Repeat<MockContext>(child, 10);

            sut.Tick(new MockContext());

            child.ReturnStatus = BehaviourStatus.Failed;

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(sut.Counter, Is.EqualTo(0));
        }

        [Test]
        public void WhenChildReturnRunning_ReturnRunning()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };

            var sut = new Repeat<MockContext>(child, 10);

            for (var i = 0; i < 10; i++)
            {
                var behaviourStatus = sut.Tick(new MockContext());

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
                Assert.That(child.TerminateCallCount, Is.EqualTo(0));
                Assert.That(sut.Counter, Is.EqualTo(0));
            }
        }

        [Test]
        public void WhenResettingWhileRunning_ReInitializeCounter()
        {
            var child = new MockBehaviour
            {
                ReturnStatus = BehaviourStatus.Running
            };

            var sut = new Repeat<MockContext>(child, 15);

            sut.Tick(new MockContext());
            sut.Tick(new MockContext());
            sut.Tick(new MockContext());

            sut.Reset();

            Assert.That(sut.Counter, Is.EqualTo(0));
        }
    }
}