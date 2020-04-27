using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.GUI.Static
{
	public class GuiText : IGuiComponent
	{
		public string Text { get; set; }
		public SpriteFont Font { get; set; }
		public Color Colour { get; set; }

		public float Layer { get; set; }
		public Vector2 Origin { get; set; }

		public Vector2 Position { get; set; }
		public float Rotation { get; set; }
		public float Scale { get; set; }

		public GuiText(SpriteFont font)
		{
			Font = font;
			Scale = 1f;
		}

		public void Update(float deltaTime)
		{
		}

		public void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			spriteBatch.DrawString(
				Font,
				Text,
				Position,
				Colour,
				Rotation,
				Origin,
				Scale,
				SpriteEffects.None,
				Layer
			);
		}
	}
}
