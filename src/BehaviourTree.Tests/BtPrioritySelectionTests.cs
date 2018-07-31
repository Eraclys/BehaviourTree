using BehaviourTree.Composites;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    public sealed class BtPrioritySelectionTests
    {
        [TestFixture]
        public sealed class GivenAChildReturnsSuccess
        {
            private BtPrioritySelection _sut;
            private WatchCollectionMock _childrenWatcher;

            [SetUp]
            public void Setup()
            {
                _childrenWatcher = new WatchCollectionMock(
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Failed),
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Succeeded),
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Succeeded));

                _sut = new BtPrioritySelection(_childrenWatcher.Behaviours);
            }

            [Test]
            public void WhenCallingTick_ShouldReturnFirstSuccess()
            {
                var behaviourStatus = _sut.Tick(new BtContext());

                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Succeeded));
                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
                Assert.That(_childrenWatcher.NbOfChildrenCalled, Is.EqualTo(2));
            }
        }

        [TestFixture]
        public sealed class GivenAllChildrenReturnsFailure
        {
            private BtPrioritySelection _sut;
            private WatchCollectionMock _childrenWatcher;

            [SetUp]
            public void Setup()
            {
                _childrenWatcher = new WatchCollectionMock(
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Failed),
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Failed),
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Failed));

                _sut = new BtPrioritySelection(_childrenWatcher.Behaviours);
            }

            [Test]
            public void WhenCallingTick_ShouldReturnFailure()
            {
                var behaviourStatus = _sut.Tick(new BtContext());

                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(_childrenWatcher.AllChildrenCalled);
            }
        }

        [TestFixture]
        public sealed class GivenOneNodeIsInRunningState
        {
            private BtPrioritySelection _sut;
            private WatchCollectionMock _childrenWatcher;

            [SetUp]
            public void Setup()
            {
                _childrenWatcher = new WatchCollectionMock(
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Failed),
                    new MockBtBehaviour(BehaviourStatus.Ready, BehaviourStatus.Running));

                _sut = new BtPrioritySelection(_childrenWatcher.Behaviours);
            }

            [Test]
            public void WhenCallingTickMultipleTimes_PreviousNodesShouldBeRerun()
            {
                _sut.Tick(new BtContext());
                _sut.Tick(new BtContext());
                var behaviourStatus = _sut.Tick(new BtContext());

                var firstChild = (MockBtBehaviour)_childrenWatcher.Behaviours[0];

                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Running));
                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));

                Assert.That(firstChild.DoTickCount, Is.EqualTo(3));

                Assert.That(_childrenWatcher.NbOfChildrenCalled, Is.EqualTo(2));
            }

            [Test]
            public void WhenThePriorityChanges_LaterNodesShouldBeReset()
            {
                _sut.Tick(new BtContext());

                var firstChild = (MockBtBehaviour)_childrenWatcher.Behaviours[0];
                var secondChild = (MockBtBehaviour)_childrenWatcher.Behaviours[1];
                firstChild.ReturnStatus = BehaviourStatus.Running;

                var behaviourStatus =_sut.Tick(new BtContext());

                Assert.That(_sut.Status, Is.EqualTo(BehaviourStatus.Running));
                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));

                Assert.That(firstChild.DoTickCount, Is.EqualTo(2));
                Assert.That(secondChild.DoTickCount, Is.EqualTo(1));
                Assert.That(secondChild.Status, Is.EqualTo(BehaviourStatus.Ready));

                Assert.That(_childrenWatcher.NbOfChildrenCalled, Is.EqualTo(2));
            }
        }
    }
}
