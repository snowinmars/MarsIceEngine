using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsIceEngine.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarsIceEngine.Entity
{
    public class WorldCell
    {
        public WorldCell(Position center, Color borderColor = default)
        {
            BorderColor = borderColor == default ? Color.Black : borderColor;
            Center = center;
        }

        public Position Center { get; set; }

        public static readonly Vector2 Size;
        public static readonly Vector2 HalfSize;

        static WorldCell()
        {
            Size = new Vector2(Constants.WorldCellWidth, Constants.WorldCellHeight);
            HalfSize = Size / 2;
        }

        public Color BorderColor { get; set; }
        public TextureAnimation Texture { get; set; }
        public Vector2 PositionRadiusVector => Center.ToVector2();

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture.Draw(spriteBatch, PositionRadiusVector);
        }
    }
}
