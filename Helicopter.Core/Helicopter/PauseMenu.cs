using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Helicopter.Core
{
	internal class PauseMenu : Menu
	{
		private bool canBuyGame;

		public PauseMenu()
			: base(horizontal: false)
		{
		}

		public void Update(float dt, InputState currInput, ref GameState gameState)
		{
			base.Update(dt, currInput);
            Rectangle pause_resume = new(440, 130, 400, 48);
            Rectangle pause_options = new(440, 190, 400, 48);
            Rectangle pause_changeLevel = new(440, 245, 400, 48);
            Rectangle pause_changeKitteh = new(440, 300, 400, 48);
            Rectangle pause_highscore = new(440, 355, 400, 48);
            Rectangle pause_mainMenu = new(440, 405, 400, 48);

            Vector2 touchPos = Game1.GetTouchPosition();
            if (currInput.IsThingTouched())
            {
                System.Console.WriteLine($"[TOUCH LOG] PauseMenu Clicked at Pos=({touchPos.X:F1}, {touchPos.Y:F1})");
            }
            if (pause_resume.Contains(touchPos))
            {
                base.index_ = 0;
                if (currInput.IsThingTouched())
                {
                    System.Console.WriteLine($"[TOUCH ACTION] PauseMenu -> RESUME clicked at ({touchPos.X:F1}, {touchPos.Y:F1})");
                    Global.PlayCatSound();
                    gameState = GameState.PLAY;
                }
            }
            else if (pause_options.Contains(touchPos))
            {
                base.index_ = 1;
                if (currInput.IsThingTouched())
                {
                    System.Console.WriteLine($"[TOUCH ACTION] PauseMenu -> OPTIONS clicked at ({touchPos.X:F1}, {touchPos.Y:F1})");
                    Global.PlayCatSound();
                    gameState = GameState.OPTIONS;
                }
            }
            else if (pause_changeLevel.Contains(touchPos))
            {
                base.index_ = 2;
                if (currInput.IsThingTouched())
                {
                    System.Console.WriteLine($"[TOUCH ACTION] PauseMenu -> CHANGE LEVEL clicked at ({touchPos.X:F1}, {touchPos.Y:F1})");
                    Global.PlayCatSound();
                    gameState = GameState.STAGE_SELECT;
                }
            }
            else if (pause_changeKitteh.Contains(touchPos))
            {
                base.index_ = 3;
                if (currInput.IsThingTouched())
                {
                    System.Console.WriteLine($"[TOUCH ACTION] PauseMenu -> CHANGE KITTEH clicked at ({touchPos.X:F1}, {touchPos.Y:F1})");
                    Global.PlayCatSound();
                    gameState = GameState.CAT_SELECT;
                }
            }
            else if (pause_highscore.Contains(touchPos))
            {
                base.index_ = 4;
                if (currInput.IsThingTouched())
                {
                    System.Console.WriteLine($"[TOUCH ACTION] PauseMenu -> HIGHSCORES clicked at ({touchPos.X:F1}, {touchPos.Y:F1})");
                    Global.PlayCatSound();
                    gameState = GameState.LEADERBOARDS;
                }
            }
            else if (pause_mainMenu.Contains(touchPos))
            {
                base.index_ = 5;
                if (currInput.IsThingTouched())
                {
                    System.Console.WriteLine($"[TOUCH ACTION] PauseMenu -> MAIN MENU clicked at ({touchPos.X:F1}, {touchPos.Y:F1})");
                    Global.PlayCatSound();
                    gameState = GameState.MAIN_MENU;
                }
            }

            if (currInput.IsButtonPressed(Buttons.Start) || currInput.IsButtonPressed(Buttons.B))
			{
				Global.PlayCatSound();
				gameState = GameState.PLAY;
				base.index_ = 0;
			}
			if (!currInput.IsButtonPressed(Buttons.A))
			{
				return;
			}
			Global.PlayCatSound();
			switch (base.index_)
			{
			case 0:
				base.index_ = 0;
				gameState = GameState.PLAY;
				break;
			case 1:
				base.index_ = 0;
				gameState = GameState.OPTIONS;
				break;
			case 2:
				base.index_ = 0;
				gameState = GameState.STAGE_SELECT;
				break;
			case 3:
				base.index_ = 0;
				gameState = GameState.CAT_SELECT;
				break;
			case 4:
				base.index_ = 0;
				gameState = GameState.LEADERBOARDS;
				break;
			case 5:
				if (this.canBuyGame)
				{
					if (Global.IsTrialMode && Global.CanBuyGame())
					{
						//Guide.ShowMarketplace(Global.playerIndex.Value);
					}
				}
				else
				{
					base.index_ = 0;
					gameState = GameState.MAIN_MENU;
				}
				break;
			case 6:
				base.index_ = 0;
				gameState = GameState.MAIN_MENU;
				break;
			}
		}

		public new void Draw(SpriteBatch spriteBatch)
		{
			if (Global.IsTrialMode)
			{
				this.canBuyGame = Global.CanBuyGame();
			}
			else
			{
				this.canBuyGame = false;
			}
			if (this.canBuyGame && (base.menuItems_.Count == 6 || base.menuItems_.Count == 0))
			{
				base.menuItems_.Clear();
				base.index_ = 0;
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(0, 612, 207, 34), new Vector2(640f, 140f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(211, 558, 168, 25), new Vector2(640f, 197.2f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(381, 558, 290, 25), new Vector2(640f, 249.8f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(0, 585, 312, 25), new Vector2(640f, 302.5f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(314, 585, 245, 25), new Vector2(640f, 355.2f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(561, 585, 78, 25), new Vector2(640f, 407.8f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(0, 558, 209, 25), new Vector2(640f, 460.5f)));
				base.SetItemVertices();
			}
			if (!this.canBuyGame && (base.menuItems_.Count == 7 || base.menuItems_.Count == 0))
			{
				base.menuItems_.Clear();
				base.index_ = 0;
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(0, 612, 207, 34), new Vector2(640f, 160f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(211, 558, 168, 25), new Vector2(640f, 217.2f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(381, 558, 290, 25), new Vector2(640f, 269.8f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(0, 585, 312, 25), new Vector2(640f, 322.5f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(314, 585, 245, 25), new Vector2(640f, 375.2f)));
				base.AddMenuItem(new MenuItem(Global.pauseTex, new Rectangle(0, 558, 209, 25), new Vector2(640f, 427.8f)));
				base.SetItemVertices();
			}
			this.DrawBackground(spriteBatch);
			base.Draw(spriteBatch);
		}

		private void DrawBackground(SpriteBatch spriteBatch)
		{
			if (Game1.IsMobile)
			{
                spriteBatch.Draw(Global.pauseTexAndroid, new Vector2(222f, 82f), (Rectangle?)new Rectangle(0, 0, 836, 556), Color.White);
            }
			else if (Game1.IsDesktop || Game1.IsWeb)
			{
                spriteBatch.Draw(Global.pauseTex, new Vector2(222f, 82f), (Rectangle?)new Rectangle(0, 0, 836, 556), Color.White);
            }
		}
	}
}
