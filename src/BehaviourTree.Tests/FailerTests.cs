using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class FailerTests
    {
        [Test]
        public void WhenChildReturnSuccess_ReturnFailure()
        {
            var sut = new Failer<MockContext>(new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded });

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
        }

        [Test]
        public void WhenChildReturnFailure_ReturnFailure()
        {
            var sut = new Failer<MockContext>(new MockBehaviour { ReturnStatus = BehaviourStatus.Failed });

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
        }

        [Test]
        public void WhenChildReturnRunning_ReturnRunning()
        {
            var sut = new Failer<MockContext>(new MockBehaviour { ReturnStatus = BehaviourStatus.Running });

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
        }
    }
}