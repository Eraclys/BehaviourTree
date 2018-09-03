using BehaviourTree.Demo.Ai.BT;
using BehaviourTree.Demo.Components;
using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.UI;
using System;
using System.Drawing;
using System.Numerics;

namespace BehaviourTree.Demo
{
    public sealed class World
    {
        private readonly Engine _engine;
        private readonly Size _mapSize;
        private readonly int _playableAreaStartPosition;
        private readonly Font _font;
        private static readonly Random Random = new Random();

        public World(Engine engine, Size mapSize, Font font)
        {
            _engine = engine;
            _mapSize = mapSize;
            _playableAreaStartPosition = mapSize.Width / 3;
            _font = font;
        }

        public void Create()
        {
            GenerateGrass();
            GenerateStones();
            GenerateTrees();
            GenerateFood();
            CreatePickaxe();
            CreateAxe();
            CreatePickaxe();
            CreateAxe();

            const int botNumber = 1;

            for (var i = 0; i < botNumber; i++)
            {
                CreateBot(logger: i+1 == botNumber);
            }
        }

        private void CreatePickaxe()
        {
            var asset = Assets.Loot;

            _engine.NewEntity()
                .AddComponent(new RenderComponent(new StaticImage(asset)))
                .AddComponent(new ItemComponent(ItemTypes.Pickaxe))
                .AddComponent(new LootableComponent())
                .AddComponent(new PositionComponent(GetRandomMapPosition(asset.Size)));
        }

        private void CreateAxe()
        {
            var asset = Assets.Loot;

            _engine.NewEntity()
                .AddComponent(new RenderComponent(new StaticImage(asset)))
                .AddComponent(new ItemComponent(ItemTypes.Axe))
                .AddComponent(new LootableComponent())
                .AddComponent(new PositionComponent(GetRandomMapPosition(asset.Size)));
        }

        private void GenerateGrass()
        {
            var grass = Assets.Grass;

            var backgroundImage = new Bitmap(_mapSize.Width, _mapSize.Height);

            using (var graphics = Graphics.FromImage(backgroundImage))
            {
                var xCount = _mapSize.Width / grass.Width;
                var yCount = Math.Ceiling(_mapSize.Height / (float)grass.Height);

                for (var i = 0; i < xCount; i++)
                {
                    for (var j = 0; j < yCount; j++)
                    {
                        graphics.DrawImage(grass, new Point(i*grass.Width, j*grass.Height));
                    }
                }
            }

            _engine.NewEntity()
                .AddComponent(new PositionComponent(new Vector2(backgroundImage.Width/2,backgroundImage.Height/2)))
                .AddComponent(new RenderComponent(new StaticImage(backgroundImage)));
        }

        private void CreateBot(bool logger)
        {
            var inventoryComponent = new InventoryComponent();

            var behaviour = BotBehaviours.BotBehaviour();

            var entity = _engine.NewEntity()
                .AddComponent(new MovementComponent { Velocity = new Vector2(2, 2) })
                .AddComponent(new HealthComponent(100))
                .AddComponent(new StaminaComponent(100))
                .AddComponent(new StaminaBarComponent())
                .AddComponent(new HealthBarComponent())
                .AddComponent(new InventoryViewComponent())
                .AddComponent(inventoryComponent)
                .AddComponent(new BTBehaviourComponent(behaviour));

            var renderable = new BotView(entity);

            entity
                .AddComponent(new PositionComponent(GetRandomMapPosition(renderable.Size)))
                .AddComponent(new RenderComponent(renderable));

            if (logger)
            {
                CreateBehaviourLogger(behaviour);
            }
        }

        private void CreateBehaviourLogger(IBehaviour<BtContext> behaviour)
        {
            _engine.NewEntity()
                .AddComponent(new PositionComponent(Vector2.Zero))
                .AddComponent(new RenderComponent(new BehaviourTreeView(behaviour, _font, _mapSize)));
        }

        private void GenerateStones()
        {
            const int nbOfStones = 6;
            var asset = Assets.Stone;

            for (var i = 0; i < nbOfStones; i++)
            {
                _engine.NewEntity()
                    .AddComponent(new RenderComponent(new StaticImage(asset)))
                    .AddComponent(new ItemComponent(ItemTypes.Stone))
                    .AddComponent(new LootableComponent())
                    .AddComponent(new PositionComponent(GetRandomMapPosition(asset.Size)));
            }
        }

        private void GenerateTrees()
        {
            const int nbOfTrees = 12;
            var asset = Assets.Tree;

            for (var i = 0; i < nbOfTrees; i++)
            {
                _engine.NewEntity()
                    .AddComponent(new RenderComponent(new StaticImage(asset)))
                    .AddComponent(new ItemComponent(ItemTypes.Wood))
                    .AddComponent(new LootableComponent())
                    .AddComponent(new PositionComponent(GetRandomMapPosition(asset.Size)));
            }
        }

        private void GenerateFood()
        {
            const int nbOfFoodItems = 12;
            var asset = Assets.Food;

            for (var i = 0; i < nbOfFoodItems; i++)
            {
                _engine.NewEntity()
                    .AddComponent(new RenderComponent(new StaticImage(asset)))
                    .AddComponent(new ItemComponent(ItemTypes.Food))
                    .AddComponent(new LootableComponent())
                    .AddComponent(new PositionComponent(GetRandomMapPosition(asset.Size)));
            }
        }

        private Vector2 GetRandomMapPosition(Size size)
        {
            var x = _playableAreaStartPosition + Random.Next(_mapSize.Width- _playableAreaStartPosition - size.Width/2);
            var y = Random.Next(_mapSize.Height - size.Height/2);

            return new Vector2(x, y);
        }
    }
}
