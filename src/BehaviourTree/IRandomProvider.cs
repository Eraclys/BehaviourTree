namespace BehaviourTree
{
    public interface IRandomProvider
    {
        /// <summary>Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.</summary>
        /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        double NextRandomDouble();
    }
}
