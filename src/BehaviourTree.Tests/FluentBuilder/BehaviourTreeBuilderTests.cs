using System;
using BehaviourTree.FluentBuilder;
using NUnit.Framework;

namespace BehaviourTree.Tests.FluentBuilder
{
    [TestFixture]
    internal sealed class BehaviourTreeBuilderTests
    {
        [Test]
        public void Test()
        {
            var subTree = BehaviourTree.FluentBuilder.FluentBuilder.Create<MockContext>()
                .LimitCallRate("LimitCallRate1", 963)
                    .AlwaysSucceed("AlwaysSucceed1")
                        .AlwaysFail("AlwaysFail1")
                            .UntilSuccess("UntilSuccess1")
                                .Do("action9", _ => BehaviourStatus.Failed)
                            .End()
                        .End()
                    .End()
                .End()
                .Build();

            var tree = BehaviourTree.FluentBuilder.FluentBuilder.Create<MockContext>()
                .Selector("Selector1")
                    .Subtree(subTree)
                    .Sequence("PrioritySelector1")
                        .PrioritySelector("PrioritySelector1")
                            .Do("action1", _ => BehaviourStatus.Succeeded)
                            .Do("action2", _ => BehaviourStatus.Succeeded)
                        .End()
                        .Random("Random1", 0.6)
                            .Do("action3", _ => BehaviourStatus.Succeeded)
                        .End()
                    .End()
                    .PrioritySequence("PrioritySequence1")
                        .Condition("Condition1", _ => true)
                        .Wait("Wait1", 456)
                        .SimpleParallel("SimpleParallel1")
                            .AutoReset("AutoReset1")
                                .Cooldown("Cooldown1", 789)
                                    .Repeat("Repeat1", 4)
                                        .Condition("Condition5", _ => false)
                                    .End()
                                .End()
                            .End()
                            .TimeLimit("Timelimit1", 147)
                                .UntilFailed("UntilFailed1")
                                    .Invert("Invert1")
                                        .Condition("Condition2", _ => false)
                                    .End()
                                .End()
                            .End()
                        .End()
                    .End()
                    .RandomSelector("RandomSelector1")
                        .Do("action10", _ => BehaviourStatus.Succeeded)
                        .Do("action11", _ => BehaviourStatus.Succeeded)
                    .End()
                    .RandomSequence("RandomSequence1")
                        .Do("action12", _ => BehaviourStatus.Succeeded)
                        .Do("action13", _ => BehaviourStatus.Succeeded)
                    .End()
                .End()
                .Build();

            var result = BehaviourTreeExpressionPrinter<MockContext>.GetExpression(tree);

            Console.Write(result);

            // TODO: string comparison is flaky on build server
        }

        public class MockContext : IClock
        {
            public long GetTimeStampInMilliseconds()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
