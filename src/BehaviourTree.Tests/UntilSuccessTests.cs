using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class UntilSuccessTests
    {
        [Test]
        public void WhenChildReturnFailed_RepeatChildBehaviourAndReturnRunning()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Failed };

            var sut = new UntilSuccess<MockContext>(child);

            for (var i = 0; i < 10; i++)
            {
                var behaviourStatus = sut.Tick(new MockContext());

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
                Assert.That(child.TerminateCallCount, Is.EqualTo(i + 1));
            }
        }

        [Test]
        public void WhenChildReturnSuccess_ReturnSuccess()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };

            var sut = new UntilSuccess<MockContext>(child);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
        }

        [Test]
        public void WhenChildReturnRunning_ReturnRunning()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };

            var sut = new UntilSuccess<MockContext>(child);

            for (var i = 0; i < 10; i++)
            {
                var behaviourStatus = sut.Tick(new MockContext());

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
                Assert.That(child.TerminateCallCount, Is.EqualTo(0));
            }
        }
    }
}