using BehaviourTree.Composites;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class SimpleParallelTests
    {
        [TestCase(SimpleParallelPolicy.OnlyOneMustSucceed)]
        [TestCase(SimpleParallelPolicy.BothMustSucceed)]
        public void WhenFirstTicked_ChildrenShouldAllBeStarted(SimpleParallelPolicy policy)
        {
            var first = new MockBehaviour {ReturnStatus = BehaviourStatus.Running};
            var second = new MockBehaviour {ReturnStatus = BehaviourStatus.Running};

            var sut = new SimpleParallel<MockContext>(policy, first, second);

            sut.Tick(new MockContext());

            Assert.That(first.InitializeCallCount, Is.EqualTo(1));
            Assert.That(second.InitializeCallCount, Is.EqualTo(1));
            Assert.That(first.UpdateCallCount, Is.EqualTo(1));
            Assert.That(second.UpdateCallCount, Is.EqualTo(1));
        }

        [TestCase(SimpleParallelPolicy.OnlyOneMustSucceed)]
        [TestCase(SimpleParallelPolicy.BothMustSucceed)]
        public void WhenTicked_RunningChildrenShouldAllBeTicked(SimpleParallelPolicy policy)
        {
            var first = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };
            var second = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };

            var sut = new SimpleParallel<MockContext>(policy, first, second);

            sut.Tick(new MockContext());
            sut.Tick(new MockContext());

            Assert.That(first.UpdateCallCount, Is.EqualTo(2));
            Assert.That(second.UpdateCallCount, Is.EqualTo(2));
        }

        [TestCase(SimpleParallelPolicy.OnlyOneMustSucceed)]
        [TestCase(SimpleParallelPolicy.BothMustSucceed)]
        public void WhenTickedAndInACompletedStatus_ChildrenShouldAllBeTicked(SimpleParallelPolicy policy)
        {
            var first = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
            var second = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };

            var sut = new SimpleParallel<MockContext>(policy, first, second);

            sut.Tick(new MockContext());
            sut.Tick(new MockContext());

            Assert.That(first.UpdateCallCount, Is.EqualTo(2));
            Assert.That(second.UpdateCallCount, Is.EqualTo(2));
        }

        [TestFixture]
        public sealed class GivenOnlyOneMustSucceed
        {
            [Test]
            public void WhenOneFailAndOtherIsStillRunning_ReturnRunning()
            {
                var first = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };
                var second = new MockBehaviour { ReturnStatus = BehaviourStatus.Failed };

                var sut = new SimpleParallel<MockContext>(SimpleParallelPolicy.OnlyOneMustSucceed, first, second);

                var behaviourStatus = sut.Tick(new MockContext());

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
            }

            [Test]
            public void WhenOneSucceed_ReturnSuccessAndReset()
            {
                var first = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
                var second = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };

                var sut = new SimpleParallel<MockContext>(SimpleParallelPolicy.OnlyOneMustSucceed, first, second);

                var behaviourStatus = sut.Tick(new MockContext());

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
                Assert.That(first.ResetCount, Is.EqualTo(1));
                Assert.That(second.ResetCount, Is.EqualTo(1));
            }

            [Test]
            public void WhenBothFail_ReturnFailure()
            {
                var first = new MockBehaviour { ReturnStatus = BehaviourStatus.Failed };
                var second = new MockBehaviour { ReturnStatus = BehaviourStatus.Failed };

                var sut = new SimpleParallel<MockContext>(SimpleParallelPolicy.OnlyOneMustSucceed, first, second);

                var behaviourStatus = sut.Tick(new MockContext());

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            }
        }

        [TestFixture]
        public sealed class GivenBothMustSucceed
        {
            [Test]
            public void WhenTickedAndSomeChildrenAreStillRunning_CompletedChildrenShouldNotBeTicked()
            {
                var first = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
                var second = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };

                var sut = new SimpleParallel<MockContext>(SimpleParallelPolicy.BothMustSucceed, first, second);

                sut.Tick(new MockContext());
                sut.Tick(new MockContext());

                Assert.That(first.UpdateCallCount, Is.EqualTo(1));
                Assert.That(second.UpdateCallCount, Is.EqualTo(2));
            }

            [Test]
            public void WhenOneSucceedAndOtherIsStillRunning_ReturnRunning()
            {
                var first = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
                var second = new MockBehaviour { ReturnStatus = BehaviourStatus.Running };

                var sut = new SimpleParallel<MockContext>(SimpleParallelPolicy.BothMustSucceed, first, second);

                var behaviourStatus = sut.Tick(new MockContext());

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Running));
            }

            [Test]
            public void WheOneFails_ReturnFailureAndReset()
            {
                var first = new MockBehaviour { ReturnStatus = BehaviourStatus.Failed };
                var second = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };

                var sut = new SimpleParallel<MockContext>(SimpleParallelPolicy.BothMustSucceed, first, second);

                var behaviourStatus = sut.Tick(new MockContext());

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
                Assert.That(first.ResetCount, Is.EqualTo(1));
                Assert.That(second.ResetCount, Is.EqualTo(1));
            }

            [Test]
            public void WhenBothSucceed_ReturnSuccess()
            {
                var first = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };
                var second = new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded };

                var sut = new SimpleParallel<MockContext>(SimpleParallelPolicy.BothMustSucceed, first, second);

                var behaviourStatus = sut.Tick(new MockContext());

                Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
            }
        }
    }
}
