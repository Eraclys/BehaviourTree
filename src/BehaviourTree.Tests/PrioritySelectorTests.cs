﻿using System.Linq;
using BehaviourTree.Composites;
using BehaviourTree.Tests.Utils;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class PrioritySelectorTests
    {
        [Test]
        public void WhenAllChildrenReturnFailure_ReturnFailure()
        {
            var behaviours = Enumerable.Range(0, 10)
                .Select(x => new MockBehaviour { ReturnStatus = BehaviourStatus.Failed })
                .ToArray();

            var sut = new PrioritySelector(behaviours);

            var behaviourStatus = sut.Tick(new BtContext());

            Assert.That(behaviourStatus, Is.EqualTo(BehaviourStatus.Failed));
            Assert.That(behaviours.AllInitialized(), Is.True);
            Assert.That(behaviours.AllUpdated(), Is.True);
            Assert.That(behaviours.AllTerminated(), Is.True);
        }

        [TestCase(BehaviourStatus.Succeeded)]
        [TestCase(BehaviourStatus.Running)]
        public void WhenAChildReturnsSuccessOrRunning_ReturnTheSameAndDoNotCallNextChildInSequence(BehaviourStatus status)
        {
            var behaviours = Enumerable.Range(0, 10)
                .Select(x => new MockBehaviour { ReturnStatus = BehaviourStatus.Failed })
                .ToArray();

            behaviours[4].ReturnStatus = status;

            var sut = new PrioritySelector(behaviours);

            var behaviourStatus = sut.Tick(new BtContext());

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
        public void WhenTicked_ReevaluateAllPreviousChildren()
        {
            var behaviours = Enumerable.Range(0, 10)
                .Select(x => new MockBehaviour { ReturnStatus = BehaviourStatus.Failed })
                .ToArray();

            behaviours[4].ReturnStatus = BehaviourStatus.Running;

            var sut = new PrioritySelector(behaviours);

            sut.Tick(new BtContext());
            behaviours[4].ReturnStatus = BehaviourStatus.Succeeded;
            sut.Tick(new BtContext());


            for (int i = 0; i < 4; i++)
            {
                var mockBehaviour = behaviours[i];

                Assert.That(mockBehaviour.InitializeCallCount, Is.EqualTo(1));
                Assert.That(mockBehaviour.UpdateCallCount, Is.EqualTo(2));
                Assert.That(mockBehaviour.TerminateCallCount, Is.EqualTo(2));
            }

            Assert.That(behaviours[4].InitializeCallCount, Is.EqualTo(1));
            Assert.That(behaviours[4].UpdateCallCount, Is.EqualTo(2));
            Assert.That(behaviours[4].TerminateCallCount, Is.EqualTo(1));

            for (int i = 5; i < behaviours.Length; i++)
            {
                var mockBehaviour = behaviours[i];

                Assert.That(mockBehaviour.InitializeCallCount, Is.EqualTo(0));
                Assert.That(mockBehaviour.UpdateCallCount, Is.EqualTo(0));
                Assert.That(mockBehaviour.TerminateCallCount, Is.EqualTo(0));
            }
        }

        [Test]
        public void WhenAReevaluatedChildReturnsSuccess_ReturnSuccessAndResetChildren()
        {
            var behaviours = Enumerable.Range(0, 10)
                .Select(x => new MockBehaviour { ReturnStatus = BehaviourStatus.Failed })
                .ToArray();

            behaviours[4].ReturnStatus = BehaviourStatus.Running;

            var sut = new PrioritySelector(behaviours);

            sut.Tick(new BtContext());
            behaviours[0].ReturnStatus = BehaviourStatus.Succeeded;
            sut.Tick(new BtContext());

            Assert.That(behaviours[0].InitializeCallCount, Is.EqualTo(1));
            Assert.That(behaviours[0].UpdateCallCount, Is.EqualTo(2));
            Assert.That(behaviours[0].TerminateCallCount, Is.EqualTo(2));
            Assert.That(behaviours[0].ResetCount, Is.EqualTo(1));

            for (int i = 1; i < 4; i++)
            {
                var mockBehaviour = behaviours[i];

                Assert.That(mockBehaviour.InitializeCallCount, Is.EqualTo(1));
                Assert.That(mockBehaviour.UpdateCallCount, Is.EqualTo(1));
                Assert.That(mockBehaviour.TerminateCallCount, Is.EqualTo(1));
                Assert.That(mockBehaviour.ResetCount, Is.EqualTo(1));
            }

            Assert.That(behaviours[4].InitializeCallCount, Is.EqualTo(1));
            Assert.That(behaviours[4].UpdateCallCount, Is.EqualTo(1));
            Assert.That(behaviours[4].TerminateCallCount, Is.EqualTo(0));
            Assert.That(behaviours[4].ResetCount, Is.EqualTo(1));

            for (int i = 5; i < behaviours.Length; i++)
            {
                var mockBehaviour = behaviours[i];

                Assert.That(mockBehaviour.InitializeCallCount, Is.EqualTo(0));
                Assert.That(mockBehaviour.UpdateCallCount, Is.EqualTo(0));
                Assert.That(mockBehaviour.TerminateCallCount, Is.EqualTo(0));
                Assert.That(mockBehaviour.ResetCount, Is.EqualTo(0));
            }
        }
    }
}