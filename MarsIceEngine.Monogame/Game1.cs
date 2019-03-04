using MarsIceEngine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SandS.Algorithm.Library.OtherNamespace;

namespace MarsIceEngine.Monogame
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private readonly InputHelper inputHelper;
        private readonly KeyboardInputHelper keyboardInputHelper;
        private SpriteBatch spriteBatch;
        Player Player { get;  }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            keyboardInputHelper = new KeyboardInputHelper();
            inputHelper = new InputHelper(keyboardInputHelper);
            Player = new Player();
            Content.RootDirectory = "Content";
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(Player.Texture, Player.PositionRadiusVector, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.Texture = spriteBatch.GraphicsDevice.Generate(10, 15, Color.Bisque);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update(gameTime);

            if (inputHelper.WasActionHappened(Act.Back))
            {
                Exit();
            }

            // inputs
            const float multiplier = 0.1f;
            var delta = Vector2.Zero;
            if (inputHelper.WasActionHappened(Act.MoveUp))
            {
                delta -= Vector2.UnitY * multiplier;
            }

            if (inputHelper.WasActionHappened(Act.MoveDown))
            {
                delta += Vector2.UnitY * multiplier;
            }

            if (inputHelper.WasActionHappened(Act.MoveRigth))
            {
                delta += Vector2.UnitX * multiplier;
            }

            if (inputHelper.WasActionHappened(Act.MoveLeft))
            {
                delta -= Vector2.UnitX * multiplier;
            }

            Player.Acceleration += delta;

            // inertion
            const float inertion = 1f;
            var acc = Player.Acceleration;
            if (Player.Acceleration.Length() > float.Epsilon)
            {
                Player.Acceleration -= acc * inertion;
            }

            if (Player.Acceleration.Length() < float.Epsilon)
            {
                Player.Acceleration += acc * inertion;
            }

            // recalc

            Player.Speed += Player.Acceleration;
            Player.PositionRadiusVector += Player.Speed;

            base.Update(gameTime);
        }
    }
}