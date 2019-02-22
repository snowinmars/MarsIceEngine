using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarsIceEngine.Entity
{
    public static class Extensions
    {
        public static double RelativeAngle(this Vector2 thisVector, Vector2 otherVector)
        {
            var sin = thisVector.X * otherVector.Y 
                         - otherVector.X * thisVector.Y;
            var cos = thisVector.X * otherVector.X 
                         + thisVector.Y * otherVector.Y;

            return Math.Atan2(sin, cos) * (180 / Math.PI);
        }

        public static float Sin(this Vector2 vector)
        {
            return vector.Y / vector.LengthSquared();
        }

        public static float Cos(this Vector2 vector)
        {
            return vector.X / vector.LengthSquared();
        }

        public static float Tg(this Vector2 vector)
        {
            return vector.X / vector.Y;
        }

        public static float Ctg(this Vector2 vector)
        {
            return vector.Y / vector.X;
        }

    }
}
