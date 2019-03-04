using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarsIceEngine.Entity
{
    public class Player
    {
        public Player()
        {
            Speed = new Vector2();
            Acceleration = new Vector2();
            PositionRadiusVector = new Vector2();
        }

        public Vector2 Speed { get; set; }

        public Vector2 Acceleration { get; set; }

        public Vector2 PositionRadiusVector { get; set; }

        public Texture2D Texture { get; set; }

        public Point Position => PositionRadiusVector.ToPoint();
    }
}
