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
        private readonly IDictionary<TextureAnimationInfo, Texture2D> spriteList;
        private int currentFrame;

        public delegate void OnAnimationDoneDelegate();

        public event OnAnimationDoneDelegate OnAnimationDone;

        internal Act Act { get; set; }

        private Act currentAction;

        public TextureAnimation(Texture2D sprite) : this(
            new Dictionary<TextureAnimationInfo, Texture2D> {{new TextureAnimationInfo(Act.Nop, 1), sprite}})
        {
        }

        public TextureAnimation(IDictionary<TextureAnimationInfo, Texture2D> spriteList)
        {
            this.spriteList = spriteList;

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
            var (info, sprite) = spriteList.First(x => x.Key.Act == currentAction);
            var isAnimationDone = currentFrame >= info.FrameCount;

            if (isAnimationDone)
            {
                currentFrame = 0;
                currentAction = Act.Nop;
                OnAnimationDone?.Invoke();
            }
            
            var spriteWidth = sprite.Width / info.FrameCount;

            var currentWindowX = currentFrame * spriteWidth;
            var currentWindowWidth = (int)Constants.WorldCellWidth * (isAnimationDone? 1 : 2);
            var window = new Rectangle(currentWindowX, 0, currentWindowWidth, (int)Constants.WorldCellHeight);

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

            if (currentAction != Act && Act != Act.Nop)
            {
                currentAction = Act;
                currentFrame = 0;
            }
        }
    }
}
