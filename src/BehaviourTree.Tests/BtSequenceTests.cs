using BehaviourTree.Composites;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    public sealed class BtSequenceTests
    {
        [TestFixture]
        public sealed class GivenChildrenReturnsSuccess
        {
            private BtSequence _sut;
            private WatchCollectionMock _childrenWatcher;

            [SetUp]
            public void Setup()
            {
                _childrenWatcher = new WatchCollectionMock(
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Succeeded),
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Succeeded),
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Succeeded));

                _sut = new BtSequence(_childrenWatcher.Behaviours);
            }

            [Test]
            public void WhenCallingTick_ShouldReturnSuccess()
            {
                var behaviourStatus = _sut.Tick(new BtContext());

                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Succeeded));
                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
                Assert.That(_childrenWatcher.AllChildrenCalled);
            }
        }

        [TestFixture]
        public sealed class GivenChildrenReturnsFailure
        {
            private BtSequence _sut;
            private WatchCollectionMock _childrenWatcher;

            [SetUp]
            public void Setup()
            {
                _childrenWatcher = new WatchCollectionMock(
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Succeeded),
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Failed),
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Succeeded));

                _sut = new BtSequence(_childrenWatcher.Behaviours);
            }


            [Test]
            public void WhenCallingTick_ShouldReturnFailure()
            {
                var behaviourStatus = _sut.Tick(new BtContext());

                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(_childrenWatcher.NbOfChildrenCalled, Is.EqualTo(2));
            }
        }
    }
}
