using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ARPG.Util.Collisions;
using ARPG.Entities.Sprites.Kinematic.Player;
using Microsoft.Xna.Framework;
using ARPG.Entities;
using ARPG.Util.Debug;
using ARPG.Models.Sprites.Util;
using ARPG.Entities.Sprites.Static.Decor.Forest;
using ARPG.World.Tiles;
using ARPG.World.Tiles.Forest;
using ARPG.Entities.Sprites;
using ARPG.Util.Collisions.Colliders;
using ARPG.Entities.Sprites.Items;
using ARPG.Entities.Sprites.Items.Food;

namespace ARPG.Game_States
{
	public class StatePlaying : StateBase
	{
		private TileMap tileMap;

		private DebugConsole debugConsole;
		private List<Entity> entities;

		public StatePlaying(Game1 game, ContentManager content) : base(game, content)
		{
		}

		public override void LoadContent()
		{
			debugConsole = new DebugConsole(Content.Load<SpriteFont>("fonts/general/ubuntu_mono"));

			var atlas = Content.Load<Texture2D>("world/forest/tiles/forest_background_tiles");
			tileMap = new TileMap(atlas, new ForestFloorTile(atlas, 3, 1));
			tileMap.Generate(new Vector2[,]
			{
				{ new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0) },
				{ new Vector2(0, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, 0) },
				{ new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(1, 0), new Vector2(0, 0) },
				{ new Vector2(1, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(1, 0) },
				{ new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(2, 0), new Vector2(1, 0), new Vector2(0, 0) },
				{ new Vector2(0, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, 0) },
				{ new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 0), new Vector2(0, 0), new Vector2(0, 0) }
			});

			entities = new List<Entity>();

			var player = new Player(new Dictionary<string, Animation>
			{
				{
					"Walk",
					new Animation(Content.Load<Texture2D>("player/player"), 9, 1)
					{
						IsLooping = true,
						FrameSpeed = 0.1f
					}
				}
			})
			{
				Position = new Vector2(75, 50)
			};
			var oakTree = new OakTree(Content.Load<Texture2D>("world/forest/environment/decor/oak_tree"))
			{
				Position = new Vector2(200, 100)
			};

			entities.Add(player);
			entities.Add(oakTree);

			#region Items

			ItemContainer.GenerateItemDefinitions(Content);
			entities.Add(ItemContainer.GetItem(0));

			#endregion
		}

		public override void UnloadContent()
		{
		}

		public override void Update(float deltaTime)
		{
			debugConsole.Update(deltaTime);

			if(debugConsole.Enabled)
				return;

			foreach(var entity in entities)
			{
				entity.Update(deltaTime);
			}
		}

		public override void PostUpdate(float deltaTime)
		{
			if(debugConsole.NoClip == false)
				CollisionHelper.HandleCollisions(entities);

			int entityCount = entities.Count;

			for(int ii = 0; ii < entityCount; ii++)
			{
				Entity entity = entities[ii];
				for(int jj = 0; jj < entity.Children.Count; jj++)
				{
					entities.Add(entity.Children[jj]);
				}
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
			tileMap.Draw(spriteBatch);

			foreach(var entity in entities)
			{
				entity.Draw(deltaTime, spriteBatch);
			}

			if(debugConsole.ShowDebugLines)
			{
				foreach(Sprite sprite in entities)
				{
					if(sprite.Collider is BoxCollider)
					{
						var r = ((BoxCollider)sprite.Collider).CollisionArea;

						// Draw Outline
						DebugTools.DrawLine(spriteBatch, new Vector2(r.X, r.Y), new Vector2(r.X + r.Width, r.Y), Color.White, 1);
						DebugTools.DrawLine(spriteBatch, new Vector2(r.X + r.Width, r.Y), new Vector2(r.X + r.Width, r.Y + r.Height), Color.White, 1);
						DebugTools.DrawLine(spriteBatch, new Vector2(r.X + r.Width, r.Y + r.Height), new Vector2(r.X, r.Y + r.Height), Color.White, 1);
						DebugTools.DrawLine(spriteBatch, new Vector2(r.X, r.Y + r.Height), new Vector2(r.X, r.Y), Color.White, 1);
					}
				}
			}
		}

		public override void DrawGUI(float deltaTime, SpriteBatch spriteBatch)
		{
			debugConsole.Draw(deltaTime, spriteBatch);
		}
	}
}
