using Microsoft.Xna.Framework;
using System;
using System.Runtime.CompilerServices;

namespace MarsIceEngine.Entity
{
    public enum VectorType
    {
        /// <summary>
        /// Initial point is zero. F.e., radius-vector has standard positioning
        /// </summary>
        Standard = 1,

        /// <summary>
        /// Initial point is not zero
        /// </summary>
        Component = 2,
    }

    public struct PositionVector : IComparable, IComparable<PositionVector>, ICloneable
    {
        public static readonly PositionVector HorizontalUnit;

        public static readonly PositionVector VerticalUnit;

        public static readonly PositionVector Zero;

        static PositionVector()
        {
            VerticalUnit = new PositionVector(0, 0, 0, 1);
            HorizontalUnit = new PositionVector(0, 0, 1, 0);
            Zero = new PositionVector(0, 0, 0, 0);
        }

        public PositionVector(int initialX, int initialY, int terminalX, int terminalY) : this(
                                            new Position(initialX, initialY), new Position(terminalX, terminalY))
        {
        }

        public PositionVector(float initialX, float initialY, float terminalX, float terminalY) : this(
            new Position(initialX, initialY), new Position(terminalX, terminalY))
        {
        }

        public PositionVector(double initialX, double initialY, double terminalX, double terminalY) : this(
            new Position(initialX, initialY), new Position(terminalX, terminalY))
        {
        }

        public PositionVector(Position initialPosition, Position terminalPosition)
        {
            this.InitialPosition = initialPosition;
            this.TerminalPosition = terminalPosition;
        }

        public PositionVector(Vector2 vector)
        {
            var initialPosition = new Position(vector.X, vector.Y);
            var length = vector.Length();

            var terminalPositionX = vector.X + length * vector.Sin();
            var terminalPositionY = vector.Y + length * vector.Cos();

            var terminalPosition = new Position(terminalPositionX, terminalPositionY);

            this.InitialPosition = initialPosition;
            this.TerminalPosition = terminalPosition;
        }

        public Position InitialPosition { get; set; }

