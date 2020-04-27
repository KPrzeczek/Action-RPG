using ARPG.Entities.Sprites;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Util.Collisions
{
	public class PolygonCollider : ICollider
	{
		public List<Vector2> Points;
		public List<Vector2> Original;

		public Sprite Parent;

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
