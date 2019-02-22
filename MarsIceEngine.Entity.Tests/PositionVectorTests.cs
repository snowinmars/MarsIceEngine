using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MarsIceEngine.Entity.Tests
{
    public class PositionVectorTests
    {
        [Test]
        public void DefaultCtor()
        {
            var vector = new PositionVector();

            Assert.Zero(vector.Magnitude, "Magnitude should be zero");
            Assert.Zero(vector.MagnitudeSquared, "Magnitude square should be zero");

            Assert.AreEqual(Position.Zero, vector.InitialPosition, "Initial position should be zero");
            Assert.AreEqual(Position.Zero, vector.TerminalPosition, "Terminal position should be zero");
            Assert.AreEqual(Position.Zero, vector.Projection, "Projection should be zero");

            Assert.AreEqual(PositionVector.Zero, vector.Normalized, "Normalized vector should be zero");

            Assert.AreEqual(VectorType.Standard, vector.Type, "Type should be standard");}
    }
}
