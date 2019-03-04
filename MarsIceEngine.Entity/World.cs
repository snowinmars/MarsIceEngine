using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MarsIceEngine.Entity
{
    public class World
    {
        public World(WorldCell[,] cells)
        {
            Cells = cells;
        }

        public WorldCell[,] Cells { get; set; }

        public int Width => Cells.GetLength(0);

        public int Height => Cells.GetLength(1);

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var cell in Cells)
            {
                cell.Draw(spriteBatch);
            }
        }
    }
}
