using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsIceEngine.Common;
using NUnit.Framework;

namespace MarsIceEngine.Entity.Tests
{
    public class PositionVectorTests
    {
        [Test]
        public void DefaultCtor()
        {
            var vector = new PositionVector();

            Assert.Zero(vector.Magnitude, nameof(vector.Magnitude));
            Assert.Zero(vector.MagnitudeSquared, nameof(vector.MagnitudeSquared));

            Assert.AreEqual(Position.Zero, vector.InitialPosition, nameof(vector.InitialPosition));
            Assert.AreEqual(Position.Zero, vector.TerminalPosition, nameof(vector.TerminalPosition));
            Assert.AreEqual(Position.Zero, vector.AxisProjection, nameof(vector.AxisProjection));

            Assert.AreEqual(PositionVector.Zero, vector.Normalized, nameof(vector.Normalized));

            Assert.AreEqual(VectorType.Standard, vector.Type, nameof(vector.Type));
        }

        [Theory]

        [TestCase(0, 3, 4, 0, 5)]
        [TestCase(0, -3, 4, 0, 5)]
        [TestCase(0, 3, -4, 0, 5)]
        [TestCase(0, -3, -4, 0, 5)]

        [TestCase(7, 3, 4, 7, 5)]

        [TestCase(7, 7, 4, 7, 3)]

        [TestCase(7, 8, 4, 7, 3.1622776601683795 /* Math.Sqrt(10) */)]
        [TestCase(-2, -3, -20, -7, 18.439088914585774 /* Math.Sqrt(340) */)]
        public void Magnitude(double initialX, double initialY, double terminalX, double terminalY, double expectedMagnitude)
        {
            var vector = new PositionVector(initialX, initialY, terminalX, terminalY);

            Assert.IsTrue(expectedMagnitude - vector.Magnitude < Constants.SmallDouble, nameof(vector.Magnitude));
            Assert.IsTrue(expectedMagnitude * expectedMagnitude - vector.MagnitudeSquared < Constants.SmallDouble, nameof(vector.MagnitudeSquared));
        }

        [Theory]

        [TestCase(0, 0, 0, 0)]

        [TestCase(-4, 3, 0, 0)]
        [TestCase(0, 0, 4, 7)]

        [TestCase(0, 3, 4, 0)]
        [TestCase(0, -3, 4, 0)]
        [TestCase(0, 3, -4, 0)]
        [TestCase(0, -3, -4, 0)]

        [TestCase(7, 3, 4, 7)]

        [TestCase(7, 7, 4, 7)]

        [TestCase(7, 8, 4, 7)]
        [TestCase(-2, -3, -20, -7)]
        public void InitialTerminalPositions(double initialX, double initialY, double terminalX, double terminalY)
        {
            var vector = new PositionVector(initialX, initialY, terminalX, terminalY);

            var initialPosition = new Position(initialX, initialY);
            var terminalPosition = new Position(terminalX, terminalY);

            Assert.AreEqual(initialPosition, vector.InitialPosition, nameof(vector.InitialPosition));
            Assert.AreEqual(terminalPosition, vector.TerminalPosition, nameof(vector.TerminalPosition));

            var vectorType = initialPosition == Position.Zero ? VectorType.Standard : VectorType.Component;

            Assert.AreEqual(vectorType, vector.Type, nameof(vector.Type));
        }

        [Test]
        public void Zero()
        {
            var vector = PositionVector.Zero;

            Assert.Zero(vector.Magnitude, nameof(vector.Magnitude));
            Assert.Zero(vector.MagnitudeSquared, nameof(vector.MagnitudeSquared));

            Assert.AreEqual(Position.Zero, vector.InitialPosition, nameof(vector.InitialPosition));
            Assert.AreEqual(Position.Zero, vector.TerminalPosition, nameof(vector.TerminalPosition));
            Assert.AreEqual(Position.Zero, vector.AxisProjection, nameof(vector.AxisProjection));

            Assert.AreEqual(PositionVector.Zero, vector.Normalized, nameof(vector.Normalized));

            Assert.AreEqual(VectorType.Standard, vector.Type, nameof(vector.Type));
        }

