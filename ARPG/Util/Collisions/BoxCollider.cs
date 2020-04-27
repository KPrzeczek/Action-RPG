using ARPG.Entities.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Util.Collisions
{
	public class BoxCollider : ICollider
	{
		public Sprite Parent;

		public Rectangle Rectangle { get; set; }

		public Vector2 Position
		{
			get
			{
				if(Parent != null)
				{
					return Parent.Position;
				}

				return position;
			}
			set
			{
				if(Parent == null)
				{
					position = value;
				}
			}
		}

		private Vector2 position;
	}
}
