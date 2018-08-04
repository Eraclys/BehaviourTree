using BehaviourTree.Behaviours;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class ConditionTests
    {
        [Test]
        public void WhenPredicateReturnTrue_ReturnSuccess()
        {
            var sut = new Condition<MockContext>(_ => true);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
        }

        [Test]
        public void WhenPredicateReturnFalse_ReturnFailure()
        {
            var sut = new Condition<MockContext>(_ => false);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
        }
    }
}
