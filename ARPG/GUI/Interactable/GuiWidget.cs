using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.GUI.Interactable
{
	public class GuiWidget : IGuiComponent
	{
		public Vector2 Position { get; set; }
		public Vector2 Origin { get; set; }

		public Rectangle Rectangle { get; protected set; }

		public virtual void Update(float deltaTime)
		{
		}

		public virtual void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
		}
	}
}
