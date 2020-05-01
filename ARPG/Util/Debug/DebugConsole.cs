using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using ARPG.GUI.Static;

/*
 * I want to admit that yes this class is a complete and utter mess on fire, but hey, it's for 
 * debug purposes. I might keep it though since it might be fun to mess around with...
 * 
 * Strangely enough, it's probably the most nice-looking thing in this entire project!
 */

/*
 * Currently reworking... (PS: I cannot tell you how many times I've re-written this thing... maybe 3 times now?)
 */

namespace ARPG.Util.Debug
{
	public class DebugConsole : IComponent
	{
		private Vector2 linePos;
		private SpriteFont font;

		private KeyboardState lastKeyboardState = Keyboard.GetState();
		private Keys[] lastKeys = Keyboard.GetState().GetPressedKeys();

		private double timer;

		private GuiText outputText;
		private GuiText inputText;

		private Rectangle rectangle;

		private delegate void CommandFunction(params string[] args);
		private Dictionary<string, CommandFunction> commands;

		private bool canEnable = true;

		public bool Enabled { get; set; }

		#region Game Manipulation Variables

		public bool ShowDebugLines { get; set; }
		public bool EnableCollisions { get; set; }

		#endregion

		public DebugConsole(SpriteFont font)
		{
			commands = new Dictionary<string, CommandFunction>();
			commands.Add("clear", clear);
			commands.Add("help", help);

			this.font = font;

			inputText = new GuiText(this.font)
			{
				Text = "",
				Position = new Vector2(10, ((Game1.ScreenHeight / 4) * 3)),
				Colour = Color.White,
				Layer = 0.975f
			};

			outputText = new GuiText(this.font)
			{
				Text = "Type 'help' for a list of all available commands.\n",
				Position = new Vector2(10, 10),
				Colour = Color.White,
				Layer = 0.975f
			};

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
			else if(!Keyboard.GetState().IsKeyDown(Keys.F3))
			{
				canEnable = true;
			}

			if(Enabled)
			{
				rectangle.Y = (int)MathHelper.Lerp(rectangle.Y, 0f, 0.2f);
			}
			else
			{
				rectangle.Y = (int)MathHelper.Lerp(rectangle.Y, (-(Game1.ScreenHeight / 4f) * 3) - 50, 0.2f);
			}

			inputText.Position = new Vector2(inputText.Position.X, rectangle.Y + ((Game1.ScreenHeight / 4f) * 3) + 5);
			outputText.Position = new Vector2(outputText.Position.X, rectangle.Y + 10);

			#endregion

			#region Input

			if(Enabled)
			{
				KeyboardState keyboard = Keyboard.GetState();
				Keys[] keys = keyboard.GetPressedKeys();

				foreach(Keys key in keys)
				{
					if(key != Keys.None)
					{
						if(lastKeys.Contains(key))
						{
							if(timer > 16f)
							{
								timer = 0;
								HandleKey(deltaTime, key);
							}
						}
						else
						{
							HandleKey(deltaTime, key);
						}
					}
				}

				timer += deltaTime;

				lastKeyboardState = keyboard;
				lastKeys = keys;
			}

			#endregion
		}

		public void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			DebugTools.DrawRectangle(
				spriteBatch,
				rectangle,
				new Color(0, 0, 0, 0.55f),
				0.95f
			);

			DebugTools.DrawRectangle(
				spriteBatch,
				new Rectangle(
					rectangle.X,
					rectangle.Y + ((Game1.ScreenHeight / 4) * 3),
					rectangle.Width,
					30
				),
				new Color(0.15f, 0.15f, 0.15f, 1f),
				0.96f
			);

			if(Enabled)
			{
				outputText.Draw(deltaTime, spriteBatch);
				inputText.Draw(deltaTime, spriteBatch);

				float lenX = font.MeasureString(inputText.Text).X;
				float lenY = font.MeasureString(inputText.Text).Y;

				linePos.X = MathHelper.Lerp(linePos.X, inputText.Position.X + lenX + 2.5f, 0.5f);

				DebugTools.DrawLine(
					spriteBatch,
					new Vector2(linePos.X, inputText.Position.Y),
					new Vector2(linePos.X, inputText.Position.Y + lenY),
					Color.White
				);
			}
		}

		public void WriteToConsole(string text)
		{
			outputText.Text += text;
		}

		private void HandleKey(float deltaTime, Keys key)
		{
			string keyString = key.ToString();
			if(key == Keys.Space)
				inputText.Text += " ";
			else if((key == Keys.Back || key == Keys.Delete) && inputText.Text.Length > 0)
				inputText.Text = inputText.Text.Remove(inputText.Text.Length - 1);
			else if(key == Keys.F3)
				inputText.Text += "";
			else if(key == Keys.Enter)
			{
				ProcessCommand(inputText.Text);
				inputText.Text = "";
			}
			else
				inputText.Text += keyString.ToLower();
		}

		#region Command Handling

		private void ProcessCommand(string rawCommand)
		{
			char[] delimiters = new char[] { ' ', '\r', '\n' };
			int wordAmount = rawCommand.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).Length;

			// Command Example:     set       player_dead      true
			//                    keyword        value        args[]

			string[] words = rawCommand.Split(' ');
			string keyword = rawCommand.Split(' ')[0];
			List<string> args = new List<string>();

			// Add Args
			foreach(string word in words)
			{
				if(!(word == words[0]))
				{
					args.Add(word);
				}
			}

			/*
			// If no arguments were given, automatically set argument to nothing
			if(words.Length <= 1)
			{
				arguments.Add("");
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
			*/
		}

		#region Command Functions

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

		#endregion

		#endregion
	}
	/*
		private void showDebugLines(params string[] args)
		{
			if(args[0] == "")
				ShowDebugLines = !ShowDebugLines;
			else
				ShowDebugLines = args[0] == "true" ? true : false;

			outputText.Text += "showdebuglines set to " + ShowDebugLines + "\n";
		}

		private void noClip(params string[] args)
		{
			if(args[0] == "")
				NoClip = !NoClip;
			else
				NoClip = args[0] == "true" ? true : false;

			outputText.Text += "noclip set to " + NoClip + "\n";
		}
	*/
}
