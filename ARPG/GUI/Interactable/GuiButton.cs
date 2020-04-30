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

		#endregion Fields

		protected Texture2D texture;
		protected Texture2D hoverTexture;
		protected Texture2D pressTexture;

		#region Properties

		public float Scale { get; set; } = 1f;

		public event EventHandler OnClick;
		public bool Clicked { get; private set; }
		public bool Pressed { get; private set; }

		public Color DefaultColour { get; set; } = Color.White;
		public Color HoverColour { get; set; } = Color.White;

		public new Rectangle Rectangle
		{
			get
			{
				return new Rectangle(
					(int)Position.X - (int)(Origin.X * Scale),
					(int)Position.Y - (int)(Origin.Y * Scale),
					(int)(texture.Width * Scale),
					(int)(texture.Height * Scale)
				);
			}
		}

		public GuiText Text { get; set; }

		#endregion Properties

		#region Methods

		public GuiButton(Texture2D tex, Texture2D hoverTex = null, Texture2D pressTex = null, GuiText text = null)
		{
			this.texture = tex;
			this.hoverTexture = hoverTex;
			this.pressTexture = pressTex;

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

			Clicked = false;
			Pressed = false;

			if(mouseRect.Intersects(Rectangle))
			{
				isHovering = true;

				if(currentMouse.LeftButton == ButtonState.Pressed)
				{
					Pressed = true;
				}

				if(currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
				{
					Clicked = true;
					OnClick?.Invoke(this, new EventArgs());
				}
			}

			previousMouse = currentMouse;
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			var colour = DefaultColour;
			var tex = texture;

			if(isHovering)
			{
				colour = HoverColour;

				if(hoverTexture != null)
				{
					tex = hoverTexture;
				}
			}

			if(Pressed)
			{
				if(pressTexture != null)
				{
					tex = pressTexture;
				}
			}

			spriteBatch.Draw(tex, Position, null, colour, 0f, Origin, Scale, SpriteEffects.None, 0.9f);

			if(Text != null)
			{
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
		}

		#endregion Methods
	}
}
