namespace BehaviourTree
{
    public sealed class Random : Composite
    {
        private static readonly System.Random Rng = new System.Random();
        private int _currentIndex;


        public Random(IBehaviour[] children) : base(children)
        {
        }

        protected override void DoStart()
        {
            _currentIndex = Rng.Next(0, Children.Length);
            Children[_currentIndex].Start();
        }

        protected override void DoStop()
        {
            Children[_currentIndex].Stop();
        }

        public override void OnChildStopped(IBehaviour child, bool success)
        {
            RaiseStopped(success);
        }
    }
}