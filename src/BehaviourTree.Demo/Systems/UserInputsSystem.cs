using System.Windows.Forms;
using BehaviourTree.Demo.GameEngine;

namespace BehaviourTree.Demo.Systems
{
    public sealed class UserInputsSystem : ISystem
    {
        public void Update(long ellapsedMilliseconds)
        {
            Application.DoEvents();
        }
    }
}