        public double Magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Math.Sqrt(MagnitudeSquared);
            }
        }

        public double MagnitudeSquared
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (TerminalPosition.X - InitialPosition.X) * (TerminalPosition.X - InitialPosition.X)
                       + (TerminalPosition.Y - InitialPosition.Y) * (TerminalPosition.Y - InitialPosition.Y);
            }
        }

        public PositionVector Normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var projection = AxisProjection;
                var magnitude = Magnitude;

                var x = this.InitialPosition.X + projection.X / magnitude;
                var y = this.InitialPosition.Y + projection.Y / magnitude;

                return new PositionVector(this.InitialPosition.X, this.InitialPosition.Y, x, y);
            }
        }

        public Position AxisProjection
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                var x = Math.Abs(this.InitialPosition.X - this.TerminalPosition.X);
                var y = Math.Abs(this.InitialPosition.Y - this.TerminalPosition.Y);

                var isCollinearWithX = this.InitialPosition.X < this.TerminalPosition.X;
                var isCollinearWithY = this.InitialPosition.Y < this.TerminalPosition.Y;

                if (!isCollinearWithX)
                {
                    x *= -1;
                }

                if (!isCollinearWithY)
                {
                    y *= -1;
                }

                return new Position(x, y);
            }
        }

        public Position TerminalPosition { get; set; }

        public VectorType Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return this.InitialPosition == Entity.Position.Zero
                    ? VectorType.Standard
                    : VectorType.Component;
            }
        }

        public double DotProduct(PositionVector other)
        {
            return this.AxisProjection.X * other.AxisProjection.X + this.AxisProjection.Y * other.AxisProjection.Y;
        }

        public static PositionVector operator -(PositionVector pos)
        {
            var unit = pos.Normalized;
            return pos - unit;
        }

        public PositionVector Translate(PositionVector other)
        {
            var delta = this.InitialPosition - other.InitialPosition;

            var result = new PositionVector();

            result.InitialPosition = this.InitialPosition;
            result.TerminalPosition = other.TerminalPosition - delta;

            return result;
        }

        public static PositionVector operator -(PositionVector lhs, PositionVector rhs)
        {
            return new PositionVector
            {
                InitialPosition = rhs.TerminalPosition,
                TerminalPosition = lhs.TerminalPosition,
            };
        }

        public static PositionVector operator *(PositionVector vector, double number)
        {
            var projection = vector.AxisProjection;
            var x = projection.X * number;
            var y = projection.Y * number;

            return new PositionVector
            {
                InitialPosition = vector.InitialPosition,
                TerminalPosition = new Position(x, y),
            };
        }
        
        public static PositionVector operator *(double number, PositionVector vector)
            => vector * number;

        public static PositionVector operator *(PositionVector vector, float number)
            => vector * (double)number;

        public static PositionVector operator *(float number, PositionVector vector)
            => vector * number;

        public static PositionVector operator *(PositionVector vector, int number)
            => vector * (double)number;

        public static PositionVector operator *(int number, PositionVector vector)
            => vector * number;

        public static PositionVector operator +(PositionVector pos)
        {
            var unit = pos.Normalized;
            return pos + unit;
        }

        public static PositionVector operator +(PositionVector lhs, PositionVector rhs)
        {
            return new PositionVector
            {
                InitialPosition = lhs.InitialPosition,
                TerminalPosition = rhs.TerminalPosition
            };
        }

        public static bool operator <(PositionVector lhs, PositionVector rhs)
        {
            var lhsMagnitude = lhs.Magnitude;
            var rhsMagnitude = rhs.Magnitude;

            return lhsMagnitude < rhsMagnitude;
        }

        public static bool operator <=(PositionVector lhs, PositionVector rhs)
            => lhs < rhs || lhs == rhs;

        public static bool operator >(PositionVector lhs, PositionVector rhs)
        {
            var lhsMagnitude = lhs.Magnitude;
            var rhsMagnitude = rhs.Magnitude;

            return lhsMagnitude > rhsMagnitude;
        }

        public static bool operator >=(PositionVector lhs, PositionVector rhs)
            => lhs > rhs || lhs == rhs;

        public PositionVector Clone()
        {
            return new PositionVector(this.InitialPosition, this.TerminalPosition);
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }

        public void SetZeroLength()
        {
            this.TerminalPosition = this.InitialPosition;
        }

        public Point ToPoint()
        {
            return this.TerminalPosition.ToPoint();
        }

        public Vector2 ToVector2()
        {
            return this.TerminalPosition.ToVector2();
        }
        
        #region trigonometric

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Cos()
        {
            return this.TerminalPosition.X / Magnitude;
        }

        public double Cos(PositionVector other)
        {
            var dotProduct = this.DotProduct(other);

            return dotProduct / (this.Magnitude * other.Magnitude);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Cosec()
        {
            return Magnitude / this.TerminalPosition.X;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Ctg()
        {
            return this.TerminalPosition.X / this.TerminalPosition.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sec()
        {
            return Magnitude / this.TerminalPosition.Y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Sin()
        {
            return this.TerminalPosition.Y / Magnitude;
        }

        public double Sin(PositionVector other)
        {
            var thisMagnitude = this.Magnitude;
            var otherMagnitude = other.Magnitude;
            var thirdMagnitude = (this - other).Magnitude;

            var halfPerimeter = (thisMagnitude + otherMagnitude + thirdMagnitude) / 2;

            return (2 
                        * Math.Sqrt(halfPerimeter 
                                    * (halfPerimeter - thisMagnitude) 
                                    * (halfPerimeter - otherMagnitude) 
                                    * (halfPerimeter - thirdMagnitude))) 
                / (thisMagnitude * otherMagnitude);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public double Tg()
        {
            return this.TerminalPosition.Y / this.TerminalPosition.X;
        }

        #endregion trigonometric

        public override string ToString()
        {
            return
                $"From [ X: {this.InitialPosition.X}, Y: {this.InitialPosition.Y} ] to [ X: {this.TerminalPosition.X}, Y: {this.TerminalPosition.Y} ]";
        }

        #region Equals

        public static bool operator !=(PositionVector lhs, PositionVector rhs)
            => !(lhs == rhs);

        public static bool operator ==(PositionVector lhs, PositionVector rhs)
        {
            return lhs.CompareTo(rhs) == 0;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is PositionVector position))
            {
                return -1;
            }

            return this.CompareTo(position);
        }

        public int CompareTo(PositionVector pos)
        {
            return this.Magnitude.CompareTo(pos.Magnitude);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PositionVector position))
            {
                return false;
            }

            return this.Equals(position);
        }

        public bool Equals(PositionVector pos)
        {
            return this.CompareTo(pos) == 0;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (int)((this.InitialPosition.GetHashCode() * 397) * 7 * this.TerminalPosition.GetHashCode());
            }
        }

        #endregion Equals
    }
}