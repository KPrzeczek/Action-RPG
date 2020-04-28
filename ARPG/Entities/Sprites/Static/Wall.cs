using ARPG.Util.Collisions.Colliders;
using Microsoft.Xna.Framework.Graphics;

namespace ARPG.Entities.Sprites.Static
{
	public class Wall : Sprite, ICollidable
	{
		public Wall(Texture2D tex) : base(tex)
		{
		}

		public void OnCollide(Sprite other)
		{
		}
	}
}
