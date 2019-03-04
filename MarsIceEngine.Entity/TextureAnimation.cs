using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MarsIceEngine.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarsIceEngine.Entity
{
    public class TextureAnimation
    {
        private readonly IDictionary<Act, Texture2D> spriteList;
        private readonly int frameCountX;
        private int currentFrame;
        internal bool AnimationIsDone => currentAction != Act.Nop;

        internal Act Act { get; set; }

        private Act currentAction;

        public TextureAnimation(Texture2D sprite, int frameCountX) : this(
            new Dictionary<Act, Texture2D> {{Act.Nop, sprite}}, frameCountX)
        {
        }

        public TextureAnimation(IDictionary<Act, Texture2D> spriteList, int frameCountX)
        {
            this.spriteList = spriteList;
            this.frameCountX = frameCountX;

            currentFrame = 0;
        }

        private Vector2 CalculatePosition(Vector2 objectPosition, int spriteWidth)
        {
            var result = objectPosition;

            result -= new Vector2(spriteWidth, 0);

            return result;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 objectPosition)
        {
            if (currentAction != Act && Act != Act.Nop)
            {
                currentAction = Act;
                currentFrame = 0;
            }

            var sprite = spriteList[currentAction];

            var spriteWidth = sprite.Width / frameCountX;

            var window = new Rectangle(currentFrame * spriteWidth, 0, (int)Constants.WorldCellWidth, (int)Constants.WorldCellHeight);

            spriteBatch.Draw(
                sprite, 
                CalculatePosition(objectPosition, spriteWidth),
                window,
                Color.White);
        }

        private TimeSpan pr;

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime - pr > TimeSpan.FromMilliseconds(100))
            {
                pr = gameTime.TotalGameTime;
                currentFrame++;
            }

            if (currentFrame >= frameCountX)
            {
                currentFrame = 0;
                currentAction = Act.Nop;
            }
        }
    }
}
