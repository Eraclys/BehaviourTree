using System;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class BaseBehaviourTests
    {
        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Running)]
        [TestCase(BehaviourStatus.Failed)]
        public void OnInitialize_ShouldOnlyBeCalledTheFirstTime(BehaviourStatus status)
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = status
            };

            Assert.That(sut.InitializeCallCount, Is.EqualTo(0));

            sut.Tick(new MockContext());

            Assert.That(sut.InitializeCallCount, Is.EqualTo(1));

            sut.Tick(new MockContext());

            Assert.That(sut.InitializeCallCount, Is.EqualTo(1));
        }

        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Failed)]
        public void OnTerminate_ShouldBeCalledOnSuccessOrFailure(BehaviourStatus status)
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = status
            };

            Assert.That(sut.TerminateCallCount, Is.EqualTo(0));

            sut.Tick(new MockContext());

            Assert.That(sut.TerminateCallCount, Is.EqualTo(1));
            Assert.That(sut.TerminateStatus, Is.EqualTo(status));
        }

        [Test]
        public void OnTerminate_ShouldNotBeCalledOnRunning()
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = BehaviourStatus.Running
            };

            Assert.That(sut.TerminateCallCount, Is.EqualTo(0));

            sut.Tick(new MockContext());

            Assert.That(sut.TerminateCallCount, Is.EqualTo(0));
        }

        [Test]
        public void ReturningReady_ShouldNotBeAllowed()
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = BehaviourStatus.Ready
            };

            Assert.Throws<InvalidOperationException>(() => sut.Tick(new MockContext()));
        }

        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Running)]
        [TestCase(BehaviourStatus.Failed)]
        public void WhenNotReadyAndCallingReset_DoResetShouldBeCalled(BehaviourStatus status)
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = status
            };

            Assert.That(sut.ResetCount, Is.EqualTo(0));

            sut.Tick(new MockContext());

            sut.Reset();

            Assert.That(sut.ResetCount, Is.EqualTo(1));
            Assert.That(sut.ResetStatus, Is.EqualTo(status));
        }

        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Running)]
        [TestCase(BehaviourStatus.Failed)]
        public void WhenReadyAndCallingReset_DoResetShouldNotBeCalled(BehaviourStatus status)
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = status
            };

            Assert.That(sut.ResetCount, Is.EqualTo(0));

            sut.Reset();

            Assert.That(sut.ResetCount, Is.EqualTo(0));
        }
    }
}
