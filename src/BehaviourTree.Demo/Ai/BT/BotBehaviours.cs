using BehaviourTree.Demo.Components;
using BehaviourTree.FluentBuilder;

namespace BehaviourTree.Demo.Ai.BT
{
    internal static class BotBehaviours
    {
        public static IBehaviour<BtContext> BotBehaviour()
        {
            return FluentBuilder.FluentBuilder.Create<BtContext>()
                .PrioritySelector("Root")
                    .Subtree(LowHealthBehaviour())
                    .Subtree(TiredBehaviour())
                    .Subtree(BuildHouseBehaviour())
                    .Subtree(CollectFoodBehaviour())
                .End()
                .Build();
        }

        private static IBehaviour<BtContext> CollectFoodBehaviour()
        {
            return FindAndPickupItem(ItemTypes.Food);
        }

        private static IBehaviour<BtContext> LowHealthBehaviour()
        {
            return FluentBuilder.FluentBuilder.Create<BtContext>()
                .Sequence("Low health")
                    .Condition("Is health low?", BotBehaviourFunctions.IsHealthLow)
                    .Selector("Find and eat food")
                        .Do("Eat food from inventory", BotBehaviourFunctions.EatFoodFromInventory)
                        .Subtree(FindAndPickupItem(ItemTypes.Food))
                    .End()
                .End()
                .Build();
        }

        private static IBehaviour<BtContext> TiredBehaviour()
        {
            return FluentBuilder.FluentBuilder.Create<BtContext>()
                .Sequence("Low stamina")
                    .Condition("Is stamina low?", BotBehaviourFunctions.IsStaminaLow)
                    .Subtree(FindAndPickupItem(ItemTypes.Food))
                .End()
                .Build();
        }

        private static IBehaviour<BtContext> MineStoneBehaviour()
        {
            return FluentBuilder.FluentBuilder.Create<BtContext>()
                .PrioritySequence("Mine stone")
                    .Selector("Get pickaxe")
                        .Condition("Has pickaxe?", c => BotBehaviourFunctions.HasItem(c, ItemTypes.Pickaxe, 1))
                        .Subtree(FindAndPickupItem(ItemTypes.Pickaxe))
                    .End()
                    .Subtree(FindAndPickupItem(ItemTypes.Stone))
                .End()
                .Build();
        }

        private static IBehaviour<BtContext> ChopWoodBehaviour()
        {
            return FluentBuilder.FluentBuilder.Create<BtContext>()
                .PrioritySequence("Chop wood")
                    .Selector("Get axe")
                        .Condition("Has axe?", c => BotBehaviourFunctions.HasItem(c, ItemTypes.Axe, 1))
                        .Subtree(FindAndPickupItem(ItemTypes.Axe))
                    .End()
                    .Subtree(FindAndPickupItem(ItemTypes.Wood))
                .End()
                .Build();
        }

        private static IBehaviour<BtContext> BuildHouseBehaviour()
        {
            const int requiredStones = 2;
            const int requiredWood = 4;

            var gatherRequiredResources = FluentBuilder.FluentBuilder.Create<BtContext>()
                .Sequence("Gather resources")
                    .Selector("Gather wood")
                        .Condition("Has enough wood?", c => BotBehaviourFunctions.HasItem(c, ItemTypes.Wood, requiredWood))
                        .Subtree(ChopWoodBehaviour())
                    .End()
                    .Selector("Gather stone")
                        .Condition("Has enough stone?", c => BotBehaviourFunctions.HasItem(c, ItemTypes.Stone, requiredStones))
                        .Subtree(MineStoneBehaviour())
                    .End()
                .End()
                .Build();

            return FluentBuilder.FluentBuilder.Create<BtContext>()
                .PrioritySequence("Build house")
                    .Subtree(gatherRequiredResources)
                    .Do("Spawn house", c => BotBehaviourFunctions.BuildHouse(c, requiredStones, requiredWood))
                .End()
                .Build();
        }

        private static IBehaviour<BtContext> FindAndPickupItem(ItemTypes itemType)
        {
            return FluentBuilder.FluentBuilder.Create<BtContext>()
                .Sequence("Find and pick up item")
                    .Do($"Set closest [{itemType}] as target", c => BotBehaviourFunctions.SetItemAsTarget(c, itemType))
                    .Do("Move to target", BotBehaviourFunctions.MoveToTargetEntity)
                    .Do("Pickup target", BotBehaviourFunctions.PickupTarget)
                .End()
                .Build();
        }
    }
}
