using System.Globalization;

namespace Albion.Common.Math
{
    public struct FixPoint
    {
        public const long InternalFaktor = 10000L;
        public static readonly FixPoint One = new FixPoint(InternalFaktor);
        public double FloatValue => (double)InternalValue / InternalFaktor;
        public long IntegerValue => InternalValue / InternalFaktor;

        public long InternalValue
        {
            get;
        }

        private FixPoint(long internalValue)
        {
            InternalValue = internalValue;
        }

        public static FixPoint FromInternalValue(long internalValue)
        {
            return new FixPoint(internalValue);
        }

        public static FixPoint FromFloatingPointValue(double value)
        {
            value = System.Math.Min(System.Math.Max(value, double.MinValue), double.MaxValue);
            return new FixPoint((long)System.Math.Round(value * InternalFaktor));
        }

        public override string ToString()
        {
            return FloatValue.ToString(CultureInfo.InvariantCulture);
        }

        public static FixPoint operator +(FixPoint a, FixPoint b)
        {
            return new FixPoint(a.InternalValue + b.InternalValue);
        }

        public static FixPoint operator -(FixPoint a, FixPoint b)
        {
            return new FixPoint(a.InternalValue - b.InternalValue);
        }

        public static FixPoint operator -(FixPoint a)
        {
            return new FixPoint(-a.InternalValue);
        }

        public static FixPoint operator *(FixPoint a, FixPoint b)
        {
            return new FixPoint(a.InternalValue * b.InternalValue / InternalFaktor);
        }

        public static FixPoint operator *(FixPoint a, int b)
        {
            return new FixPoint(a.InternalValue * b);
        }

        public static FixPoint operator *(int b, FixPoint a)
        {
            return new FixPoint(a.InternalValue * b);
        }

        public static FixPoint operator *(FixPoint a, long b)
        {
            return new FixPoint(a.InternalValue * b);
        }

        public static FixPoint operator *(long b, FixPoint a)
        {
            return new FixPoint(a.InternalValue * b);
        }
    }
}
