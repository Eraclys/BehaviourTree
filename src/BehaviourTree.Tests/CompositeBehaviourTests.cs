using System.Linq;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    internal sealed class CompositeBehaviourTests
    {
        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Running)]
        [TestCase(BehaviourStatus.Failed)]
        public void WhenNotReadyAndCallingReset_DoResetShouldBeCalledOnChild(BehaviourStatus status)
        {
            var behaviours = Enumerable.Range(0, 10)
                .Select(x => new MockBehaviour { ReturnStatus = status })
                .ToArray();

            var sut = new MockCompositeBehaviour(behaviours);

            foreach (var child in behaviours)
            {
                Assert.That(child.ResetCount, Is.EqualTo(0));
            }

            sut.Tick(new MockContext());

            sut.Reset();

            foreach (var child in behaviours)
            {
                Assert.That(child.ResetCount, Is.EqualTo(1));
                Assert.That(child.ResetStatus, Is.EqualTo(status));
            }
        }

        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Running)]
        [TestCase(BehaviourStatus.Failed)]
        public void WhenReadyAndCallingReset_DoResetShouldNotBeCalledOnChild(BehaviourStatus status)
        {
            var behaviours = Enumerable.Range(0, 10)
                .Select(x => new MockBehaviour { ReturnStatus = status })
                .ToArray();

            var sut = new MockCompositeBehaviour(behaviours);

            foreach (var child in behaviours)
            {
                Assert.That(child.ResetCount, Is.EqualTo(0));
            }

            sut.Reset();

            foreach (var child in behaviours)
            {
                Assert.That(child.ResetCount, Is.EqualTo(0));
            }
        }
    }
}
