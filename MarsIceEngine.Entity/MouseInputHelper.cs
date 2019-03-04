using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SandS.Algorithm.Library.OtherNamespace;

namespace MarsIceEngine.Entity
{
    public class MouseInputHelper
    {
        private MouseState mouseState;
        private MouseState oldMouseState;
        public InputKeyPressType InputKeyPressType { get; set; }

        public MouseInputHelper()
        {
            this.mouseState = Mouse.GetState();
            this.InputKeyPressType = InputKeyPressType.OnUp;

        }


        public Position GetMousePosition()
        {
            return new Position(this.mouseState.X, this.mouseState.Y);
        }

        public bool WasMouseButtonPressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return this.WasLeftMouseButtonPressed();

                case MouseButton.Middle:
                    return this.WasMiddleMouseButtonPressed();

                case MouseButton.Right:
                    return this.WasRightMouseButtonPressed();

                default:
                    throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }

        public bool WasMouseMoved()
            => this.mouseState.X != this.oldMouseState.X ||
               this.mouseState.Y != this.oldMouseState.Y;

        public bool IsAnyMouseButtonPressed()
            => this.mouseState.LeftButton == ButtonState.Pressed ||
               this.mouseState.RightButton == ButtonState.Pressed ||
               this.mouseState.MiddleButton == ButtonState.Pressed ||
               this.mouseState.XButton1 == ButtonState.Pressed ||
               this.mouseState.XButton2 == ButtonState.Pressed;

        public bool WasMouseWheelIn()
            => this.mouseState.ScrollWheelValue - this.oldMouseState.ScrollWheelValue > 0;

        public bool WasMouseWheelOut()
            => this.mouseState.ScrollWheelValue - this.oldMouseState.ScrollWheelValue < 0;


        public void Update(GameTime gameTime)
        {
            this.oldMouseState = this.mouseState;

            this.mouseState = Mouse.GetState();
        }


        private bool WasLeftMouseButtonPressed()
        {
            switch (this.InputKeyPressType)
            {
                case InputKeyPressType.OnUp:
                    return this.oldMouseState.LeftButton == ButtonState.Pressed &&
                           this.mouseState.LeftButton == ButtonState.Released;

                case InputKeyPressType.OnDown:
                    return this.oldMouseState.LeftButton == ButtonState.Released &&
                           this.mouseState.LeftButton == ButtonState.Pressed;

                default:
                    throw new ArgumentOutOfRangeException(nameof(this.InputKeyPressType), this.InputKeyPressType, null);
            }
        }

        private bool WasMiddleMouseButtonPressed()
        {
            switch (this.InputKeyPressType)
            {
                case InputKeyPressType.OnUp:
                    return this.oldMouseState.MiddleButton == ButtonState.Pressed &&
                           this.mouseState.MiddleButton == ButtonState.Released;

                case InputKeyPressType.OnDown:
                    return this.oldMouseState.MiddleButton == ButtonState.Released &&
                           this.mouseState.MiddleButton == ButtonState.Pressed;

                default:
                    throw new ArgumentOutOfRangeException(nameof(this.InputKeyPressType), this.InputKeyPressType, null);
            }
        }

        private bool WasRightMouseButtonPressed()
        {
            switch (this.InputKeyPressType)
            {
                case InputKeyPressType.OnUp:
                    return this.oldMouseState.RightButton == ButtonState.Pressed &&
                           this.mouseState.RightButton == ButtonState.Released;

                case InputKeyPressType.OnDown:
                    return this.oldMouseState.RightButton == ButtonState.Released &&
                           this.mouseState.RightButton == ButtonState.Pressed;

                default:
                    throw new ArgumentOutOfRangeException(nameof(this.InputKeyPressType), this.InputKeyPressType, null);
            }
        }
    }
}
