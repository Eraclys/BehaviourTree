using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    public sealed class InvertTests
    {
        [TestFixture]
        public sealed class GivenChildrenReturnsSuccess
        {
            private WatchMock _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new WatchMock(new Invert(new TerminateImediatlyMockBehaviour(BehaviourStatus.Inactive, true)));
            }

            [Test]
            public void WhenRunningToEnd_ShouldReturnFailure()
            {
                _sut.Start();

                Assert.That(_sut.IsSuccess, Is.EqualTo(false));
            }
        }

        [TestFixture]
        public sealed class GivenChildrenReturnsFailure
        {
            private WatchMock _sut;

            [SetUp]
            public void Setup()
            {
                _sut = new WatchMock(new Invert(new TerminateImediatlyMockBehaviour(BehaviourStatus.Inactive, false)));
            }

            [Test]
            public void WhenRunningToEnd_ShouldReturnFailure()
            {
                _sut.Start();

                Assert.That(_sut.IsSuccess, Is.EqualTo(true));
            }
        }
    }
}
