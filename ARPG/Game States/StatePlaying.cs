using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ARPG.Util.Collisions;
using ARPG.Entities.Sprites.Kinematic.Player;
using Microsoft.Xna.Framework;
using ARPG.Entities.Sprites.Static;
using ARPG.Entities;
using ARPG.Util.Debug;
using ARPG.Entities.Sprites.Util.Drawing;
using ARPG.Models.Sprites.Util;
using ARPG.Entities.Sprites.Static.Decor.Forest;

namespace ARPG.Game_States
{
	public class StatePlaying : StateBase
	{
		private DebugConsole debugConsole;

		private List<Entity> entities;

		public StatePlaying(Game1 game, ContentManager content) : base(game, content)
		{
		}

		public override void LoadContent()
		{
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
				Position = new Vector2(100, 100)
			};

			entities.Add(player);
			entities.Add(oakTree);

			debugConsole = new DebugConsole();
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

			debugConsole.Update(deltaTime);
		}

		public override void PostUpdate(float deltaTime)
		{
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
			foreach(var entity in entities)
			{
				entity.Draw(deltaTime, spriteBatch);
			}
		}

		public override void DrawGUI(float deltaTime, SpriteBatch spriteBatch)
		{
			debugConsole.Draw(deltaTime, spriteBatch);
		}
	}
}
