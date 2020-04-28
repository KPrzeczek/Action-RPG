using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Entities.Sprites.Static.Decor.Forest
{
	public class OakTree : DecorSprite
	{
		public OakTree(Texture2D tex) : base(tex)
		{
			AutoSpriteSorter.YOffset = -7;
		}
	}
}
