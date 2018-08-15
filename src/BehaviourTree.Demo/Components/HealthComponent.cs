using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Components
{
    public sealed class HealthComponent : IComponent
    {
        public double MaxHealth { get; }
        public double Health { get; private set; }

        public HealthComponent(double maxHealth)
        {
            Health = maxHealth;
            MaxHealth = maxHealth;
        }

        public void ReduceBy(double amount)
        {
            Health -= amount;

            if (Health < 0)
            {
                Health = 0;
            }
        }

        public void IncreaseBy(double amount)
        {
            Health += amount;

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
    }
}
