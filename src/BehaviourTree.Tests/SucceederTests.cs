using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class SucceederTests
    {
        [Test]
        public void WhenChildReturnSuccess_ReturnSuccess()
        {
            var sut = new Succeeder(new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded });

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
        }

        [Test]
        public void WhenChildReturnFailure_ReturnSuccess()
        {
            var sut = new Succeeder(new MockBehaviour { ReturnStatus = BehaviourStatus.Failed });

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
        }

        [Test]
        public void WhenChildReturnRunning_ReturnRunning()
        {
            var sut = new Succeeder(new MockBehaviour { ReturnStatus = BehaviourStatus.Running });

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
        }
    }
}