using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARPG.Entities
{
	public class Entity : IComponent, ICloneable
	{
		public List<Entity> Children { get; private set; }
		public Entity Parent { get; private set; }

		public bool IsRemoved { get; set; }

		public Entity()
		{
			Children = new List<Entity>();
		}

		public virtual void Update(float deltaTime)
		{
		}

		public virtual void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
		}

		public object Clone() => this.MemberwiseClone() as Entity ?? throw new Exception("Failed to return Clone.");
	}
}
