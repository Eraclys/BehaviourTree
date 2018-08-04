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
            var sut = new Cooldown<MockContext>(child, 1000);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
            Assert.That(sut.OnCooldown, Is.True);
        }

        [Test]
        public void WhenNotOnCooldownAndChildReturnFailure_ReturnFailureAndDoNotGoOnCooldown()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Failed };
            var sut = new Cooldown<MockContext>(child, 1000);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
            Assert.That(sut.OnCooldown, Is.False);
        }

        [Test]
        public void WhenNotOnCooldownAndChildReturnRunning_ReturnRunningAndDoNotGoOnCooldown()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };
            var sut = new Cooldown<MockContext>(child, 1000);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
            Assert.That(child.TerminateCallCount, Is.EqualTo(0));
            Assert.That(sut.OnCooldown, Is.False);
        }

        [Test]
        public void WhenOnCooldown_ReturnFailure()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
            var sut = new Cooldown<MockContext>(child, 1000);

            sut.Tick(new MockContext());

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(child.TerminateCallCount, Is.EqualTo(1));
            Assert.That(sut.OnCooldown, Is.True);
        }

        [Test]
        public void WhenCooldownExpires_GoBackToRegularBehaviour()
        {
            var child = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
            var sut = new Cooldown<MockContext>(child, 1000);
            var context = new MockContext();

            sut.Tick(context);

            context.AddMilliseconds(2000);

            var behaviourStatus = sut.Tick(context);

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
            Assert.That(child.TerminateCallCount, Is.EqualTo(2));
            Assert.That(sut.OnCooldown, Is.True);
        }
    }
}
