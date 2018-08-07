using BehaviourTree.Behaviours;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class WaitTests
    {
        [Test]
        public void WhenStarted_ReturnRunning()
        {
            var sut = new Wait<MockContext>(1000);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
        }

        [Test]
        public void WhenWaitTimeExpires_ReturnSuccessAndResetTimer()
        {
            var sut = new Wait<MockContext>(1000);
            var context = new MockContext();

            sut.Tick(context);

            context.AddMilliseconds(2000);

            var behaviourStatus = sut.Tick(context);

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));

            behaviourStatus = sut.Tick(context);

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
        }

        [Test]
        public void WhenResetIsCalled_ReturnResetTimer()
        {
            var sut = new Wait<MockContext>(1000);
            var context = new MockContext();

            context.AddMilliseconds(500);

            var behaviourStatus = sut.Tick(context);

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));

            sut.Reset();
            context.AddMilliseconds(600);

            behaviourStatus = sut.Tick(context);

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
        }
    }
}
