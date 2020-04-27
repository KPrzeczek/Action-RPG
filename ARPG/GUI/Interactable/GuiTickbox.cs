using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.GUI.Interactable
{
	public class GuiTickbox : GuiWidget
	{
		private MouseState currentMouse;
		private MouseState previousMouse;

		private bool isHovering;

		private Texture2D enabledTexture;
		private Texture2D disabledTexture;

		public bool IsEnabled { get; set; }

		public new Rectangle Rectangle
		{
			get
			{
				if(IsEnabled)
				{
					return new Rectangle(
						(int)Position.X - (int)Origin.X,
						(int)Position.Y - (int)Origin.Y,
						(int)enabledTexture.Width,
						(int)enabledTexture.Height
					);
				}
				else
				{
					return new Rectangle(
						(int)Position.X - (int)Origin.X,
						(int)Position.Y - (int)Origin.Y,
						(int)disabledTexture.Width,
						(int)disabledTexture.Height
					);
				}
			}
		}

		public GuiTickbox(Texture2D enabledTex, Texture2D disabledTex)
		{
			enabledTexture = enabledTex;
			disabledTexture = disabledTex;
		}

		public override void Update(float deltaTime)
		{
			currentMouse = Mouse.GetState();

			var mouseRect = new Rectangle(
				currentMouse.X,
				currentMouse.Y,
				1,
				1
			);

			isHovering = false;

			if(mouseRect.Intersects(Rectangle))
			{
				isHovering = true;

				if(currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
				{
					IsEnabled = !IsEnabled;
				}
			}

			previousMouse = currentMouse;
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			Texture2D texture = IsEnabled ? enabledTexture : disabledTexture;
			spriteBatch.Draw(texture, Rectangle, Color.White);
		}
	}
}
