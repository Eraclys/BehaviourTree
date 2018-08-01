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
            var sut = new Wait(1000);

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
        }

        [Test]
        public void WhenWaitTimeExpires_ReturnSuccessAndResetTimer()
        {
            var sut = new Wait(1000);
            var clock = new MockClock();

            sut.Tick(new BtContext(clock));

            clock.AddMilliseconds(2000);

            var behaviourStatus = sut.Tick(new BtContext(clock));

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));

            behaviourStatus = sut.Tick(new BtContext(clock));

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
        }
    }
}
