using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SandS.Algorithm.Library.OtherNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsIceEngine.Entity
{
    public enum Act
    {
        Nop = 0,

        Back,

        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRigth,
    }

    public class InputHelper
    {
        private static IDictionary<Keys, Act> actions { get; } = new Dictionary<Keys, Act>
        {
            { Keys.Escape, Act.Back },
            { Keys.E, Act.MoveUp},
            { Keys.D, Act.MoveDown },
            { Keys.S, Act.MoveLeft},
            { Keys.F, Act.MoveRigth },
        };

        public InputHelper(KeyboardInputHelper keyboardInputHelper)
        {
            KeyboardInputHelper = keyboardInputHelper;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardInputHelper.Update(gameTime);
        }

        private KeyboardInputHelper KeyboardInputHelper { get; }

        public bool WasActionHappened(Act act)
        {
            foreach (var pair in actions)
            {
                if (KeyboardInputHelper.WasKeyPressed(pair.Key) && pair.Value == act)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
