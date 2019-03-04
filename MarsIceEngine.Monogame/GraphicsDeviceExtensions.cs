using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsIceEngine.Monogame
{
    public static class GraphicsDeviceExtensions
    {
        /// <summary>
        /// Generate rectangle texture
        /// </summary>
        /// <param name="device">Game graphic device</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Texture2D GenerateRectangle(this GraphicsDevice device, int width, int height, Color color)
        {
            var texture = new Texture2D(device, width, height);
            var data = new Color[width * height];

            for (var i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }

            texture.SetData(data);

            return texture;
        }

        /// <summary>
        /// Generate rectangle texture with borders
        /// </summary>
        /// <param name="device">Game graphic device</param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="textureColor"></param>
        /// <param name="borderThick"></param>
        /// <param name="borderColor"></param>
        /// <returns></returns>
        public static Texture2D GenerateRectangle(this GraphicsDevice device, int width, int height, Color textureColor, int borderThick, Color borderColor)
        {
            var texture = new Texture2D(device, width, height);

            var data = new Color[width * height];

            for (var i = 0; i < data.Length; i++)
            {
                data[i] = textureColor;
            }

            // painting vertical borders
            for (var i = 0; i < data.Length; i = i + width)
            {
                for (var j = 0; j < borderThick; j++)
                {
                    data[i + j] = borderColor;
                }

                if (i > 1)
                {
                    for (var j = 0; j < borderThick; j++)
                    {
                        data[i - 1 - j] = borderColor;
                    }
                }
            }

            // painting horizontal borders
            for (var j = 0; j < borderThick; j++)
            {
                var bias = j * width;

                for (var i = 0; i < height; i++)
                {
                    data[i + bias] = borderColor;
                    data[data.Length - i - 1 - j * width] = borderColor;
                }
            }

            //set the color
            texture.SetData(data);

            return texture;
        }
    }

}
