using Microsoft.Xna.Framework.Graphics;
using ARPG.Util.Collisions;

namespace ARPG.Entities.Sprites.Static.Decor.Forest
{
	public class OakTree : DecorSprite, ISolid
	{
		public OakTree(Texture2D tex) : base(tex)
		{
			AutoSpriteSorter.YOffset = -7;

			colliderOffsetX = 14;
			colliderOffsetY = 38;
			colliderOffsetWidth = -29;
			colliderOffsetHeight = -45;
		}
	}
}
