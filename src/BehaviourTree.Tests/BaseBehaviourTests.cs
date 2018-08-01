using System;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class BaseBehaviourTests
    {
        [Test]
        public void OnInitialize_ShouldOnlyBeCalledTheFirstTime()
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = BehaviourStatus.Succeeded
            };

            Assert.That(sut.InitializeCallCount, Is.EqualTo(0));

            sut.Tick(new BtContext());

            Assert.That(sut.InitializeCallCount, Is.EqualTo(1));

            sut.Tick(new BtContext());

            Assert.That(sut.InitializeCallCount, Is.EqualTo(1));
        }

        [Test]
        public void OnTerminate_ShouldBeCalledOnSuccess()
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = BehaviourStatus.Succeeded
            };

            Assert.That(sut.TerminateCallCount, Is.EqualTo(0));

            sut.Tick(new BtContext());

            Assert.That(sut.TerminateCallCount, Is.EqualTo(1));
        }

        [Test]
        public void OnTerminate_ShouldBeCalledOnFailure()
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = BehaviourStatus.Failed
            };

            Assert.That(sut.TerminateCallCount, Is.EqualTo(0));

            sut.Tick(new BtContext());

            Assert.That(sut.TerminateCallCount, Is.EqualTo(1));
        }

        [Test]
        public void OnTerminate_ShouldNotBeCalledOnRunning()
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = BehaviourStatus.Running
            };

            Assert.That(sut.TerminateCallCount, Is.EqualTo(0));

            sut.Tick(new BtContext());

            Assert.That(sut.TerminateCallCount, Is.EqualTo(0));
        }

        [Test]
        public void Ready_ShouldNotAllowedBeReturnedByTick()
        {
            var sut = new MockBehaviour
            {
                ReturnStatus = BehaviourStatus.Ready
            };

            Assert.Throws<InvalidOperationException>(() => sut.Tick(new BtContext()));
        }
    }
}
