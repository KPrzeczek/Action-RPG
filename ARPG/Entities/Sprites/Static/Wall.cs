using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPG.Util.Collisions;

namespace ARPG.Entities.Sprites.Static
{
	public class Wall : Sprite, ICollidable, ISolid
	{
		public Wall(Texture2D tex) : base(tex)
		{
		}

		public void OnCollide(Sprite other)
		{
		}
	}
}
