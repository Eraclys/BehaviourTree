using System;

namespace BehaviourTree
{
    public struct ElaspedTicks : IEquatable<ElaspedTicks>
    {
        private readonly long _value;

        private ElaspedTicks(long value)
        {
            if (value < 0)
            {
                throw new ArgumentException("ElaspedTicks must be positive", nameof(value));
            }

            _value = value;
        }

        public static ElaspedTicks From(long ticks)
        {
            return new ElaspedTicks(ticks);
        }

        public static implicit operator long(ElaspedTicks value)
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
            return _value.GetHashCode();
        }

        #endregion
    }
}
