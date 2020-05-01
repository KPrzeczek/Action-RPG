using Microsoft.Xna.Framework.Graphics;

namespace ARPG.Entities.Sprites.Items
{
	public abstract class Item : Sprite
	{
		public Texture2D Icon { get; protected set; }
		public string Name { get; protected set; }
		public int MaxStackSize { get; protected set; }
		public int ID { get; protected set; }

		public Item(Texture2D tex, string name, int id) : base(tex)
		{
			Name = name;
			MaxStackSize = 16;
			AutoSpriteSorter.Continuous = true;
			ID = id;
			Icon = tex; // By default the icon is the texture
		}

		public override void Update(float deltaTime)
		{
			AutoSpriteSorter.Update(deltaTime);
		}

		public abstract void OnUse();
	}
}
