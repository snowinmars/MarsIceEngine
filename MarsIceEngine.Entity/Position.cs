using MarsIceEngine.Common;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

namespace MarsIceEngine.Entity
{
    public struct Position : IComparable, IComparable<Position>, ICloneable
    {
        public static readonly Position Zero;

        static Position()
        {
            Zero = new Position(0, 0);
        }

        public Position(int x, int y) : this((double)x, y)
        {
        }

        public Position(float x, float y) : this((double)x, y)
        {
        }

        public Position(double x, double y)
        {
            this.X = Normalize(x);
            this.Y = Normalize(y);
        }

        public Position(Point point) : this(point.X, point.Y)
        {
        }

        public Position(Vector2 vector) : this(vector.X, vector.Y)
        {
        }

        public double X { get; set; }

        public double Y { get; set; }

        public static Position operator -(Position pos)
            => new Position(-pos.X, -pos.Y);

        public static Position operator -(Position lhs, Position rhs)
            => new Position(lhs.X - rhs.X, lhs.Y - rhs.Y);

        public static Position operator *(Position vector, double number)
            => new Position(vector.X * number, vector.Y * number);

        public static Position operator *(double number, Position vector)
            => vector * number;

        public static Position operator *(Position vector, float number)
            => vector * (double)number;

        public static Position operator *(float number, Position vector)
            => vector * number;

        public static Position operator *(Position vector, int number)
            => vector * (double)number;

        public static Position operator *(int number, Position vector)
            => vector * number;

        public static Position operator +(Position pos)
            => new Position(+pos.X, +pos.Y);

        public static Position operator +(Position lhs, Position rhs)
            => new Position(lhs.X + rhs.X, lhs.Y + rhs.Y);

        public Position Clone()
        {
            return new Position(this.X, this.Y);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public void SetZero()
        {
            var zero = Zero;

            this.X = zero.X;
            this.Y = zero.Y;
        }

        public Point ToPoint()
        {
            return new Point((int)this.X, (int)this.Y);
        }

        public override string ToString()
        {
            return $"X: {this.X}, Y: {this.Y}";
        }

        public Vector2 ToVector2()
        {
            return new Vector2((float)this.X, (float)this.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static double Normalize(double value)
        {
            if (double.IsNaN(value))
            {
                return 0;
            }

            if (double.IsPositiveInfinity(value))
            {
                return double.MaxValue;
            }

            if (double.IsNegativeInfinity(value))
            {
                return double.MinValue;
            }

            return value;
        }

        #region compareoperators

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AreEqualsByX(Position other)
        {
            return Math.Abs(this.X - other.X) < float.Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AreEqualsByY(Position other)
        {
            return Math.Abs(this.Y - other.Y) < float.Epsilon;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsGreaterByX(Position other)
        {
            return this.X > other.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsGreaterByY(Position other)
        {
            return this.Y > other.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsGreaterOrEqualByX(Position other)
        {
            return this.X >= other.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsGreaterOrEqualByY(Position other)
        {
            return this.Y >= other.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLesserByX(Position other)
        {
            return this.X < other.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLesserByY(Position other)
        {
            return this.Y < other.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLesserOrEqualByX(Position other)
        {
            return this.X <= other.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsLesserOrEqualByY(Position other)
        {
            return this.Y <= other.Y;
        }

        #endregion compareoperators

        #region Equals

        public static bool operator !=(Position lhs, Position rhs)
            => !(lhs == rhs);

        public static bool operator ==(Position lhs, Position rhs)
        {
            return lhs.CompareTo(rhs) == 0;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Position position))
            {
                return -1;
            }

            return this.CompareTo(position);
        }

        public int CompareTo(Position other)
        {
            if (this.AreEqualsByX(other) &&
                this.AreEqualsByY(other))
            {
                return 0;
            }

            return IsGreaterByX(other) && IsGreaterByY(other) ? 1 : -1;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Position position))
            {
                return false;
            }

            return this.Equals(position);
        }

        public bool Equals(Position pos)
        {
            return this.CompareTo(pos) == 0;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.X.GetHashCode() * 397) ^ this.Y.GetHashCode();
            }
        }

        #endregion Equals
    }
}