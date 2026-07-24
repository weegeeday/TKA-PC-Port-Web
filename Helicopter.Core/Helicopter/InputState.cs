using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Helicopter.Core
{
	public class InputState
	{
		private GamePadState prevInput;

		private GamePadState currInput;

		private KeyboardState prevKeyInput;

		private KeyboardState currKeyInput;

        private TouchCollection prevTouches;

        private TouchCollection currTouches;

        private MouseState prevMouseState;

        private MouseState currMouseState;

        public void Update()
		{
			this.currInput = GamePad.GetState(Global.playerIndex ?? Microsoft.Xna.Framework.PlayerIndex.One);
			this.currKeyInput = Keyboard.GetState();
            this.currTouches = TouchPanel.GetState();
            if (Game1.IsWeb)
            {
                this.currMouseState = Mouse.GetState();
            }
        }

		public void EndUpdate()
		{
			this.prevInput = this.currInput;
			this.prevKeyInput = this.currKeyInput;
            this.prevTouches = this.currTouches;
            if (Game1.IsWeb)
            {
                this.prevMouseState = this.currMouseState;
            }
        }

        public bool IsThingTouched()
        {
            bool touchPressed = this.currTouches.Count > 0 && this.prevTouches.Count == 0;
            bool mousePressed = Game1.IsWeb
                && this.currMouseState.LeftButton == ButtonState.Pressed
                && this.prevMouseState.LeftButton == ButtonState.Released;
            return touchPressed || mousePressed;
        }

		public bool IsButtonPressed(Buttons button)
		{
			bool flag = this.currInput.IsButtonDown(button) && this.prevInput.IsButtonUp(button);
			bool flag2 = false;
			switch (button)
			{
			case Buttons.A:
				flag2 = (this.currKeyInput.IsKeyDown(Keys.Space) && this.prevKeyInput.IsKeyUp(Keys.Space))
                     || (this.currKeyInput.IsKeyDown(Keys.Enter) && this.prevKeyInput.IsKeyUp(Keys.Enter))
                     || (this.currKeyInput.IsKeyDown(Keys.Z) && this.prevKeyInput.IsKeyUp(Keys.Z))
                     || (this.currKeyInput.IsKeyDown(Keys.J) && this.prevKeyInput.IsKeyUp(Keys.J))
                     || (Game1.IsWeb && this.currMouseState.LeftButton == ButtonState.Pressed && this.prevMouseState.LeftButton == ButtonState.Released);
				break;
			case Buttons.B:
				flag2 = (this.currKeyInput.IsKeyDown(Keys.Escape) && this.prevKeyInput.IsKeyUp(Keys.Escape))
                     || (this.currKeyInput.IsKeyDown(Keys.Back) && this.prevKeyInput.IsKeyUp(Keys.Back))
                     || (this.currKeyInput.IsKeyDown(Keys.B) && this.prevKeyInput.IsKeyUp(Keys.B))
                     || (this.currKeyInput.IsKeyDown(Keys.X) && this.prevKeyInput.IsKeyUp(Keys.X))
                     || (this.currKeyInput.IsKeyDown(Keys.K) && this.prevKeyInput.IsKeyUp(Keys.K));
				break;
			case Buttons.DPadLeft:
				flag = flag || (this.currInput.ThumbSticks.Left.X < -0.5f && this.prevInput.ThumbSticks.Left.X >= -0.5f);
				flag2 = (this.currKeyInput.IsKeyDown(Keys.Left) && this.prevKeyInput.IsKeyUp(Keys.Left))
                     || (this.currKeyInput.IsKeyDown(Keys.A) && this.prevKeyInput.IsKeyUp(Keys.A));
				break;
			case Buttons.DPadRight:
				flag = flag || (this.currInput.ThumbSticks.Left.X > 0.5f && this.prevInput.ThumbSticks.Left.X <= 0.5f);
				flag2 = (this.currKeyInput.IsKeyDown(Keys.Right) && this.prevKeyInput.IsKeyUp(Keys.Right))
                     || (this.currKeyInput.IsKeyDown(Keys.D) && this.prevKeyInput.IsKeyUp(Keys.D));
				break;
			case Buttons.DPadUp:
				flag = flag || (this.currInput.ThumbSticks.Left.Y > 0.5f && this.prevInput.ThumbSticks.Left.Y <= 0.5f);
				flag2 = (this.currKeyInput.IsKeyDown(Keys.Up) && this.prevKeyInput.IsKeyUp(Keys.Up))
                     || (this.currKeyInput.IsKeyDown(Keys.W) && this.prevKeyInput.IsKeyUp(Keys.W));
				break;
			case Buttons.DPadDown:
				flag = flag || (this.currInput.ThumbSticks.Left.Y < -0.5f && this.prevInput.ThumbSticks.Left.Y >= -0.5f);
				flag2 = (this.currKeyInput.IsKeyDown(Keys.Down) && this.prevKeyInput.IsKeyUp(Keys.Down))
                     || (this.currKeyInput.IsKeyDown(Keys.S) && this.prevKeyInput.IsKeyUp(Keys.S));
				break;
			case Buttons.Start:
				flag2 = (this.currKeyInput.IsKeyDown(Keys.P) && this.prevKeyInput.IsKeyUp(Keys.P))
                     || (this.currKeyInput.IsKeyDown(Keys.Escape) && this.prevKeyInput.IsKeyUp(Keys.Escape));
				break;
			case Buttons.BigButton:
				flag2 = this.currKeyInput.IsKeyDown(Keys.F1) && this.prevKeyInput.IsKeyUp(Keys.F1);
				break;
			}
			return flag || flag2;
		}

		public bool IsButtonUp(Buttons button)
		{
			bool flag = this.currInput.IsButtonUp(button);
			bool flag2 = false;
			switch (button)
			{
			case Buttons.A:
				flag2 = this.currKeyInput.IsKeyUp(Keys.Space) && this.currKeyInput.IsKeyUp(Keys.Enter) && this.currKeyInput.IsKeyUp(Keys.Z) && this.currKeyInput.IsKeyUp(Keys.J) && (!Game1.IsWeb || this.currMouseState.LeftButton == ButtonState.Released);
				break;
			case Buttons.B:
				flag2 = this.currKeyInput.IsKeyUp(Keys.Escape) && this.currKeyInput.IsKeyUp(Keys.Back) && this.currKeyInput.IsKeyUp(Keys.B) && this.currKeyInput.IsKeyUp(Keys.X) && this.currKeyInput.IsKeyUp(Keys.K);
				break;
			case Buttons.DPadLeft:
				flag = flag && (this.currInput.ThumbSticks.Left.X >= -0.5f);
				flag2 = this.currKeyInput.IsKeyUp(Keys.Left) && this.currKeyInput.IsKeyUp(Keys.A);
				break;
			case Buttons.DPadRight:
				flag = flag && (this.currInput.ThumbSticks.Left.X <= 0.5f);
				flag2 = this.currKeyInput.IsKeyUp(Keys.Right) && this.currKeyInput.IsKeyUp(Keys.D);
				break;
			case Buttons.DPadUp:
				flag = flag && (this.currInput.ThumbSticks.Left.Y <= 0.5f);
				flag2 = this.currKeyInput.IsKeyUp(Keys.Up) && this.currKeyInput.IsKeyUp(Keys.W);
				break;
			case Buttons.DPadDown:
				flag = flag && (this.currInput.ThumbSticks.Left.Y >= -0.5f);
				flag2 = this.currKeyInput.IsKeyUp(Keys.Down) && this.currKeyInput.IsKeyUp(Keys.S);
				break;
			case Buttons.Start:
				flag2 = this.currKeyInput.IsKeyUp(Keys.P) && this.currKeyInput.IsKeyUp(Keys.Escape);
				break;
            case Buttons.BigButton:
                flag2 = this.currKeyInput.IsKeyUp(Keys.F1);
                break;
            }
			return flag || flag2;
		}

		public bool IsButtonDown(Buttons button)
		{
			bool flag = this.currInput.IsButtonDown(button);
			bool flag2 = false;
			switch (button)
			{
			case Buttons.A:
				flag2 = this.currKeyInput.IsKeyDown(Keys.Space) || this.currKeyInput.IsKeyDown(Keys.Enter) || this.currKeyInput.IsKeyDown(Keys.Z) || this.currKeyInput.IsKeyDown(Keys.J) || (Game1.IsWeb && this.currMouseState.LeftButton == ButtonState.Pressed);
				break;
			case Buttons.B:
				flag2 = this.currKeyInput.IsKeyDown(Keys.Escape) || this.currKeyInput.IsKeyDown(Keys.Back) || this.currKeyInput.IsKeyDown(Keys.B) || this.currKeyInput.IsKeyDown(Keys.X) || this.currKeyInput.IsKeyDown(Keys.K);
				break;
			case Buttons.DPadLeft:
				flag = flag || (this.currInput.ThumbSticks.Left.X < -0.5f);
				flag2 = this.currKeyInput.IsKeyDown(Keys.Left) || this.currKeyInput.IsKeyDown(Keys.A);
				break;
			case Buttons.DPadRight:
				flag = flag || (this.currInput.ThumbSticks.Left.X > 0.5f);
				flag2 = this.currKeyInput.IsKeyDown(Keys.Right) || this.currKeyInput.IsKeyDown(Keys.D);
				break;
			case Buttons.DPadUp:
				flag = flag || (this.currInput.ThumbSticks.Left.Y > 0.5f);
				flag2 = this.currKeyInput.IsKeyDown(Keys.Up) || this.currKeyInput.IsKeyDown(Keys.W);
				break;
			case Buttons.DPadDown:
				flag = flag || (this.currInput.ThumbSticks.Left.Y < -0.5f);
				flag2 = this.currKeyInput.IsKeyDown(Keys.Down) || this.currKeyInput.IsKeyDown(Keys.S);
				break;
			case Buttons.Start:
				flag2 = this.currKeyInput.IsKeyDown(Keys.P) || this.currKeyInput.IsKeyDown(Keys.Escape);
				break;
            case Buttons.BigButton:
                flag2 = this.currKeyInput.IsKeyDown(Keys.F1);
                break;
            }
			return flag || flag2;
		}
	}
}
