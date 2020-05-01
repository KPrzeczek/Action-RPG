using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARPG.Entities.Sprites.Items.Food
{
	public class AppleItem : Item
	{
		public AppleItem(Texture2D tex, string name, int id) : base(tex, name, id)
		{
			Position = new Vector2(100, 100);
			Scale = 1 / 2f;
		}

		public override void OnUse()
		{
		}
	}
}
