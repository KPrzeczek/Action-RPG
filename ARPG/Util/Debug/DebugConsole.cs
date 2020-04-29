using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPG.GUI.Static;

/*
 * I want to admit that yes this class is a mess, but hey, it's for debug purposes.
 * I might keep it though since it might be fun to mess around with...
 */

namespace ARPG.Util.Debug
{
	public class DebugConsole : IComponent
	{
		private SpriteFont font;

		private GuiText outputText;
		private GuiText inputText;

		private bool canType = true;
		private bool canEnable = true;

		private delegate void CommandFunction(params string[] args);

		private Rectangle rectangle;
		private Dictionary<string, CommandFunction> commands;

		public bool Enabled = false;

		#region Console Variables

		// TODO: Not sure if this is the way to go, so do fix this later
		// TODO: Support for arguments, eg: drawdebuglines true

		public bool NoClip = false;
		public bool ShowDebugLines = false;

		#endregion

		#region Methods

		public DebugConsole(SpriteFont font)
		{
			#region Commands

			commands = new Dictionary<string, CommandFunction>();

			commands.Add("clear", clear);
			commands.Add("help", help);

			commands.Add("showdebuglines", showDebugLines);
			commands.Add("noclip", noClip);

			#endregion

			#region Display

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

			#endregion
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
						else if(keys[ii] == Keys.D1)
						{
							key2str += "1";
						}
						else if(keys[ii] == Keys.D2)
						{
							key2str += "2";
						}
						else if(keys[ii] == Keys.D3)
						{
							key2str += "3";
						}
						else if(keys[ii] == Keys.D4)
						{
							key2str += "4";
						}
						else if(keys[ii] == Keys.D5)
						{
							key2str += "5";
						}
						else if(keys[ii] == Keys.D6)
						{
							key2str += "6";
						}
						else if(keys[ii] == Keys.D7)
						{
							key2str += "7";
						}
						else if(keys[ii] == Keys.D8)
						{
							key2str += "8";
						}
						else if(keys[ii] == Keys.D9)
						{
							key2str += "9";
						}
						else if(keys[ii] == Keys.D0)
						{
							key2str += "0";
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

		void ProcessCommand(string rawCommand)
		{
			char[] delimiters = new char[] { ' ', '\r', '\n' };
			int wordAmount = rawCommand.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;

			string[] words = rawCommand.Split(' ');
			string command = rawCommand.Split(' ')[0];

			List<string> arguments = new List<string>();

			// Add arguments
			foreach(string word in words)
			{
				if(!(word == words[0]))
				{
					arguments.Add(word);
				}
			}

			// If no arguments were given, automatically set argument to false
			if(words.Length <= 1)
			{
				arguments.Add("false");
			}

			// Execute Command
			if(commands.ContainsKey(command))
			{
				string[] args = arguments.ToArray();
				commands[command].Invoke(args);
			}
			else
			{
				outputText.Text += "Unknown Command\n";
			}
		}

		#region Commands

		private void clear(params string[] args)
		{
			outputText.Text = "";
		}

		private void help(params string[] args)
		{
			clear();

			outputText.Text =
				"help - print all commands available\n" +
				"clear - clear output window\n" +
				"noclip - turn off collisions\n" +
				"showdebuglines - enable debug lines\n";
		}

		private void showDebugLines(params string[] args)
		{
			ShowDebugLines = args[0] == "true" ? true : false;
			outputText.Text += "DrawDebugLines set to " + ShowDebugLines + "\n";
		}

		private void noClip(params string[] args)
		{
			NoClip = args[0] == "true" ? true : false;
			outputText.Text += "NoClip set to " + NoClip + "\n";
		}

		#endregion

		#endregion
	}
}
