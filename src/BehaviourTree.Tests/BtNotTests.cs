using BehaviourTree.Decorators;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    public sealed class BtNotTests
    {
        [TestFixture]
        public sealed class GivenChildrenReturnsSuccess
        {
            private IBtBehaviour _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new BtNot(new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Succeeded));
            }

            [Test]
            public void WhenCallingTick_ShouldReturnFailure()
            {
                var behaviourStatus = _sut.Tick(ElaspedTicks.From(0));

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Failed));
            }
        }

        [TestFixture]
        public sealed class GivenChildrenReturnsFailure
        {
            private IBtBehaviour _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new BtNot(new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Failed));
            }

            [Test]
            public void WhenCallingTick_ShouldReturnFailure()
            {
                var behaviourStatus = _sut.Tick(ElaspedTicks.From(0));

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Succeeded));
            }
        }



        [TestFixture]
        public sealed class GivenChildrenReturnsRunning
        {
            private IBtBehaviour _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new BtNot(new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Running));
            }

            [Test]
            public void WhenCallingTick_ShouldReturnRunning()
            {
                var behaviourStatus = _sut.Tick(ElaspedTicks.From(0));

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Running));
            }
        }
    }
}
