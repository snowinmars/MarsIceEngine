using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MarsIceEngine.Entity.Tests
{
    public class PositionTests
    {
        [Test]
        public void DefaultCtor()
        {
            var p = new Position();

            Assert.Zero(p.X);
            Assert.Zero(p.Y);
        }

        [Theory]

        [TestCase(-1, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]

        [TestCase(0, 0)]

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]

        [TestCase(20, 40)]
        [TestCase(0, 11)]
        [TestCase(13, 0)]
        [TestCase(15, 17)]
        public void IntegerCtor(int x, int y)
        {
            var p = new Position(x, y);

            Assert.AreEqual(x, p.X);
            Assert.AreEqual(y, p.Y);
        }

        [Theory]

        [TestCase(float.NaN, 17)]
        [TestCase(27, float.NaN)]
        [TestCase(float.NaN, float.NaN)]

        [TestCase(float.PositiveInfinity, 17)]
        [TestCase(27, float.PositiveInfinity)]
        [TestCase(float.PositiveInfinity, float.PositiveInfinity)]

        [TestCase(float.NegativeInfinity, 17)]
        [TestCase(27, float.NegativeInfinity)]
        [TestCase(float.NegativeInfinity, float.NegativeInfinity)]
        public void FloatCtor(float x, float y)
        {
            var p = new Position(x, y);

            var expectedX = Position.Normalize(x);
            var expectedY = Position.Normalize(y);

            Assert.AreEqual(expectedX, p.X);
            Assert.AreEqual(expectedY, p.Y);
        }

        [Theory]

        [TestCase(double.NaN, 17)]
        [TestCase(27, double.NaN)]
        [TestCase(double.NaN, double.NaN)]

        [TestCase(double.PositiveInfinity, 17)]
        [TestCase(27, double.PositiveInfinity)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity)]

        [TestCase(double.NegativeInfinity, 17)]
        [TestCase(27, double.NegativeInfinity)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity)]
        public void DoubleCtor(double x, double y)
        {
            var p = new Position(x, y);

            var expectedX = Position.Normalize(x);
            var expectedY = Position.Normalize(y);

            Assert.AreEqual(expectedX, p.X);
            Assert.AreEqual(expectedY, p.Y);
        }

        [Test]
        public void PositionZero_ShouldBeZero()
        {
            var p = Position.Zero;

            Assert.Zero(p.X);
            Assert.Zero(p.Y);
        }

        [Theory]

        [TestCase(-1, -1, 1, 1, 0, 0)]

        [TestCase(0, 0, 0, 0, 0, 0)]

        [TestCase(10, 17, 0, 0, 10, 17)]
        [TestCase(10, 27, 503, 11, 513, 38)]
        [TestCase(10, 27, -53, -100, -43, -73)]
        [TestCase(-23, -7, 5, 20, -18, 13)]
        [TestCase(-23, -7, -7, -20, -30, -27)]
        public void Sum(int lhsX, int lhsY, int rhsX, int rhsY, int resultX, int resultY)
        {
            var lhs = new Position(lhsX, lhsY);
            var rhs = new Position(rhsX, rhsY);
            var result = new Position(resultX, resultY);
            var sum = lhs + rhs;

            Assert.AreEqual(result, sum);
        }

        [Theory]

        [TestCase(-1, -1, 1, 1, -2, -2)]

        [TestCase(0, 0, 0, 0, 0, 0)]

        [TestCase(10, 17, 0, 0, 10, 17)]
        [TestCase(10, 27, 503, 11, -493, 16)]
        [TestCase(10, 27, -53, -100, 63, 127)]
        [TestCase(-23, -7, 5, 20, -28, -27)]
        [TestCase(-23, -7, -7, -20, -16, 13)]
        public void Subtraction(int lhsX, int lhsY, int rhsX, int rhsY, int resultX, int resultY)
        {
            var lhs = new Position(lhsX, lhsY);
            var rhs = new Position(rhsX, rhsY);
            var result = new Position(resultX, resultY);
            var sum = lhs - rhs;

            Assert.AreEqual(result, sum);
        }

        [Theory]

        [TestCase(-1, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]

        [TestCase(0, 0)]

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]

        [TestCase(10, 27)]
        [TestCase(-10, 27)]
        [TestCase(10, -27)]
        [TestCase(-10, -27)]
        public void UnarySings(int x, int y)
        {
            var position = new Position(x, y);
            var positivePosition = +position;
            var negativePosition = -position;

            Assert.AreEqual(position.X, positivePosition.X);
            Assert.AreEqual(position.Y, positivePosition.Y);

            Assert.AreEqual(-position.X, negativePosition.X);
            Assert.AreEqual(-position.Y, negativePosition.Y);
        }

        [Theory]

        [TestCase(-1, -1, 4)]
        [TestCase(-1, 0, 6)]
        [TestCase(0, -1, -1)]

        [TestCase(0, 0, 5)]

        [TestCase(0, 1, 6)]
        [TestCase(1, 0, -7)]
        [TestCase(1, 1, 12)]

        [TestCase(10, 27, -9)]
        [TestCase(-10, 27, 0)]
        [TestCase(10, -27, 55)]
        [TestCase(-10, -27, 2)]
        public void MultiplyByNumber(int x, int y, int number)
        {
            var position = new Position(x, y);
            var resultPosition = position * number;

            Assert.AreEqual(position.X * number, resultPosition.X);
            Assert.AreEqual(position.Y * number, resultPosition.Y);
        }

        [Theory]

        [TestCase(0, 10)]
        [TestCase(10, 10)]
        [TestCase(10, 0)]
        [TestCase(10, -10)]

        [TestCase(0, 0)]

        [TestCase(0, -10)]
        [TestCase(-10, -10)]
        [TestCase(-10, 0)]
        [TestCase(-10, 10)]
        public void IsGreaterByX(int lhsX, int lhsY)
        {
            const int diff = 7;

            var rhsCoordinates = new []
            {
                new { rhsX = lhsX, rhsY = lhsY },

                new { rhsX = lhsX, rhsY = lhsY + diff },
                new { rhsX = lhsX + diff, rhsY = lhsY + diff },

                new { rhsX = lhsX + diff, rhsY = lhsY },
                new { rhsX = lhsX + diff, rhsY = lhsY - diff },

                new { rhsX = lhsX, rhsY = lhsY - diff },
                new { rhsX = lhsX - diff, rhsY = lhsY - diff },

                new { rhsX = lhsX - diff, rhsY = lhsY },
                new { rhsX = lhsX - diff, rhsY = lhsY + diff },
            };

            foreach (var rhsCoordinate in rhsCoordinates)
            {
                var compare = new
                {
                    IsGreaterByX = lhsX > rhsCoordinate.rhsX,
                    IsGreaterByY = lhsY > rhsCoordinate.rhsY,
                    IsLesserByX = lhsX < rhsCoordinate.rhsX,
                    IsLesserByY = lhsY < rhsCoordinate.rhsY,

                    IsGreaterOrEqualByX = lhsX >= rhsCoordinate.rhsX,
                    IsGreaterOrEqualByY = lhsY >= rhsCoordinate.rhsY,
                    IsLesserOrEqualByX = lhsX <= rhsCoordinate.rhsX,
                    IsLesserOrEqualByY = lhsY <= rhsCoordinate.rhsY,

                    AreEuqalByX = lhsX == rhsCoordinate.rhsX,
                    AreEuqalByY = lhsY == rhsCoordinate.rhsY,
                };

                var lhs = new Position(lhsX, lhsY);
                var rhs = new Position(rhsCoordinate.rhsX, rhsCoordinate.rhsY);

                Assert.AreEqual(compare.IsGreaterByX, lhs.IsGreaterByX(rhs));
                Assert.AreEqual(compare.IsGreaterByY, lhs.IsGreaterByY(rhs));
                Assert.AreEqual(compare.IsLesserByX, lhs.IsLesserByX(rhs));
                Assert.AreEqual(compare.IsLesserByY, lhs.IsLesserByY(rhs));

                Assert.AreEqual(compare.IsGreaterOrEqualByX, lhs.IsGreaterOrEqualByX(rhs));
                Assert.AreEqual(compare.IsGreaterOrEqualByY, lhs.IsGreaterOrEqualByY(rhs));
                Assert.AreEqual(compare.IsLesserOrEqualByX, lhs.IsLesserOrEqualByX(rhs));
                Assert.AreEqual(compare.IsLesserOrEqualByY, lhs.IsLesserOrEqualByY(rhs));

                Assert.AreEqual(compare.AreEuqalByX, lhs.AreEqualsByX(rhs));
                Assert.AreEqual(compare.AreEuqalByY, lhs.AreEqualsByY(rhs));
            }
        }
    }
}
