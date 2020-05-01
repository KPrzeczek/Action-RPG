using Microsoft.Xna.Framework.Graphics;
using System;

namespace ARPG.Entities.Sprites.Items.Misc
{
	public class NullItem : Item
	{
		public NullItem(Texture2D tex, string name, int id) : base(tex, name, id)
		{
		}

		public override void OnUse()
		{
			throw new Exception("Almost Heavens, West Virginia... Blue-ridge mountains, Shenandoah River...");
		}
	}
}
