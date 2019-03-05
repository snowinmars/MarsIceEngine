using System.Collections.Generic;
using MarsIceEngine.Common;
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

        public TextureAnimation Texture { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            Texture.Draw(spriteBatch, PositionRadiusVector);
        }

        public void Update(GameTime gameTime, InputHelper inputHelper)
        {
            var act = Act.Nop;

            var delta = new Vector2();

            if (inputHelper.WasActionHappened(Act.MoveRight))
            {
                act = Act.MoveRight;
                delta += Vector2.UnitX * Constants.WorldCellWidth;
            }

            if (inputHelper.WasActionHappened(Act.MoveLeft))
            {
                act = Act.MoveLeft;
                delta -= Vector2.UnitX * Constants.WorldCellWidth;
            }

            if (inputHelper.WasActionHappened(Act.MoveUp))
            {
                act = Act.MoveUp;
                delta -= Vector2.UnitY * Constants.WorldCellWidth;
            }

            if (inputHelper.WasActionHappened(Act.MoveDown))
            {
                act = Act.MoveDown;
                delta += Vector2.UnitY * Constants.WorldCellWidth;
            }

            Texture.Act = act;
            Texture.Update(gameTime);
            Texture.OnAnimationDone += () => PositionRadiusVector += delta;
        }
    }
}
