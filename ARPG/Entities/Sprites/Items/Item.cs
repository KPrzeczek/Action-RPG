using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Entities.Sprites.Items
{
	public abstract class Item : Sprite
	{
		private Texture2D tex;

		public string Name { get; protected set; }
		public int MaxStackSize { get; protected set; }

		public Item(Texture2D tex, string name) : base(tex)
		{
			Name = name;
			MaxStackSize = 16;
			AutoSpriteSorter.Continuous = true;
		}

		public override void Update(float deltaTime)
		{
			AutoSpriteSorter.Update(deltaTime);
		}

		public abstract void OnUse();
	}
}
