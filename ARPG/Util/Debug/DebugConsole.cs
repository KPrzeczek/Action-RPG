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
 * I want to admit that yes this class is a complete and utter mess on fire, but hey, it's for 
 * debug purposes. I might keep it though since it might be fun to mess around with...
 */

namespace ARPG.Util.Debug
{
	public class DebugConsole : IComponent
	{
		private Vector2 linePos;

		private SpriteFont font;

		private GuiText outputText;
		private GuiText inputText;

		private float lag = 0;
		private float lagThreshold = 0.6f;

		private bool canPress = true;
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
				Position = new Vector2(10, ((Game1.ScreenHeight / 4) * 3)),
				Colour = Color.White,
				Layer = 0.95f
			};

			outputText = new GuiText(font)
			{
				Text = "Type 'help' for a list of all available commands.\n",
				Position = new Vector2(10, 10),
				Colour = Color.White,
				Layer = 0.95f
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
				inputText.Position = new Vector2(inputText.Position.X, (int)MathHelper.Lerp(inputText.Position.Y, (Game1.ScreenHeight / 4) * 3, 0.2f));
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
				var keys = Keyboard.GetState().GetPressedKeys().ToList();
				string key2str = "";

				if(Keyboard.GetState().GetPressedKeys().Length > 0)
				{
					lag += deltaTime;
				}

				#region Keyboard Detection

				if(canPress)
				{
					int count = keys.Count;

					for(int ii = 0; ii < count; ii++)
					{
						// Remove Key
						if(keys[ii] == Keys.Back)
						{
							if(inputText.Text.Length > 0)
							{
								inputText.Text = inputText.Text.Remove(inputText.Text.Length - 1);
							}
						}

						// Process CMD Key
						else if(keys[ii] == Keys.Enter)
						{
							ProcessCommand(inputText.Text);
							inputText.Text = "";
						}

						// Misc
						else if(keys[ii] == Keys.F3)
							key2str += "";
						else if(keys[ii] == Keys.OemPeriod)
							key2str += ".";
						else if(keys[ii] == Keys.Space)
							key2str += " ";

						// Numbers
						else if(keys[ii] == Keys.D1)
							key2str += "1";
						else if(keys[ii] == Keys.D2)
							key2str += "2";
						else if(keys[ii] == Keys.D3)
							key2str += "3";
						else if(keys[ii] == Keys.D4)
							key2str += "4";
						else if(keys[ii] == Keys.D5)
							key2str += "5";
						else if(keys[ii] == Keys.D6)
							key2str += "6";
						else if(keys[ii] == Keys.D7)
							key2str += "7";
						else if(keys[ii] == Keys.D8)
							key2str += "8";
						else if(keys[ii] == Keys.D9)
							key2str += "9";
						else if(keys[ii] == Keys.D0)
							key2str += "0";

						// Others
						else
							key2str += keys[ii].ToString().ToLower();

						inputText.Text += key2str;
					}

					canPress = false;
				}

				if(lag > lagThreshold)
				{
					for(int ii = 0; ii < keys.Count; ii++)
					{
						// Remove Key
						if(keys[ii] == Keys.Back)
						{
							if(inputText.Text.Length > 0)
							{
								inputText.Text = inputText.Text.Remove(inputText.Text.Length - 1);
							}
						}

						// Process CMD Key
						else if(keys[ii] == Keys.Enter)
						{
							ProcessCommand(inputText.Text);
							inputText.Text = "";
						}

						// Misc
						else if(keys[ii] == Keys.F3)
							key2str += "";
						else if(keys[ii] == Keys.OemPeriod)
							key2str += ".";
						else if(keys[ii] == Keys.Space)
							key2str += " ";

						// Numbers
						else if(keys[ii] == Keys.D1)
							key2str += "1";
						else if(keys[ii] == Keys.D2)
							key2str += "2";
						else if(keys[ii] == Keys.D3)
							key2str += "3";
						else if(keys[ii] == Keys.D4)
							key2str += "4";
						else if(keys[ii] == Keys.D5)
							key2str += "5";
						else if(keys[ii] == Keys.D6)
							key2str += "6";
						else if(keys[ii] == Keys.D7)
							key2str += "7";
						else if(keys[ii] == Keys.D8)
							key2str += "8";
						else if(keys[ii] == Keys.D9)
							key2str += "9";
						else if(keys[ii] == Keys.D0)
							key2str += "0";

						// Others
						else
							key2str += keys[ii].ToString().ToLower();

						inputText.Text += key2str;
					}
				}

				if(Keyboard.GetState().GetPressedKeys().Length <= 0)
				{
					lag = 0;
					canPress = true;
				}

				#endregion
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
				new Color(0, 0, 0, 0.55f)
			);

			// Draw Input Text Background
			DebugTools.DrawRectangle(
				spriteBatch,
				new Rectangle(
					rectangle.X,
					rectangle.Y + ((Game1.ScreenHeight / 4) * 3) - 8,
					rectangle.Width,
					30
				),
				new Color(0.15f, 0.15f, 0.15f, 1f)
			);

			if(Enabled)
			{
				// Draw Output Text
				outputText.Draw(deltaTime, spriteBatch);

				// Draw Input Text
				inputText.Draw(deltaTime, spriteBatch);

				// Draw That Line Thing
				float lenX = font.MeasureString(inputText.Text).X;
				float lenY = font.MeasureString(inputText.Text).Y;

				linePos.X = MathHelper.Lerp(linePos.X, inputText.Position.X + lenX + 5, 0.5f);

				DebugTools.DrawLine(
					spriteBatch,
					new Vector2(linePos.X, inputText.Position.Y),
					new Vector2(linePos.X, inputText.Position.Y + lenY),
					Color.White,
					1
				);
			}
		}

		#endregion

		#region Command Handling

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
				"noclip (true/false) - enable/disable collisions\n" +
				"showdebuglines (true/false) - enable/disable debug lines\n";
		}

		private void showDebugLines(params string[] args)
		{
			ShowDebugLines = args[0] == "true" ? true : false;
			outputText.Text += "showdebuglines set to " + ShowDebugLines + "\n";
		}

		private void noClip(params string[] args)
		{
			NoClip = args[0] == "true" ? true : false;
			outputText.Text += "noclip set to " + NoClip + "\n";
		}

		#endregion

		#endregion
	}
}
