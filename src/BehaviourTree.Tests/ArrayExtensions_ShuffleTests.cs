using System;
using NUnit.Framework;

namespace BehaviourTree.Tests
{
    [TestFixture]
    internal sealed class ArrayExtensions
    {
        [Test]
        public void Shuffle_ShouldReturnANewShuffledArray()
        {
            var original = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var shuffled = original.Shuffle(new RandomProvider());

            Console.WriteLine($"original: {string.Join(",", original)}");
            Console.WriteLine($"shuffled: {string.Join(",", shuffled)}");

            Assert.AreNotEqual(original, shuffled);
        }
    }
}
