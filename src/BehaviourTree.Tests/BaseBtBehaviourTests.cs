using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    public sealed class BaseBtBehaviourTests
    {
        [TestFixture]
        public sealed class GivenCurrentStatusIsSucceded
        {
            private MockBtBehaviour _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new MockBtBehaviour(BehaviourStatus.Succeeded, BehaviourStatus.Succeeded);
            }

            [Test]
            public void WhenCallingTick_DoTickShouldNotBeCalled()
            {
                var behaviourStatus = _sut.Tick(ElaspedTicks.From(0), new BtContext());

                Assert.That(_sut.DoTickCount, Is.EqualTo(0));
                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Succeeded));
                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new BehaviourStatus[0]));
            }

            [Test]
            public void WhenCallingReset_DoResetShouldBeCalled()
            {
                _sut.Reset();

                Assert.That(_sut.DoResetCount, Is.EqualTo(1));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Ready));
            }
        }


        [TestFixture]
        public sealed class GivenCurrentStatusIsFailed
        {
            private MockBtBehaviour _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new MockBtBehaviour(BehaviourStatus.Failed, BehaviourStatus.Failed);
            }

            [Test]
            public void WhenCallingTick_DoTickShouldNotBeCalled()
            {
                var behaviourStatus = _sut.Tick(ElaspedTicks.From(0), new BtContext());

                Assert.That(_sut.DoTickCount, Is.EqualTo(0));
                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new BehaviourStatus[0]));
            }

            [Test]
            public void WhenCallingReset_DoResetShouldBeCalled()
            {
                _sut.Reset();

                Assert.That(_sut.DoResetCount, Is.EqualTo(1));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Ready));
            }
        }


        [TestFixture]
        public sealed class GivenCurrentStatusIsReady
        {
            private MockBtBehaviour _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Failed);
            }

            [Test]
            public void WhenCallingTick_DoTickShouldBeCalled()
            {
                var behaviourStatus = _sut.Tick(ElaspedTicks.From(0), new BtContext());

                Assert.That(_sut.DoTickCount, Is.EqualTo(1));
                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new[]
                {
                    BehaviourStatus.Running,
                    BehaviourStatus.Failed
                }));
            }

            [Test]
            public void WhenCallingReset_DoResetShouldNotBeCalled()
            {
                _sut.Reset();

                Assert.That(_sut.DoResetCount, Is.EqualTo(0));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Ready));
                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new BehaviourStatus[0]));
            }
        }



        [TestFixture]
        public sealed class GivenCurrentStatusIsRunning
        {
            private MockBtBehaviour _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new MockBtBehaviour(BehaviourStatus.Running, BehaviourStatus.Failed);
            }

            [Test]
            public void WhenCallingTick_DoTickShouldBeCalled()
            {
                var behaviourStatus = _sut.Tick(ElaspedTicks.From(0), new BtContext());

                Assert.That(_sut.DoTickCount, Is.EqualTo(1));
                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new[]
                {
                    BehaviourStatus.Failed
                }));
            }

            [Test]
            public void WhenCallingReset_DoResetShouldBeCalled()
            {
                _sut.Reset();

                Assert.That(_sut.DoResetCount, Is.EqualTo(1));
                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Ready));
            }
        }
    }
}
