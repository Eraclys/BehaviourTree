using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class AutoResetTests
    {
        [TestCase(BehaviourStatus.Failed)]
        [TestCase(BehaviourStatus.Succeeded)]
        public void WhenChildTerminates_ResetChild(BehaviourStatus status)
        {
            var child = new MockBehaviour { ReturnStatus = status };

            var sut = new AutoReset<MockContext>(child);

            sut.Tick(new MockContext());

            Assert.That(child.ResetCount, Is.EqualTo(1));
        }

        [Test]
        public void WhenChildIsRunning_ContinueRunning()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };

            var sut = new AutoReset<MockContext>(child);

            sut.Tick(new MockContext());

            Assert.That(child.ResetCount, Is.EqualTo(0));
        }

        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Running)]
        [TestCase(BehaviourStatus.Failed)]
        public void OnTick_ShouldReturnChildStatus(BehaviourStatus status)
        {
            var child = new MockBehaviour { ReturnStatus = status };

            var sut = new AutoReset<MockContext>(child);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(status));
        }
    }
}