        [Test]
        public void VerticalUnit()
        {
            var vector = PositionVector.VerticalUnit;

            Assert.AreEqual(1, vector.Magnitude, nameof(vector.Magnitude));
            Assert.AreEqual(1, vector.MagnitudeSquared, nameof(vector.MagnitudeSquared));

            Assert.AreEqual(Position.Zero, vector.InitialPosition, nameof(vector.InitialPosition));

            var verticalUnit = new Position(0, 1);
            Assert.AreEqual(verticalUnit, vector.TerminalPosition, nameof(vector.TerminalPosition));
            Assert.AreEqual(verticalUnit, vector.AxisProjection, nameof(vector.AxisProjection));

            Assert.AreEqual(new PositionVector(Position.Zero, verticalUnit), vector.Normalized, nameof(vector.Normalized));

            Assert.AreEqual(VectorType.Standard, vector.Type, nameof(vector.Type));
        }

        [Test]
        public void HorizontalUnit()
        {
            var vector = PositionVector.HorizontalUnit;

            Assert.AreEqual(1, vector.Magnitude, nameof(vector.Magnitude));
            Assert.AreEqual(1, vector.MagnitudeSquared, nameof(vector.MagnitudeSquared));

            Assert.AreEqual(Position.Zero, vector.InitialPosition, nameof(vector.InitialPosition));

            var horizontalUnit = new Position(1, 0);
            Assert.AreEqual(horizontalUnit, vector.TerminalPosition, nameof(vector.TerminalPosition));
            Assert.AreEqual(horizontalUnit, vector.AxisProjection, nameof(vector.AxisProjection));

            Assert.AreEqual(new PositionVector(horizontalUnit, Position.Zero), vector.Normalized, nameof(vector.Normalized));

            Assert.AreEqual(VectorType.Standard, vector.Type, nameof(vector.Type));
        }

        [Theory]

        [TestCase(0, 0, 0, 0, 
            0, 0)]

        [TestCase(1, 0, 0, 0, 
            -1, 0)]
        [TestCase(0, 0, 1, 0, 
            1, 0)]

        [TestCase(0, 1, 0, 0, 
            0, -1)]
        [TestCase(0, 0, 0, 1,
            0, 1)]

        [TestCase(11, 13, 17, 19, 
            6, 6)]
        [TestCase(17, 19, 11, 13, 
            -6, -6)]
        public void AxisProjection(double initialX, double initialY, double terminalX, double terminalY, double expectedProjectionX, double expectedProjectionY)
        {
            var vector = new PositionVector(initialX, initialY, terminalX, terminalY);
            var expectedProjection = new Position(expectedProjectionX, expectedProjectionY);

            Assert.AreEqual(expectedProjection, vector.AxisProjection);
        }

        [Theory]

        [TestCase(0, 0, 0, 0,
                    0, 0, 0, 0,
                    0)]

        [TestCase(0, 10, 0, 0, 
                    0, 0, 11, 0,
                    0)]

        [TestCase(0, 0, 7, 10,
                    0, 0, 11, 6,
                    137)]

        [TestCase(0, 0, -11, 10,
                    0, 0, -21, -7,
                    161)]

        [TestCase(0, 0, 12, 13,
                    0, 0, -23, -7,
                    -367)]
        public void DotProduct(double leftInitialX, double leftInitialY, double leftTerminalX, double leftTerminalY,
            double rightInitialX, double rightInitialY, double rightTerminalX, double rightTerminalY, 
            double expectedDotProduct)
        {
            var lhs = new PositionVector(leftInitialX, leftInitialY, leftTerminalX, leftTerminalY);
            var rhs = new PositionVector(rightInitialX, rightInitialY, rightTerminalX, rightTerminalY);

            var result = lhs.DotProduct(rhs);
            Assert.AreEqual(expectedDotProduct, result);
        }

        [Theory]

        [TestCase(0, 0, 0, 0)]

        [TestCase(1, 0, 0, 0)]
        [TestCase(0, 0, 1, 0)]

        [TestCase(0, 1, 0, 0)]
        [TestCase(0, 0, 0, 1)]

        [TestCase(11, 13, 17, 19)]

        [TestCase(17, 19, 11, 13)]
        public void Normalized(double initialX, double initialY, double terminalX, double terminalY)
        {
            var vector = new PositionVector(initialX, initialY, terminalX, terminalY);

            var normalizedVector = vector.Normalized;

            if (vector.Magnitude > Constants.SmallDouble)
            {
                Assert.IsTrue(1 - normalizedVector.Magnitude < Constants.SmallDouble, nameof(normalizedVector.Magnitude));
                Assert.IsTrue(1 - vector.Cos(normalizedVector) < Constants.SmallDouble, nameof(vector.Cos));
            }
            else
            {
                Assert.IsTrue(normalizedVector.Magnitude < Constants.SmallDouble, nameof(normalizedVector.Magnitude));
            }
        }
    }
}
