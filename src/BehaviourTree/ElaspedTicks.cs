using System;

namespace BehaviourTree
{
    public struct ElaspedTicks : IEquatable<ElaspedTicks>
    {
        private readonly int _value;

        private ElaspedTicks(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("ElaspedTicks must be positive", nameof(value));
            }

            _value = value;
        }

        public static ElaspedTicks From(int ticks)
        {
            return new ElaspedTicks(ticks);
        }

        public static implicit operator int (ElaspedTicks value)
        {
            return value._value;
        }

        #region Equality

        public bool Equals(ElaspedTicks other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            return obj is ElaspedTicks && Equals((ElaspedTicks) obj);
        }

        public override int GetHashCode()
        {
            return _value;
        }

        #endregion
    }
}
