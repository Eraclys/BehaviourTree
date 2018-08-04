using System.Linq;
using BehaviourTree.Composites;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class SequenceTests
    {
        [Test]
        public void WhenAllChildrenReturnSuccess_ReturnSuccess()
        {
            var behaviours = Enumerable.Range(0, 10)
                .Select(x => new MockBehaviour {ReturnStatus = BehaviourStatus.Succeeded})
                .ToArray();

            var sut = new Sequence<MockContext>(behaviours);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Succeeded));
            Assert.That(behaviours.AllInitialized(), Is.True);
            Assert.That(behaviours.AllUpdated(), Is.True);
            Assert.That(behaviours.AllTerminated(), Is.True);
        }

        [TestCase(BehaviourStatus.Failed)]
        [TestCase(BehaviourStatus.Running)]
        public void WhenAChildDoesNotReturnsFailedOrRunning_ReturnTheSameAndDoNotCallNextChildInSequence(BehaviourStatus status)
        {
            var behaviours = Enumerable.Range(0, 10)
                .Select(x => new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded })
                .ToArray();

            behaviours[4].ReturnStatus = status;

            var sut = new Sequence<MockContext>(behaviours);

            var behaviourStatus = sut.Tick(new MockContext());

            Assert.That(behaviourStatus, Is.EqualTo(status));

            for (int i = 0; i < 4; i++)
            {
                var mockBehaviour = behaviours[i];

                Assert.That(mockBehaviour.InitializeCallCount, Is.EqualTo(1));
                Assert.That(mockBehaviour.UpdateCallCount, Is.EqualTo(1));
            }

            for (int i = 5; i < behaviours.Length; i++)
            {
                var mockBehaviour = behaviours[i];

                Assert.That(mockBehaviour.InitializeCallCount, Is.EqualTo(0));
                Assert.That(mockBehaviour.UpdateCallCount, Is.EqualTo(0));
                Assert.That(mockBehaviour.TerminateCallCount, Is.EqualTo(0));
            }
        }

        [Test]
        public void WhenAChildTakesMultipleTicksToComplete_ResumeSequenceFromRunningChild()
        {
            var behaviours = Enumerable.Range(0, 10)
                .Select(x => new MockBehaviour { ReturnStatus = BehaviourStatus.Succeeded })
                .ToArray();

            behaviours[4].ReturnStatus = BehaviourStatus.Running;

            var sut = new Sequence<MockContext>(behaviours);

            sut.Tick(new MockContext());
            behaviours[4].ReturnStatus = BehaviourStatus.Succeeded;
            sut.Tick(new MockContext());


            for (int i = 0; i < behaviours.Length; i++)
            {
                if (i == 4)
                {
                    continue;
                }

                var mockBehaviour = behaviours[i];

                Assert.That(mockBehaviour.InitializeCallCount, Is.EqualTo(1));
                Assert.That(mockBehaviour.UpdateCallCount, Is.EqualTo(1));
                Assert.That(mockBehaviour.TerminateCallCount, Is.EqualTo(1));
            }

            Assert.That(behaviours[4].InitializeCallCount, Is.EqualTo(1));
            Assert.That(behaviours[4].UpdateCallCount, Is.EqualTo(2));
            Assert.That(behaviours[4].TerminateCallCount, Is.EqualTo(1));
        }
    }
}
