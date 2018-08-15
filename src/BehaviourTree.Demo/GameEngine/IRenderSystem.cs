namespace BehaviourTree.Demo.GameEngine
{
    public interface IRenderSystem
    {
        void Render(long ellapsedMilliseconds, float interpolation);
    }
}