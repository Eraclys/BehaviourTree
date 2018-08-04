using BehaviourTree;
using BehaviourTree.FluentBuilder;
using NUnit.Framework;

namespace BehaviourTreeBuilder.Tests
{
    [TestFixture]
    internal sealed class BehaviourTreeBuilderTests
    {
        [Test]
        public void Test()
        {
            var subTree = FluentBuilder.Create<Clock>()
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

            var tree = FluentBuilder.Create<Clock>()
                .Selector("Selector1")
                    .Subtree("Subtree1", subTree)
                    .Sequence("PrioritySelector1")
                        .PrioritySelector("PrioritySelector1")
                            .Do("action1", _ => BehaviourStatus.Succeeded)
                            .Do("action2", _ => BehaviourStatus.Succeeded)
                        .End()
                        .Do("action3", _ => BehaviourStatus.Succeeded)
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
                .End()
                .Build();

            // TODO: generate same tree by hand and assert equality
        }
    }
}
