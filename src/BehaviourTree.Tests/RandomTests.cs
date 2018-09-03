using System;
using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class RandomTests
    {
        [Test]
        public void WhenThresholdIsBelowOrEqualToZero_ThrowException()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };

            Assert.Throws<ArgumentException>(() => { new Random<MockContext>(child, 0); });
            Assert.Throws<ArgumentException>(() => { new Random<MockContext>(child, -10); });
        }

        [Test]
        public void WhenThresholdIsAboveOne_ThrowException()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };

            Assert.Throws<ArgumentException>(() => { new Random<MockContext>(child, 1.1); });
        }

        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Failed)]
        public void WhenRandomValueIsAboveThreshold_CallChildAndReturnChildStatus(BehaviourStatus childStatus)
        {
            var child = new MockBehaviour {ReturnStatus = childStatus };
            var randomProvider = new MockRandomProvider();
            var sut = new Random<MockContext>(child, 0.5, randomProvider);

            randomProvider.SetNextRandomDouble(0.6);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(childStatus));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
        }

        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Failed)]
        public void WhenRandomValueIsEqualToThreshold_CallChildAndReturnChildStatus(BehaviourStatus childStatus)
        {
            var child = new MockBehaviour { ReturnStatus = childStatus };
            var randomProvider = new MockRandomProvider();
            var sut = new Random<MockContext>(child, 0.4, randomProvider);

            randomProvider.SetNextRandomDouble(0.4);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(childStatus));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
        }

        [Test]
        public void WhenRandomValueIsBelowThreshold_DoNotCallChildAndReturnFailure()
        {
            var child = new MockBehaviour {ReturnStatus = BehaviourStatus.Succeeded};
            var randomProvider = new MockRandomProvider();
            var sut = new Random<MockContext>(child, 0.5, randomProvider);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(child.TerminateCallCount, Is.EqualTo(0));
        }
    }
}
