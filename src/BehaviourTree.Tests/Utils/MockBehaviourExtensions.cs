using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree.Tests.Utils
{
    internal static class MockBehaviourExtensions
    {
        public static int InitializedCount(this IEnumerable<MockBehaviour> behaviours, int minNbOfTimes = 0)
        {
            return behaviours.Count(x => x.InitializeCallCount > minNbOfTimes);
        }

        public static int TerminatedCount(this IEnumerable<MockBehaviour> behaviours, int minNbOfTimes = 0)
        {
            return behaviours.Count(x => x.TerminateCallCount > minNbOfTimes);
        }

        public static int UpdatedCount(this IEnumerable<MockBehaviour> behaviours, int minNbOfTimes = 0)
        {
            return behaviours.Count(x => x.UpdateCallCount > minNbOfTimes);
        }


        public static bool AllInitialized(this IReadOnlyCollection<MockBehaviour> behaviours, int minNbOfTimes = 0)
        {
            return behaviours.InitializedCount(minNbOfTimes) == behaviours.Count;
        }

        public static bool AllUpdated(this IReadOnlyCollection<MockBehaviour> behaviours, int minNbOfTimes = 0)
        {
            return behaviours.UpdatedCount(minNbOfTimes) == behaviours.Count;
        }

        public static bool AllTerminated(this IReadOnlyCollection<MockBehaviour> behaviours, int minNbOfTimes = 0)
        {
            return behaviours.TerminatedCount(minNbOfTimes) == behaviours.Count;
        }
    }
}
