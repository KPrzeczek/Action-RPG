using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARPG.Util.Collisions;
using ARPG.Entities.Sprites.Kinematic.Player;
using Microsoft.Xna.Framework;
using ARPG.Entities.Sprites.Static;
using ARPG.Entities;
using ARPG.Util.Debug;
using ARPG.Entities.Sprites;
using ARPG.Util.Collisions.Colliders;

namespace ARPG.Game_States
{
	public class StatePlaying : StateBase
	{
		private List<Entity> entities;

		public StatePlaying(Game1 game, ContentManager content) : base(game, content)
		{
		}

		public override void LoadContent()
		{
			entities = new List<Entity>();

			var player = new Player(Content.Load<Texture2D>("player/player"))
			{
				Position = new Vector2(75, 50)
			};

			var wall = new Wall(Content.Load<Texture2D>("environment/collidables/wall"))
			{
				Position = new Vector2(150, 100)
			};

			entities.Add(player);
			entities.Add(wall);
		}

		public override void UnloadContent()
		{
		}

		public override void Update(float deltaTime)
		{
			foreach(var entity in entities)
			{
				entity.Update(deltaTime);
			}
		}

		public override void PostUpdate(float deltaTime)
		{
			var collidableSprites = entities.Where(c => c is Sprite && c is ICollidable);

			// TODO: Calculate whether to loop over a certain collidableSprite since looping over all is expensive

			foreach(Sprite a in collidableSprites)
			{
				foreach(Sprite b in collidableSprites)
				{
					if(a == b)
						continue;

					if(a.Collider is PolygonCollider && b.Collider is PolygonCollider)
					{
						if(CollisionHelper.ShapeOverlap_SAT((PolygonCollider)a.Collider, (PolygonCollider)b.Collider))
						{
							((ICollidable)a).OnCollide(b);
						}
					}

					if(a.Collider is BoxCollider && b.Collider is BoxCollider)
					{
						if(CollisionHelper.ShapeOverlap_AABB_STATIC((BoxCollider)a.Collider, (BoxCollider)b.Collider))
						{
							((ICollidable)a).OnCollide(b);
						}
					}
				}
			}

			int entityCount = entities.Count;

			for(int ii = 0; ii < entityCount; ii++)
			{
				Entity entity = entities[ii];
				for(int jj = 0; jj < entity.Children.Count; jj++)
				{
					entities.Add(entity.Children[jj]);
				}

				// ?? (entity.Children = new List<Entity>();) ??
			}

			for(int ii = 0; ii < entities.Count; ii++)
			{
				if(entities[ii].IsRemoved)
				{
					entities.RemoveAt(ii);
				}
			}
		}

		public override void Draw(float deltaTime, SpriteBatch spriteBatch)
		{
			foreach(var entity in entities)
			{
				entity.Draw(deltaTime, spriteBatch);
			}
		}

		public override void DrawGUI(float deltaTime, SpriteBatch spriteBatch)
		{
		}
	}
}
