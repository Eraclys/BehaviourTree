using BehaviourTree.Demo.EventListeners;
using BehaviourTree.Demo.GameEngine;
using BehaviourTree.Demo.Systems;
using BehaviourTree.Demo.UI;
using System;
using System.Windows.Forms;

namespace BehaviourTree.Demo
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var engine = new Engine();

            using (var renderSystem = new HostForm())
            {
                renderSystem.Show();

                var mapSize = renderSystem.ClientRectangle.Size;

                var world = new World(engine, mapSize, renderSystem.Font);
                world.Create();

                engine.AddSystem(new HealthSystem(engine));
                engine.AddSystem(new StaminaSystem(engine));
                engine.AddSystem(new AiSystem(engine));
                engine.AddSystem(new MoveSystem(engine, mapSize));
                engine.AddSystem(new LootableSystem(engine));
                engine.AddSystem(new UserInputsSystem());
                engine.SubscribeToEvent(new OnCharacterDeathAddTombstone());
                engine.SubscribeToEvent(new OnCharacterDeathDropBackpack());
                engine.SubscribeToEvent(new OnHealthReachedZeroRemoveEntity());
                engine.SubscribeToEvent(new OnEntityRemovedClearTargets());

                engine.AddRenderSystem(renderSystem);
                engine.AddRenderSystem(new RenderRenderableComponentsSystem(engine, renderSystem.Graphics));
                engine.AddRenderSystem(new RenderHealthBarSystem(engine, renderSystem.Graphics));
                engine.AddRenderSystem(new RenderStaminaBarSystem(engine, renderSystem.Graphics));
                engine.AddRenderSystem(new RenderInventorySystem(engine, renderSystem.Graphics, renderSystem.Font));

                var gameRunner = new GameRunner(engine);

                gameRunner.Run(shouldStop: () => !renderSystem.Created);
            }
        }
    }
}
