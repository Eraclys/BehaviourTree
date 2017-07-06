using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    public sealed class BaseBehaviourTests
    {
        [TestFixture]
        public sealed class GivenCurrentStatusIsActive
        {
            private MockBehaviour _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new MockBehaviour(BehaviourStatus.Active, false);
            }

            [Test]
            public void WhenCallingStart_DoStartShouldNotBeCalled()
            {
                _sut.Start();

                Assert.That(_sut.DoStartCount, Is.EqualTo(0));
                Assert.That(_sut.CurrentStatus, Is.EqualTo(BehaviourStatus.Active));
                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new BehaviourStatus[0]));
            }

            [Test]
            public void WhenCallingStop_DoStopShouldBeCalled()
            {
                _sut.Stop();

                Assert.That(_sut.DoStopCount, Is.EqualTo(1));
                Assert.That(_sut.CurrentStatus, Is.EqualTo(BehaviourStatus.Inactive));

                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new []
                {
                    BehaviourStatus.StopRequested,
                    BehaviourStatus.Inactive
                }));
            }
        }


        [TestFixture]
        public sealed class GivenCurrentStatusIsStopRequested
        {
            private MockBehaviour _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new MockBehaviour(BehaviourStatus.StopRequested, false);
            }

            [Test]
            public void WhenCallingStart_DoStartShouldNotBeCalled()
            {
                _sut.Start();

                Assert.That(_sut.DoStartCount, Is.EqualTo(0));
                Assert.That(_sut.CurrentStatus, Is.EqualTo(BehaviourStatus.StopRequested));
                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new BehaviourStatus[0]));

            }

            [Test]
            public void WhenCallingStop_DoStopShouldNotBeCalled()
            {
                _sut.Stop();

                Assert.That(_sut.DoStopCount, Is.EqualTo(0));
                Assert.That(_sut.CurrentStatus, Is.EqualTo(BehaviourStatus.StopRequested));
                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new BehaviourStatus[0]));
            }
        }


        [TestFixture]
        public sealed class GivenCurrentStatusIsInactive
        {
            private MockBehaviour _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new MockBehaviour(BehaviourStatus.Inactive, false);
            }

            [Test]
            public void WhenCallingStart_DoStartShouldBeCalled()
            {
                _sut.Start();

                Assert.That(_sut.DoStartCount, Is.EqualTo(1));
                Assert.That(_sut.CurrentStatus, Is.EqualTo(BehaviourStatus.Active));
                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new []
                {
                    BehaviourStatus.Active
                }));

            }

            [Test]
            public void WhenCallingStop_DoStopShouldNotBeCalled()
            {
                _sut.Stop();

                Assert.That(_sut.DoStopCount, Is.EqualTo(0));
                Assert.That(_sut.CurrentStatus, Is.EqualTo(BehaviourStatus.Inactive));
                Assert.That(_sut.StatusChanges, Is.EquivalentTo(new BehaviourStatus[0]));
            }
        }
    }
}
