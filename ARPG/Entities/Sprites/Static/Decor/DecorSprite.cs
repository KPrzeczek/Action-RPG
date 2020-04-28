using Microsoft.Xna.Framework.Graphics;
using ARPG.Util.Collisions;
using Microsoft.Xna.Framework.Input;

namespace ARPG.Entities.Sprites.Static.Decor
{
	public class DecorSprite : Sprite, ICollidable
	{
		public DecorSprite(Texture2D tex) : base(tex)
		{
		}

		public override void Update(float deltaTime)
		{
			AutoSpriteSorter.Update(deltaTime);
		}

		public void OnCollide(Sprite other)
		{
		}
	}
}
