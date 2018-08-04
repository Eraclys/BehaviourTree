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

            var printer = new BehaviourTreeExpressionPrinter<Clock>();
            printer.Visit(tree);

            var result = printer.ToString();

            Assert.That(result, Is.EqualTo(@"Selector1 
   Subtree1 
      LimitCallRate1 (963)
         AlwaysSucceed1 
            AlwaysFail1 
               UntilSuccess1 
                  action9 
   PrioritySelector1 
      PrioritySelector1 
         action1 
         action2 
      action3 
   PrioritySequence1 
      Condition1 
      Wait (456)
      SimpleParallel1 (BothMustSucceed)
         AutoReset1 
            Cooldown1 (789)
               Repeat1 (4)
                  Condition5 
         Timelimit1 (147)
            UntilFailed1 
               Invert1 
                  Condition2 
"));
        }
    }
}
