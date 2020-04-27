using ARPG.GUI.Static;
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
	public class GuiButton : GuiWidget
	{
		#region Fields

		private MouseState currentMouse;
		private MouseState previousMouse;

		private bool isHovering;

		private Texture2D texture;

		#endregion Fields

		#region Properties

		public event EventHandler Click;
		public bool Clicked { get; private set; }

		public Color DefaultColour { get; set; } = Color.White;
		public Color HoverColour { get; set; } = Color.Gray;

		public new Rectangle Rectangle
		{
			get
			{
				return new Rectangle(
					(int)Position.X - (int)Origin.X,
					(int)Position.Y - (int)Origin.Y,
					(int)texture.Width,
					(int)texture.Height
				);
			}
		}

		public GuiText Text { get; set; }

		#endregion Properties

		#region Methods

		public GuiButton(Texture2D tex, GuiText text)
		{
			this.texture = tex;
			this.Text = text;

			Origin = new Vector2((int)texture.Width / 2, (int)texture.Height / 2);
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
					Click?.Invoke(this, new EventArgs());
				}
			}

			previousMouse = currentMouse;
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			var colour = DefaultColour;

			if(isHovering)
				colour = HoverColour;

			spriteBatch.Draw(texture, Rectangle, colour);

			if(!string.IsNullOrEmpty(Text.Text))
			{
				Text.Origin = new Vector2(
					Text.Font.MeasureString(Text.Text).X / 2f,
					Text.Font.MeasureString(Text.Text).Y / 2f
				);
			}

			Text.Position = Position;
			Text.Draw(deltaTime, spriteBatch);
		}

		#endregion Methods
	}
}
