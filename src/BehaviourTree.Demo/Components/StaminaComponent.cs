using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Components
{
    public sealed class StaminaComponent : IComponent
    {
        public double MaxStamina { get; }
        public double Stamina { get; private set; }

        public StaminaComponent(double maxStamina)
        {
            Stamina = maxStamina;
            MaxStamina = maxStamina;
        }

        public void ReduceBy(double amount)
        {
            Stamina -= amount;

            if (Stamina < 0)
            {
                Stamina = 0;
            }
        }

        public void IncreaseBy(double amount)
        {
            Stamina += amount;

            if (Stamina > MaxStamina)
            {
                Stamina = MaxStamina;
            }
        }
    }
}
