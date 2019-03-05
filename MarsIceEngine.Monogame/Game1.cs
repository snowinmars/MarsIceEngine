using System.Collections.Generic;
using MarsIceEngine.Common;
using MarsIceEngine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SandS.Algorithm.Library.OtherNamespace;

namespace MarsIceEngine.Monogame
{
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager graphicsDeviceManager;
        private readonly InputHelper inputHelper;
        private readonly KeyboardInputHelper keyboardInputHelper;
        private readonly MouseInputHelper mouseInputHelper;
        private SpriteBatch spriteBatch;
        private readonly Player player;
        private readonly World world;
        public readonly Camera Camera;

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                IsFullScreen = true,
                HardwareModeSwitch = false
            };

            IsMouseVisible = false;
            Window.AllowUserResizing = false;

            keyboardInputHelper = new KeyboardInputHelper();
            mouseInputHelper = new MouseInputHelper();
            inputHelper = new InputHelper(keyboardInputHelper, mouseInputHelper);

            Content.RootDirectory = "Content";
            Camera = new Camera();

            player = new Player();

            var cells = new WorldCell[Constants.WorldWidth, Constants.WorldHeight];

            for (var x = 0; x < Constants.WorldWidth; x++)
            {
                for (var y = 0; y < Constants.WorldHeight; y++)
                {
                    var cellCenterPosition = new Position(Constants.WorldCellWidth * x, Constants.WorldCellHeight * y);
                    var cell = new WorldCell(cellCenterPosition);
                    cells[x, y] = cell;
                }
            }

            world = new World(cells);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.BackToFront,
                BlendState.AlphaBlend,
                null,
                null,
                null,
                null,
                Camera.TranslationMatrix);

            world.Draw(spriteBatch);
            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        protected override void Initialize()
        {
            Camera.ViewportWidth = graphicsDeviceManager.GraphicsDevice.Viewport.Width;
            Camera.ViewportHeight = graphicsDeviceManager.GraphicsDevice.Viewport.Height;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            var playerTextures = new Dictionary<TextureAnimationInfo, Texture2D>
            {
                {
                    new TextureAnimationInfo
                    {
                        Act = Act.Nop,
                        FrameCount = 10,
                    },
                    Content.Load<Texture2D>("player-nop")
                },
                {
                    new TextureAnimationInfo
                    {
                        Act = Act.MoveRight,
                        FrameCount = 9,
                    },
                    Content.Load<Texture2D>("player-moveright")
                },
            };

            player.Texture = new TextureAnimation(playerTextures);

            foreach (var cell in world.Cells)
            {
                cell.Texture = new TextureAnimation(Content.Load<Texture2D>("cell-nop"));
            }

            Camera.CenterOn(player.PositionRadiusVector);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            inputHelper.Update(gameTime);

            player.Update(gameTime, inputHelper);
            
            Camera.Update(inputHelper);
            Camera.CenterOn(player.PositionRadiusVector);

            if (inputHelper.WasActionHappened(Act.Back))
            {
                Exit();
            }

            base.Update(gameTime);
        }
    }
}