using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPG.GUI.Static;

namespace ARPG.Util.Debug
{
	public class DebugConsole : IComponent
	{
		private SpriteFont font;

		private GuiText outputText;
		private GuiText inputText;

		private bool canType = true;
		private bool canEnable = true;

		private Rectangle rectangle;
		private Dictionary<string, Func<bool>> commands;

		public bool Enabled = false;

		#region Console Variables

		// TODO: Not sure if this is the way to go, so do fix this later

		public bool DrawDebugLines = false;

		#endregion

		#region Methods

		public DebugConsole(SpriteFont font)
		{
			#region Commands

			commands = new Dictionary<string, Func<bool>>();
			commands["drawdebuglines"] = drawdebuglines;

			#endregion

			inputText = new GuiText(font)
			{
				Text = "",
				Position = new Vector2(30, ((Game1.ScreenHeight / 4) * 3) - 50),
				Colour = Color.White
			};

			outputText = new GuiText(font)
			{
				Text = "",
				Position = new Vector2(30, 30),
				Colour = Color.White
			};

			this.font = font;
			rectangle = new Rectangle(
				0,
				0,
				Game1.ScreenWidth,
				(Game1.ScreenHeight / 4) * 3
			);
		}

		public void Update(float deltaTime)
		{
			#region Enabling

			if(Keyboard.GetState().IsKeyDown(Keys.F3) && canEnable)
			{
				Enabled = !Enabled;
				canEnable = false;
			}

			if(!Keyboard.GetState().IsKeyDown(Keys.F3))
			{
				canEnable = true;
			}

			if(Enabled)
			{
				rectangle.Y = (int)MathHelper.Lerp(rectangle.Y, 0f, 0.2f);
				inputText.Position = new Vector2(inputText.Position.X, (int)MathHelper.Lerp(inputText.Position.Y, ((Game1.ScreenHeight / 4) * 3) - 50, 0.2f));
			}
			else
			{
				rectangle.Y = (int)MathHelper.Lerp(rectangle.Y, -Game1.ScreenWidth / 2f, 0.2f);
				inputText.Position = new Vector2(inputText.Position.X, (int)MathHelper.Lerp(inputText.Position.Y, 0f, 0.2f));
			}

			#endregion

			#region Input

			if(Enabled)
			{
				if(canType)
				{
					var keys = Keyboard.GetState().GetPressedKeys();
					string key2str = "";

					for(int ii = 0; ii < keys.Length; ii++)
					{
						if(keys[ii] == Keys.Back)
						{
							if(inputText.Text.Length > 0)
							{
								inputText.Text = inputText.Text.Remove(inputText.Text.Length - 1);
							}
						}
						else if(keys[ii] == Keys.F3)
						{
							key2str += "";
						}
						else if(keys[ii] == Keys.OemPeriod)
						{
							key2str += ".";
						}
						else if(keys[ii] == Keys.Space)
						{
							key2str += " ";
						}
						else if(keys[ii] == Keys.Enter)
						{
							ProcessCommand(inputText.Text);
							inputText.Text = "";
						}
						else
						{
							key2str += keys[ii].ToString().ToLower();
						}

						canType = false;
					}
					inputText.Text += key2str;
				}

				if(Keyboard.GetState().GetPressedKeys().Length <= 0)
				{
					canType = true;
				}
			}
			else
			{
				inputText.Text = "";
			}

			#endregion
		}

		public void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			// Draw Background
			DebugTools.DrawRectangle(
				spriteBatch,
				rectangle,
				new Color(0, 0, 0, 0.55f) // Draw transparent black rectangle
			);

			if(Enabled)
			{
				// Draw Output Text
				outputText.Draw(deltaTime, spriteBatch);

				// Draw Input Text
				spriteBatch.DrawString(font, "> ", new Vector2(inputText.Position.X - 20, inputText.Position.Y), Color.White);
				inputText.Draw(deltaTime, spriteBatch);
			}
		}

		#endregion

		#region Function Handling

		void ProcessCommand(string command)
		{
			if(commands.ContainsKey(command))
			{
				commands[command].Invoke();
			}
			else
			{
				outputText.Text += "Unknown Command\n";
			}
		}

		#region Functions

		private bool drawdebuglines()
		{
			DrawDebugLines = !DrawDebugLines;
			outputText.Text += "DrawDebugLines set to " + DrawDebugLines + "\n";
			return false;
		}

		#endregion

		#endregion
	}
}
