using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class CooldownTests
    {
        [Test]
        public void WhenNotOnCooldownAndChildReturnSuccess_ReturnSuccessAndGoOnCooldown()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
            var sut = new Cooldown(child, 1000);

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
            Assert.That(sut.OnCooldown, Is.True);
        }

        [Test]
        public void WhenNotOnCooldownAndChildReturnFailure_ReturnFailureAndDoNotGoOnCooldown()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Failed };
            var sut = new Cooldown(child, 1000);

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
            Assert.That(sut.OnCooldown, Is.False);
        }

        [Test]
        public void WhenNotOnCooldownAndChildReturnRunning_ReturnRunningAndDoNotGoOnCooldown()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };
            var sut = new Cooldown(child, 1000);

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
            Assert.That(child.TerminateCallCount, Is.EqualTo(0));
            Assert.That(sut.OnCooldown, Is.False);
        }

        [Test]
        public void WhenOnCooldown_ReturnFailure()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
            var sut = new Cooldown(child, 1000);

            sut.Tick(new BtContext());

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
            Assert.That(sut.OnCooldown, Is.True);
        }

        [Test]
        public void WhenCooldownExpires_GoBackToRegularBehaviour()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
            var sut = new Cooldown(child, 1000);
            var clock = new MockClock();

            sut.Tick(new BtContext(clock));

            clock.AddMilliseconds(2000);

            var behaviourStatus = sut.Tick(new BtContext(clock));

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
            Assert.That(child.TerminateCallCount, Is.EqualTo(2));
            Assert.That(sut.OnCooldown, Is.True);
        }
    }
}
