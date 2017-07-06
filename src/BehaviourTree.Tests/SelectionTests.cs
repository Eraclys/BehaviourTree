using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    public sealed class SelectionTests
    {
        [TestFixture]
        public sealed class GivenChildrenReturnsSuccess
        {
            private WatchMock _sut;
            private WatchCollectionMock _childrenWatcher;

            [SetUp]
            public void Setup()
            {
                _childrenWatcher = new WatchCollectionMock(
                    new TerminateImediatlyMockBehaviour(BehaviourStatus.Inactive, true),
                    new TerminateImediatlyMockBehaviour(BehaviourStatus.Inactive, true),
                    new TerminateImediatlyMockBehaviour(BehaviourStatus.Inactive, true));

                _sut = new WatchMock(new Selection(_childrenWatcher.Behaviours));
            }

            [Test]
            public void WhenRunningToEnd_ShouldReturnSuccess()
            {
                _sut.Start();

                Assert.That(_sut.IsSuccess, Is.EqualTo(true));
                Assert.That(_childrenWatcher.NbOfChildrenCalled, Is.EqualTo(1));
            }
        }

        [TestFixture]
        public sealed class GivenChildrenReturnsFailure
        {
            private WatchMock _sut;
            private WatchCollectionMock _childrenWatcher;

            [SetUp]
            public void Setup()
            {
                _childrenWatcher = new WatchCollectionMock(
                    new TerminateImediatlyMockBehaviour(BehaviourStatus.Inactive, false),
                    new TerminateImediatlyMockBehaviour(BehaviourStatus.Inactive, false),
                    new TerminateImediatlyMockBehaviour(BehaviourStatus.Inactive, false));

                _sut = new WatchMock(new Selection(_childrenWatcher.Behaviours));
            }

            [Test]
            public void WhenRunningToEnd_ShouldReturnFailure()
            {
                _sut.Start();

                Assert.That(_sut.IsSuccess, Is.EqualTo(false));
                Assert.That(_childrenWatcher.AllChildrenCalled);
            }
        }
    }
}
