using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MarsIceEngine.Entity
{
    public class Player
    {
        public Vector2 Speed { get; set; }

        public Vector2 Acceleration { get; set; }

        public Vector2 PositionRadiusVector { get; set; }

        public Point Position => PositionRadiusVector.ToPoint();
    }
}
