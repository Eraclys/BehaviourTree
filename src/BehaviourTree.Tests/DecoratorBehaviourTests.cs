using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    internal sealed class DecoratorBehaviourTests
    {
        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Running)]
        [TestCase(BehaviourStatus.Failed)]
        public void WhenNotReadyAndCallingReset_DoResetShouldBeCalledOnChild(BehaviourStatus status)
        {
            var childBehaviour = new MockBehaviour
            {
                ReturnStatus = status
            };

            var sut = new MockDecoratorBehaviour(childBehaviour);

            Assert.That(childBehaviour.ResetCount, Is.EqualTo(0));

            sut.Tick(new MockContext());

            sut.Reset();

            Assert.That(childBehaviour.ResetCount, Is.EqualTo(1));
            Assert.That(childBehaviour.ResetStatus, Is.EqualTo(status));
        }

        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Running)]
        [TestCase(BehaviourStatus.Failed)]
        public void WhenReadyAndCallingReset_DoResetShouldNotBeCalledOnChild(BehaviourStatus status)
        {
            var childBehaviour = new MockBehaviour
            {
                ReturnStatus = status
            };

            var sut = new MockDecoratorBehaviour(childBehaviour);

            Assert.That(childBehaviour.ResetCount, Is.EqualTo(0));

            sut.Reset();

            Assert.That(childBehaviour.ResetCount, Is.EqualTo(0));
        }
    }
}
