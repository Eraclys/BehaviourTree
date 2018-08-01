using BehaviourTree.Behaviours;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class ConditionTests
    {
        [Test]
        public void WhenPredicateReturnTrue_ReturnSuccess()
        {
            var sut = new Condition(_ => true);

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
        }

        [Test]
        public void WhenPredicateReturnFalse_ReturnFailure()
        {
            var sut = new Condition(_ => false);

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
        }
    }
}
