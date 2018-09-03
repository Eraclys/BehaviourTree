namespace BehaviourTree.Tests.Utils
{
    public sealed class MockRandomProvider : IRandomProvider
    {
        private double _randomDoubleValue;
        private int _randomIntegerValue;

        public double NextRandomDouble()
        {
            return _randomDoubleValue;
        }

        public int NextRandomInteger(int maxValue)
        {
            return _randomIntegerValue;
        }

        public void SetNextRandomDouble(double value)
        {
            _randomDoubleValue = value;
        }

        public void SetNextRandomInteger(int value)
        {
            _randomIntegerValue = value;
        }
    }
}