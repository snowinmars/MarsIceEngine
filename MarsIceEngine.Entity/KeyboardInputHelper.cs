using MarsIceEngine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace SandS.Algorithm.Library.OtherNamespace
{
    [Flags]
    public enum InputKeyPressType
    {
        OnUp = 0,
        OnDown = 1,
    }

    [Flags]
    public enum MouseButton
    {
        Left = 0,
        Middle = 1,
        Right = 2,
    }

    public class KeyboardInputHelper
    {

        private KeyboardState keyboardState;
        private KeyboardState oldKeyboardState;
        

        public KeyboardInputHelper()
        {
            this.keyboardState = Keyboard.GetState();

            this.InputKeyPressType = InputKeyPressType.OnUp;
        }



        public InputKeyPressType InputKeyPressType { get; set; }



        #region keyboard

        public bool WasKeyReleased(Keys key)
            => this.oldKeyboardState.IsKeyDown(key) &&
                this.keyboardState.IsKeyUp(key);

        public bool WasKeyPressed(Keys key)
        {
            switch (this.InputKeyPressType)
            {
                case InputKeyPressType.OnUp:
                    return this.oldKeyboardState.IsKeyDown(key) &&
                           this.keyboardState.IsKeyUp(key);

                case InputKeyPressType.OnDown:
                    return this.oldKeyboardState.IsKeyUp(key) &&
                           this.keyboardState.IsKeyDown(key);

                default:
                    throw new ArgumentOutOfRangeException(nameof(this.InputKeyPressType), this.InputKeyPressType, null);
            }
        }

        public bool IsKeyDown(Keys key)
            => this.keyboardState.IsKeyDown(key);

        public bool IsKeyUp(Keys key)
            => this.keyboardState.IsKeyUp(key);

        public bool IsShiftDown()
            => this.keyboardState.IsKeyDown(Keys.LeftShift) ||
                this.keyboardState.IsKeyDown(Keys.RightShift);

        public bool IsShiftUp()
            => this.keyboardState.IsKeyUp(Keys.LeftShift) ||
                this.keyboardState.IsKeyUp(Keys.RightShift);

        public bool WasShiftRelease()
            => (this.oldKeyboardState.IsKeyDown(Keys.LeftShift) && this.keyboardState.IsKeyUp(Keys.LeftShift)) ||
                (this.oldKeyboardState.IsKeyDown(Keys.RightShift) && this.keyboardState.IsKeyUp(Keys.RightShift));

        public bool IsCtrlDown()
            => this.keyboardState.IsKeyDown(Keys.LeftControl) ||
                this.keyboardState.IsKeyDown(Keys.RightControl);

        public bool IsCtrlUp()
            => this.keyboardState.IsKeyUp(Keys.LeftControl) ||
                this.keyboardState.IsKeyUp(Keys.RightControl);

        public bool WasCtrlRelease()
            => (this.oldKeyboardState.IsKeyDown(Keys.LeftControl) && this.keyboardState.IsKeyUp(Keys.LeftControl)) ||
                (this.oldKeyboardState.IsKeyDown(Keys.RightControl) && this.keyboardState.IsKeyUp(Keys.RightControl));

        public bool IsAltDown()
            => this.keyboardState.IsKeyDown(Keys.LeftAlt) ||
                this.keyboardState.IsKeyDown(Keys.RightAlt);

        public bool IsAltUp()
            => this.keyboardState.IsKeyUp(Keys.LeftAlt) ||
                this.keyboardState.IsKeyUp(Keys.RightAlt);

        public bool WasAltRelease()
            => (this.oldKeyboardState.IsKeyDown(Keys.LeftAlt) && this.keyboardState.IsKeyUp(Keys.LeftAlt)) ||
                (this.oldKeyboardState.IsKeyDown(Keys.RightAlt) && this.keyboardState.IsKeyUp(Keys.RightAlt));

        public bool WasAnyKeyPressed()
            => this.keyboardState.GetPressedKeys().Length > 0;

        public bool WasAnyKeyFreshPressed()
            => this.oldKeyboardState.GetPressedKeys().Length == 0 &&
                this.keyboardState.GetPressedKeys().Length > 0;

        #endregion keyboard

        public void Update(GameTime gameTime)
        {
            this.oldKeyboardState = this.keyboardState;

            this.keyboardState = Keyboard.GetState();
        }


    }
}
